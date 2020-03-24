using CsvHelper.Configuration;
using Stock.Services.Clients;
using Stock.Services.Models;
using Stock.Services.Models.CsvHelper;
using Stock.Services.Models.EF;
using Stock.Services.Models.TDAm;
using Stock.Services.Models.Wtd;
using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TimeZoneConverter;

namespace Stock.Services.Services
{
  public interface IWorldService
  {
    int GetDataFromWtd();
    int TransformWorldPrice();
    void CreateCsvForPrediction(string symbol, string fileName, DateTime dateFrom, DateTime dateTo, bool isBetween, bool isExcludeNullYActual);
  }

  /// <summary>
  /// - Run Prediction at 9:00 ET (14 UTC)
  /// - New York Opens 9:30 ET
  /// - London (FTSE) is 1:00 PM GMT at 9:00 AM ET Dukascopy
  /// - Europe (STOXX) is 2:00 PM CET at 9:00 AM ET Dukascopy
  /// - Germany (GDAXI) is 2:00 PM CET at 9:00 AM ET Dukascopy
  /// - Japan (N225) Closes 3:00 PM JST (1:00 AM ET Same Day) Yahoo.com
  /// - Australia (AXJO) Closes 5:30 PM AEDT (1:30 AM ET Same Day) Yahoo.com
  /// - Hong Kong (HSI) Closes 4:00 PM HKT (3 AM ET Same Day) Yahoo.com
  /// </summary>
  public class WorldService : BaseService, IWorldService
  {
    public int GetDataFromWtd()
    {
      var ret = 0;
      var models = new List<WorldPrice>();
      var indices = DB.WorldPriceIndex.ToList();
      foreach (var index in indices)
      {
        if (index.IsIntraRequired)
        {
          var intraItems = WtdClient.GetPriceIntra(new WtdPriceIntraReq()
          {
            symbol = index.Symbol,
            api_token = Configs.WtdApiKey,
            interval = 60,
            range = 10
          });
          if (intraItems == null)
          {
            // TODO: Notification for Error
            continue;
          }
          foreach (var intraItem in intraItems)
          {
            var timeZone = TZConvert.IanaToWindows(intraItem.timezone_name);
            var priceDate = DateUtils.ToDateTime(intraItem.date, "yyyy-MM-dd HH:mm:ss");
            var utcDate = DateUtils.ToUtc(priceDate, timeZone);
            var model = new WorldPrice()
            {
              Symbol = index.Symbol,
              OpenPrice = intraItem.open,
              ClosePrice = intraItem.close,
              PriceDate = utcDate
            };

            var org = GetWorldPrice(model.Symbol, model.PriceDate);
            if (org == null)
            {
              models.Add(model);
            }
          }
        }
        else
        {
          var dailyItems = WtdClient.GetPriceDaily(new WtdPriceDailyReq()
          {
            symbol = index.Symbol,
            api_token = Configs.WtdApiKey,
            date_from = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd")
          });
          if (dailyItems == null)
          {
            // TODO: Notification for Error
            continue;
          }
          foreach (var dailyItem in dailyItems)
          {
            var model = new WorldPrice()
            {
              Symbol = index.Symbol,
              OpenPrice = dailyItem.open,
              ClosePrice = dailyItem.close,
              PriceDate = DateUtils.ToDateTime(dailyItem.date, "yyyy-MM-dd")
            };

            var org = GetWorldPrice(model.Symbol, model.PriceDate);
            if (org == null)
            {
              models.Add(model);
            }
          }
        }
      }

      ret += Insert<WorldPrice>(models);
      return ret;
    }

    private static readonly DateTime MIN_DATE = new DateTime(2014, 10, 21);
    private static readonly int CALC_HOUR_UTC = 14;
    private static readonly int CLOSE_HOUR_UTC_FTSE = 16;  // 16 UTC
    private static readonly int CLOSE_HOUR_UTC_STOXX = 18; // 17 CET
    private static readonly int CLOSE_HOUR_UTC_GDAXI = 18; // 17 CET 

