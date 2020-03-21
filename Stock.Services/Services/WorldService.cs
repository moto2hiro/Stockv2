using CsvHelper.Configuration;
using Stock.Services.Clients;
using Stock.Services.Models;
using Stock.Services.Models.CsvHelper;
using Stock.Services.Models.EF;
using Stock.Services.Models.TDAm;
using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Stock.Services.Services
{
  public interface IWorldService
  {
    int TransformWorldPrice();
    void CreateCsvForPrediction(string symbol, string fileName, DateTime dateFrom, DateTime dateTo, bool isBetween, bool isExcludeNullYActual);
  }

  /// <summary>
  /// - Run Prediction at 9:00 ET
  /// - New York Opens 9:30 ET
  /// - London (FTSE) is 1:00 PM GMT at 9:00 AM ET Dukascopy (2008)
  /// - Europe (STOXX) is 2:00 PM CET at 9:00 AM ET Dukascopy (2014)
  /// - Germany (GDAXI) is 2:00 PM CET at 9:00 AM ET Dukascopy (2012)
  /// - Switzerland (SSMI) is 2:00 PM CET at 9:00 AM ET Dukascopy (2013)
  /// - Japan (N225) Close 3:00 PM JST (1:00 AM ET Same Day) Yahoo.com
  /// - Australia (AXJO) Close 5:30 PM AEDT (1:30 AM ET Same Day) Yahoo.com
  /// - Hong Kong (HSI) Close 4:00 PM HKT (3 AM ET Same Day) Yahoo.com
  /// - Shanghai (SSEC) Close 3:00 PM CST (2:00 AM ET Same Day) Investing.com
  /// - Bombay (BSESN) Close 3:30 PM IST (5:00 AM ET Same Day) Yahoo.com
  /// - India (NIFTY) Close 3:30 PM IST (5:00 AM ET Same Day) Investing.com
  /// - Korea (KS11) Close 3:30 PM KST (1:30 AM ET Same Day) Yahoo.com
  /// - Taiwan (TWII) Close 3:30 PM CST (2:00 AM ET Same Day) Yahoo.com
  /// </summary>
  public class WorldService : BaseService, IWorldService
  {
    private static readonly DateTime MIN_DATE = new DateTime(2014, 10, 20);
    private static readonly int CALC_HOUR_UTC = 14;
    private static readonly int OPEN_HOUR_UTC_FTSE = 8;
    private static readonly int OPEN_HOUR_UTC_STOXX = 7;
    private static readonly int OPEN_HOUR_UTC_GDAXI = 7;
    private static readonly int OPEN_HOUR_UTC_SSMI = 8;

    public int TransformWorldPrice()
    {
      var ret = 0;
      var models = new List<WorldPriceSet>();
      var spyPrices = DB.WorldPrice
        .Where(w => w.Symbol == Consts.SYMBOL_SPY && w.PriceDate >= MIN_DATE)
        .OrderBy(w => w.PriceDate)
        .ToList();
      if (spyPrices == null)
      {
        return ret;
      }
      foreach (var spyPrice in spyPrices)
      {
        LogUtils.Debug($"Begin {spyPrice.PriceDate}");
        var model = new WorldPriceSet();
        var isValid = true;

        isValid = isValid && spyPrice.OpenPrice > 0;
        if (isValid)
        {
          model.PriceDate = spyPrice.PriceDate;
          model.YSpydiffNorm = GetDiffNorm(spyPrice.OpenPrice, spyPrice.ClosePrice);
          model.YSpydiffActual = GetDiffActual(spyPrice);
          model.YSpyactual = GetClass(spyPrice);
        }

        var gspcPrice = GetWorldPrice(Consts.SYMBOL_GSPC, model.PriceDate);
        if (gspcPrice != null)
        {
          model.YGspcdiffNorm = GetDiffNorm(gspcPrice.OpenPrice, gspcPrice.ClosePrice);
          model.YGspcdiffActual = GetDiffActual(gspcPrice);
          model.YGspcactual = GetClass(gspcPrice);
        }

        var djiPrice = GetWorldPrice(Consts.SYMBOL_DJI, model.PriceDate);
        if (djiPrice != null)
        {
          model.YDjidiffNorm = GetDiffNorm(djiPrice.OpenPrice, djiPrice.ClosePrice);
          model.YDjidiffActual = GetDiffActual(djiPrice);
          model.YDjiactual = GetClass(djiPrice);
        }

        var diaPrice = GetWorldPrice(Consts.SYMBOL_DIA, model.PriceDate);
        if (diaPrice != null)
        {
          model.YDiadiffNorm = GetDiffNorm(diaPrice.OpenPrice, diaPrice.ClosePrice);
          model.YDiadiffActual = GetDiffActual(diaPrice);
          model.YDiaactual = GetClass(diaPrice);
        }

        #region X Features
        var ftseOpenPrice = GetWorldPrice(Consts.SYMBOL_FTSE, model.PriceDate, OPEN_HOUR_UTC_FTSE);
        var ftseEndPrice = GetWorldPrice(Consts.SYMBOL_FTSE, model.PriceDate, CALC_HOUR_UTC);
        isValid = isValid &&
          ftseOpenPrice != null &&
          ftseOpenPrice.OpenPrice > 0 &&
          ftseEndPrice != null &&
          ftseEndPrice.OpenPrice > 0;
        if (isValid)
        {
          model.XFtsediffNorm = GetDiffNorm(ftseOpenPrice.OpenPrice, ftseEndPrice.ClosePrice);
        }

        var stoxxOpenPrice = GetWorldPrice(Consts.SYMBOL_STOXX, model.PriceDate, OPEN_HOUR_UTC_STOXX);
        var stoxxEndPrice = GetWorldPrice(Consts.SYMBOL_STOXX, model.PriceDate, CALC_HOUR_UTC);
        isValid = isValid &&
          stoxxOpenPrice != null &&
          stoxxOpenPrice.OpenPrice > 0 &&
          stoxxEndPrice != null &&
          stoxxEndPrice.OpenPrice > 0;
        if (isValid)
        {
          model.XStoxxdiffNorm = GetDiffNorm(stoxxOpenPrice.OpenPrice, stoxxEndPrice.ClosePrice);
        }

        var gdaxiOpenPrice = GetWorldPrice(Consts.SYMBOL_GDAXI, model.PriceDate, OPEN_HOUR_UTC_GDAXI);
        var gdaxiEndPrice = GetWorldPrice(Consts.SYMBOL_GDAXI, model.PriceDate, CALC_HOUR_UTC);
        isValid = isValid &&
          gdaxiOpenPrice != null &&
          gdaxiOpenPrice.OpenPrice > 0 &&
          gdaxiEndPrice != null &&
          gdaxiEndPrice.OpenPrice > 0;
        if (isValid)
        {
          model.XGdaxidiffNorm = GetDiffNorm(gdaxiOpenPrice.OpenPrice, gdaxiEndPrice.ClosePrice);
        }

        var ssmiOpenPrice = GetWorldPrice(Consts.SYMBOL_SSMI, model.PriceDate, OPEN_HOUR_UTC_SSMI);
        var ssmiEndPrice = GetWorldPrice(Consts.SYMBOL_SSMI, model.PriceDate, CALC_HOUR_UTC);
        isValid = isValid &&
          ssmiOpenPrice != null &&
          ssmiOpenPrice.OpenPrice > 0 &&
          ssmiEndPrice != null &&
          ssmiEndPrice.OpenPrice > 0;
        if (isValid)
        {
          model.XSsmidiffNorm = GetDiffNorm(ssmiOpenPrice.OpenPrice, ssmiEndPrice.ClosePrice);
        }

        var n225Price = GetWorldPrice(Consts.SYMBOL_N225, model.PriceDate);
        isValid = isValid && n225Price != null && n225Price.OpenPrice > 0;
        if (isValid)
        {
          model.XN225diffNorm = GetDiffNorm(n225Price.OpenPrice, n225Price.ClosePrice);
        }

        var axjoPrice = GetWorldPrice(Consts.SYMBOL_AXJO, model.PriceDate);
        isValid = isValid && axjoPrice != null && axjoPrice.OpenPrice > 0;
        if (isValid)
        {
          model.XAxjodiffNorm = GetDiffNorm(axjoPrice.OpenPrice, axjoPrice.ClosePrice);
        }

        var hsiPrice = GetWorldPrice(Consts.SYMBOL_HSI, model.PriceDate);
        isValid = isValid && hsiPrice != null && hsiPrice.OpenPrice > 0;
        if (isValid)
        {
          model.XHsidiffNorm = GetDiffNorm(hsiPrice.OpenPrice, hsiPrice.ClosePrice);
        }

        var ssecPrice = GetWorldPrice(Consts.SYMBOL_SSEC, model.PriceDate);
        isValid = isValid && ssecPrice != null && ssecPrice.OpenPrice > 0;
        if (isValid)
        {
          model.XSsecdiffNorm = GetDiffNorm(ssecPrice.OpenPrice, ssecPrice.ClosePrice);
        }

        var bsesnPrice = GetWorldPrice(Consts.SYMBOL_BSESN, model.PriceDate);
        isValid = isValid && bsesnPrice != null && bsesnPrice.OpenPrice > 0;
        if (isValid)
        {
          model.XBsesndiffNorm = GetDiffNorm(bsesnPrice.OpenPrice, bsesnPrice.ClosePrice);
        }

        var niftyPrice = GetWorldPrice(Consts.SYMBOL_NIFTY, model.PriceDate);
        isValid = isValid && niftyPrice != null && niftyPrice.OpenPrice > 0;
        if (isValid)
        {
          model.XNiftydiffNorm = GetDiffNorm(niftyPrice.OpenPrice, niftyPrice.ClosePrice);
        }

        var ks11Price = GetWorldPrice(Consts.SYMBOL_KS11, model.PriceDate);
        isValid = isValid && ks11Price != null && ks11Price.OpenPrice > 0;
        if (isValid)
        {
          model.XKs11diffNorm = GetDiffNorm(ks11Price.OpenPrice, ks11Price.ClosePrice);
        }

        var twiiPrice = GetWorldPrice(Consts.SYMBOL_TWII, model.PriceDate);
        isValid = isValid && twiiPrice != null && twiiPrice.OpenPrice > 0;
        if (isValid)
        {
          model.XTwiidiffNorm = GetDiffNorm(twiiPrice.OpenPrice, twiiPrice.ClosePrice);
        }
        #endregion

        if (isValid)
        {
          models.Add(model);
        }
      }

      ret += DeleteAll<WorldPriceSet>();
      ret += Insert<WorldPriceSet>(models);
      return ret;
    }

    private WorldPrice GetWorldPrice(string symbol, DateTime priceDate, int hours = 0)
    {
      priceDate = priceDate.AddHours(hours);
      return DB.WorldPrice.FirstOrDefault(w => w.Symbol == symbol && w.PriceDate == priceDate);
    }

    /// <summary>
    /// Difference Normalized = (x(t) - x(t - tilda)) / x(t - tilda)
    /// </summary>
    /// <param name="worldPrice"></param>
    /// <returns></returns>
    private decimal? GetDiffNorm(decimal startPrice, decimal endPrice)
    {
      if (startPrice <= 0)
        return null;
      else
        return NumberUtils.Round((endPrice - startPrice) / startPrice, 4);
    }

    private decimal? GetDiffActual(WorldPrice price)
    {
      if (price == null || price.OpenPrice <= 0)
        return null;
      else
        return NumberUtils.Round(price.ClosePrice - price.OpenPrice);
    }

    private int? GetClass(WorldPrice price)
    {
      if (price == null || price.OpenPrice <= 0)
        return null;
      else
        return (GetDiffActual(price) > 0) ? Consts.CLASS_UP : Consts.CLASS_DOWN;
    }

    public void CreateCsvForPrediction(string symbol, string fileName, DateTime dateFrom, DateTime dateTo, bool isBetween, bool isExcludeNullYActual)
    {
      var query = DB.WorldPriceSet.AsQueryable();
      if (symbol == Consts.SYMBOL_GSPC && isExcludeNullYActual)
      {
        query = query.Where(w => w.YGspcactual != null);
      }
      else if (symbol == Consts.SYMBOL_SPY && isExcludeNullYActual)
      {
        query = query.Where(w => w.YSpyactual != null);
      }
      else if (symbol == Consts.SYMBOL_DJI && isExcludeNullYActual)
      {
        query = query.Where(w => w.YDjiactual != null);
      }
      else if (symbol == Consts.SYMBOL_DIA && isExcludeNullYActual)
      {
        query = query.Where(w => w.YDiaactual != null);
      }
      if (isBetween)
      {
        query = query.Where(c => c.PriceDate >= dateFrom && c.PriceDate <= dateTo);
      }
      else
      {
        query = query.Where(c => c.PriceDate <= dateFrom || c.PriceDate >= dateTo);
      }
      CsvUtils.WriteCsv(fileName, query.ToList());
    }
  }
}