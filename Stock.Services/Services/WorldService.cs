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
      var models = new List<WorldPrice>();
      var indices = DB.WorldPriceIndex.ToList();
      foreach (var index in indices)
      {
        if (index.IsIntraRequired)
        {
          var items = WtdClient.GetPriceIntra(new WtdPriceIntraReq()
          {
            symbol = index.Symbol,
            api_token = Configs.WtdApiKey,
            interval = 60,
            range = 30
          });
          if (items == null)
          {
            // TODO: Notification for Error
            continue;
          }
          foreach (var item in items)
          {
            var timeZone = TZConvert.IanaToWindows(item.timezone_name);
            var priceDate = DateUtils.ToDateTime(item.date, "yyyy-MM-dd HH:mm:ss");
            var utcDate = DateUtils.ToUtc(priceDate, timeZone);
            var model = new WorldPrice()
            {
              Symbol = index.Symbol,
              OpenPrice = item.open,
              ClosePrice = item.close,
              PriceDate = utcDate
            };

            var org = DB.WorldPrice.FirstOrDefault(w =>
              w.Symbol == model.Symbol &&
              w.PriceDate == model.PriceDate);
            if (org == null)
            {
              models.Add(model);
            }
          }
        }
        else
        {

        }
      }


      //var reqs = new List<WtdPriceIntraReq>();
      //foreach (var index in Consts.WORLD_PRICE_INDICES)
      //{
      //  reqs.Add(new WtdPriceIntraReq()
      //  {
      //    symbol = index,
      //    api_token = Configs.WtdApiKey,
      //    interval = 60, // hourly data
      //    range = 10, // days
      //  });
      //}
      //var resps = WtdClient.GetPriceIntra(reqs);
      //if (resps == null)
      //{
      //  // TODO: Notification for Error
      //  return 0;
      //}

      //foreach (var resp in resps)
      //{
      //  var model = new WorldPrice()
      //  {
      //    //priceDate = DateUtils.ToDateTime(price.Name, "yyyy-MM-dd HH:mm:ss"),
      //    Symbol = resp.symbol,
      //    OpenPrice = resp.open,
      //    ClosePrice = resp.close,
      //  };
      //}

      return 0;
      //return Insert<WorldPrice>(models);
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
        var yesterday = DateUtils.AddBusinessDays(spyPrice.PriceDate, -1);

        var yestSpyPrice = GetWorldPrice(Consts.SYMBOL_SPY, yesterday);
        isValid = isValid && yestSpyPrice != null && yestSpyPrice.ClosePrice > 0;
        if (isValid)
        {
          model.PriceDate = spyPrice.PriceDate;
          model.YSpydiffActual = GetDiffActual(spyPrice);
          model.YSpyactual = GetClass(spyPrice);
        }

        var diaPrice = GetWorldPrice(Consts.SYMBOL_DIA, model.PriceDate);
        var yestDiaPrice = GetWorldPrice(Consts.SYMBOL_DIA, yesterday);
        if (diaPrice != null && yestDiaPrice != null)
        {
          model.YDiadiffActual = GetDiffActual(diaPrice);
          model.YDiaactual = GetClass(diaPrice);
        }

        #region X Features
        var ftseStartPrice = GetWorldPrice(Consts.SYMBOL_FTSE, yesterday, CLOSE_HOUR_UTC_FTSE);
        var ftseEndPrice = GetWorldPrice(Consts.SYMBOL_FTSE, model.PriceDate, CALC_HOUR_UTC);
        isValid = isValid &&
          ftseStartPrice != null &&
          ftseStartPrice.ClosePrice > 0 &&
          ftseEndPrice != null &&
          ftseEndPrice.ClosePrice > 0;
        if (isValid)
        {
          model.XFtsediffNorm = GetDiffNorm(ftseStartPrice.ClosePrice, ftseEndPrice.ClosePrice);
        }

        var stoxxStartPrice = GetWorldPrice(Consts.SYMBOL_STOXX, yesterday, CLOSE_HOUR_UTC_STOXX);
        var stoxxEndPrice = GetWorldPrice(Consts.SYMBOL_STOXX, model.PriceDate, CALC_HOUR_UTC);
        isValid = isValid &&
          stoxxStartPrice != null &&
          stoxxStartPrice.ClosePrice > 0 &&
          stoxxEndPrice != null &&
          stoxxEndPrice.ClosePrice > 0;
        if (isValid)
        {
          model.XStoxxdiffNorm = GetDiffNorm(stoxxStartPrice.ClosePrice, stoxxEndPrice.ClosePrice);
        }

        var gdaxiStartPrice = GetWorldPrice(Consts.SYMBOL_GDAXI, yesterday, CLOSE_HOUR_UTC_GDAXI);
        var gdaxiEndPrice = GetWorldPrice(Consts.SYMBOL_GDAXI, model.PriceDate, CALC_HOUR_UTC);
        isValid = isValid &&
          gdaxiStartPrice != null &&
          gdaxiStartPrice.ClosePrice > 0 &&
          gdaxiEndPrice != null &&
          gdaxiEndPrice.ClosePrice > 0;
        if (isValid)
        {
          model.XGdaxidiffNorm = GetDiffNorm(gdaxiStartPrice.ClosePrice, gdaxiEndPrice.ClosePrice);
        }

        var yestN225Price = GetWorldPrice(Consts.SYMBOL_N225, yesterday);
        var n225Price = GetWorldPrice(Consts.SYMBOL_N225, model.PriceDate);
        isValid = isValid &&
          yestN225Price != null &&
          yestN225Price.ClosePrice > 0 &&
          n225Price != null &&
          n225Price.ClosePrice > 0;
        if (isValid)
        {
          model.XN225diffNorm = GetDiffNorm(yestN225Price.ClosePrice, n225Price.ClosePrice);
        }

        var yestAxjoPrice = GetWorldPrice(Consts.SYMBOL_AXJO, yesterday);
        var axjoPrice = GetWorldPrice(Consts.SYMBOL_AXJO, model.PriceDate);
        isValid = isValid &&
          yestAxjoPrice != null &&
          yestAxjoPrice.ClosePrice > 0 &&
          axjoPrice != null &&
          axjoPrice.ClosePrice > 0;
        if (isValid)
        {
          model.XAxjodiffNorm = GetDiffNorm(yestAxjoPrice.ClosePrice, axjoPrice.ClosePrice);
        }

        var yestHsiPrice = GetWorldPrice(Consts.SYMBOL_HSI, yesterday);
        var hsiPrice = GetWorldPrice(Consts.SYMBOL_HSI, model.PriceDate);
        isValid = isValid &&
          yestHsiPrice != null &&
          yestHsiPrice.ClosePrice > 0 &&
          hsiPrice != null &&
          hsiPrice.ClosePrice > 0;
        if (isValid)
        {
          model.XHsidiffNorm = GetDiffNorm(yestHsiPrice.ClosePrice, hsiPrice.ClosePrice);
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