    public int TransformWorldPrice()
    {
      var ret = 0;
      var models = new List<WorldPriceSet>();

      var now = DateTime.Now;
      var currDate = MIN_DATE;
      while (currDate <= now)
      {
        LogUtils.Debug($"Begin {currDate}");

        var model = new WorldPriceSet();
        model.PriceDate = currDate;

        #region Y (SPY and DIA)
        var spyCurrPrice = GetWorldPrice(Consts.SYMBOL_SPY, currDate);
        model.YSpydiffActual = GetDiffActual(spyCurrPrice);
        model.YSpyactual = GetClass(spyCurrPrice);

        var diaCurrPrice = GetWorldPrice(Consts.SYMBOL_DIA, currDate);
        model.YDiadiffActual = GetDiffActual(diaCurrPrice);
        model.YDiaactual = GetClass(diaCurrPrice);
        #endregion

        #region X Features
        var isValid = true;
        var prevDate = DateUtils.AddBusinessDays(currDate, -1);
        var ftsePrevPrice = GetWorldPrice(Consts.SYMBOL_FTSE, prevDate, CLOSE_HOUR_UTC_FTSE);
        var ftseCurrPrice = GetWorldPrice(Consts.SYMBOL_FTSE, currDate, CALC_HOUR_UTC);
        isValid = isValid &&
          ftsePrevPrice != null &&
          ftsePrevPrice.ClosePrice > 0 &&
          ftseCurrPrice != null &&
          ftseCurrPrice.ClosePrice > 0;
        if (isValid)
        {
          model.XFtsediffNorm = GetDiffNorm(ftsePrevPrice.ClosePrice, ftseCurrPrice.ClosePrice);
        }

        if (isValid)
        {
          var stoxxPrevPrice = GetWorldPrice(Consts.SYMBOL_STOXX, prevDate, CLOSE_HOUR_UTC_STOXX);
          var stoxxCurrPrice = GetWorldPrice(Consts.SYMBOL_STOXX, currDate, CALC_HOUR_UTC);
          isValid = isValid &&
            stoxxPrevPrice != null &&
            stoxxPrevPrice.ClosePrice > 0 &&
            stoxxCurrPrice != null &&
            stoxxCurrPrice.ClosePrice > 0;
          if (isValid)
          {
            model.XStoxxdiffNorm = GetDiffNorm(stoxxPrevPrice.ClosePrice, stoxxCurrPrice.ClosePrice);
          }
        }

        if (isValid)
        {
          var gdaxiPrevPrice = GetWorldPrice(Consts.SYMBOL_GDAXI, prevDate, CLOSE_HOUR_UTC_GDAXI);
          var gdaxiCurrPrice = GetWorldPrice(Consts.SYMBOL_GDAXI, currDate, CALC_HOUR_UTC);
          isValid = isValid &&
            gdaxiPrevPrice != null &&
            gdaxiPrevPrice.ClosePrice > 0 &&
            gdaxiCurrPrice != null &&
            gdaxiCurrPrice.ClosePrice > 0;
          if (isValid)
          {
            model.XGdaxidiffNorm = GetDiffNorm(gdaxiPrevPrice.ClosePrice, gdaxiCurrPrice.ClosePrice);
          }
        }

        if (isValid)
        {
          var n225PrevPrice = GetWorldPrice(Consts.SYMBOL_N225, prevDate);
          var n225CurrPrice = GetWorldPrice(Consts.SYMBOL_N225, currDate);
          isValid = isValid &&
            n225PrevPrice != null &&
            n225PrevPrice.ClosePrice > 0 &&
            n225CurrPrice != null &&
            n225CurrPrice.ClosePrice > 0;
          if (isValid)
          {
            model.XN225diffNorm = GetDiffNorm(n225PrevPrice.ClosePrice, n225CurrPrice.ClosePrice);
          }
        }

        if (isValid)
        {
          var axjoPrevPrice = GetWorldPrice(Consts.SYMBOL_AXJO, prevDate);
          var axjoCurrPrice = GetWorldPrice(Consts.SYMBOL_AXJO, currDate);
          isValid = isValid &&
            axjoPrevPrice != null &&
            axjoPrevPrice.ClosePrice > 0 &&
            axjoCurrPrice != null &&
            axjoCurrPrice.ClosePrice > 0;
          if (isValid)
          {
            model.XAxjodiffNorm = GetDiffNorm(axjoPrevPrice.ClosePrice, axjoCurrPrice.ClosePrice);
          }
        }

        if (isValid)
        {
          var hsiPrevPrice = GetWorldPrice(Consts.SYMBOL_HSI, prevDate);
          var hsiCurrPrice = GetWorldPrice(Consts.SYMBOL_HSI, currDate);
          isValid = isValid &&
            hsiPrevPrice != null &&
            hsiPrevPrice.ClosePrice > 0 &&
            hsiCurrPrice != null &&
            hsiCurrPrice.ClosePrice > 0;
          if (isValid)
          {
            model.XHsidiffNorm = GetDiffNorm(hsiPrevPrice.ClosePrice, hsiCurrPrice.ClosePrice);
          }
        }
        #endregion

        if (isValid)
        {
          models.Add(model);
        }
        currDate = currDate.AddDays(1);
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
      if (symbol == Consts.SYMBOL_SPY && isExcludeNullYActual)
      {
        query = query.Where(w => w.YSpyactual != null);
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