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
    int AddWorldPriceFromCsv<T>(string symbol, string fileName) where T : ClassMap;
    int TransformWorldPrice();
    void CreateCsvForPrediction(string symbol, string fileName, DateTime dateFrom, DateTime dateTo, bool isBetween, bool isExcludeNullYActual);
  }

  /// <summary>
  /// - New York Opens 9:30 ET
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
    /// <summary>
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public int AddWorldPriceFromCsv<T>(string symbol, string fileName) where T : ClassMap
    {
      var ret = 0;
      if (!string.IsNullOrEmpty(symbol) && File.Exists(fileName))
      {
        var models = new List<WorldPrice>();
        var records = CsvUtils.ParseCsv<WorldPrice, T>(fileName);
        if (records != null)
        {
          foreach (var record in records)
          {
            var orgStockPrice = DB.WorldPrice.FirstOrDefault(w => w.Symbol == symbol && w.PriceDate == record.PriceDate);
            if (orgStockPrice == null)
            {
              record.Symbol = symbol;
              models.Add(record);
            }
          }
          Insert<WorldPrice>(models);
        }
      }
      return ret;
    }

    public int TransformWorldPrice()
    {
      var ret = 0;
      var models = new List<WorldPriceSet>();
      var gspcPrices = DB.WorldPrice.Where(w => w.Symbol == Consts.SYMBOL_GSPC).OrderBy(w => w.PriceDate).ToList();
      foreach (var gspcPrice in gspcPrices)
      {
        LogUtils.Debug($"Begin {gspcPrice.PriceDate}");
        var model = new WorldPriceSet();
        var isValid = true;

        isValid = isValid && gspcPrice.OpenPrice > 0;
        model.PriceDate = gspcPrice.PriceDate;
        model.YGspcdiffNorm = GetDiffNorm(gspcPrice);
        model.YGspcdiffActual = GetDiffActual(gspcPrice);
        model.YGspcactual = GetClass(gspcPrice);

        var spyPrice = GetWorldPrice(Consts.SYMBOL_SPY, model.PriceDate);
        model.YSpydiffNorm = GetDiffNorm(spyPrice);
        model.YSpydiffActual = GetDiffActual(spyPrice);
        model.YSpyactual = GetClass(spyPrice);

        var djiPrice = GetWorldPrice(Consts.SYMBOL_DJI, model.PriceDate);
        model.YDjidiffNorm = GetDiffNorm(djiPrice);
        model.YDjidiffActual = GetDiffActual(djiPrice);
        model.YDjiactual = GetClass(djiPrice);

        var diaPrice = GetWorldPrice(Consts.SYMBOL_DIA, model.PriceDate);
        model.YDiadiffNorm = GetDiffNorm(diaPrice);
        model.YDiadiffActual = GetDiffActual(diaPrice);
        model.YDiaactual = GetClass(diaPrice);

        #region X Features
        if (isValid)
        {
          var n225Price = GetWorldPrice(Consts.SYMBOL_N225, model.PriceDate);
          isValid = isValid && n225Price != null && n225Price.OpenPrice > 0;
          model.XN225diffNorm = GetDiffNorm(n225Price);
        }
        if (isValid)
        {
          var axjoPrice = GetWorldPrice(Consts.SYMBOL_AXJO, model.PriceDate);
          isValid = isValid && axjoPrice != null && axjoPrice.OpenPrice > 0;
          model.XAxjodiffNorm = GetDiffNorm(axjoPrice);
        }
        if (isValid)
        {
          var hsiPrice = GetWorldPrice(Consts.SYMBOL_HSI, model.PriceDate);
          isValid = isValid && hsiPrice != null && hsiPrice.OpenPrice > 0;
          model.XHsidiffNorm = GetDiffNorm(hsiPrice);
        }
        if (isValid)
        {
          var ssecPrice = GetWorldPrice(Consts.SYMBOL_SSEC, model.PriceDate);
          isValid = isValid && ssecPrice != null && ssecPrice.OpenPrice > 0;
          model.XSsecdiffNorm = GetDiffNorm(ssecPrice);
        }
        if (isValid)
        {
          var bsesnPrice = GetWorldPrice(Consts.SYMBOL_BSESN, model.PriceDate);
          isValid = isValid && bsesnPrice != null && bsesnPrice.OpenPrice > 0;
          model.XBsesndiffNorm = GetDiffNorm(bsesnPrice);
        }
        if (isValid)
        {
          var niftyPrice = GetWorldPrice(Consts.SYMBOL_NIFTY, model.PriceDate);
          isValid = isValid && niftyPrice != null && niftyPrice.OpenPrice > 0;
          model.XNiftydiffNorm = GetDiffNorm(niftyPrice);
        }
        if (isValid)
        {
          var ks11Price = GetWorldPrice(Consts.SYMBOL_KS11, model.PriceDate);
          isValid = isValid && ks11Price != null && ks11Price.OpenPrice > 0;
          model.XKs11diffNorm = GetDiffNorm(ks11Price);
        }
        if (isValid)
        {
          var twiiPrice = GetWorldPrice(Consts.SYMBOL_TWII, model.PriceDate);
          isValid = isValid && twiiPrice != null && twiiPrice.OpenPrice > 0;
          model.XTwiidiffNorm = GetDiffNorm(twiiPrice);
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

    private WorldPrice GetWorldPrice(string symbol, DateTime priceDate)
    {
      return DB.WorldPrice.FirstOrDefault(w => w.Symbol == symbol && w.PriceDate == priceDate);
    }

    /// <summary>
    /// Difference Normalized = (x(t) - x(t - tilda)) / x(t - tilda)
    /// </summary>
    /// <param name="worldPrice"></param>
    /// <returns></returns>
    private decimal? GetDiffNorm(WorldPrice price)
    {
      if (price == null || price.OpenPrice <= 0)
        return null;
      else
        return NumberUtils.Round((price.ClosePrice - price.OpenPrice) / price.OpenPrice, 4);
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