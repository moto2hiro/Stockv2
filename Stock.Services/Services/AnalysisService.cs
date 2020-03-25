using Stock.Services.Models;
using Stock.Services.Models.EF;
using Stock.Services.Services.TechnicalServices;
using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Stock.Services.Services
{
  public interface IAnalysisService
  {
    int GetTechnicals();
    int GetYActuals();
  }

  public class AnalysisService : BaseService, IAnalysisService
  {
    public int GetTechnicals()
    {
      var sp = new Stopwatch();
      sp.Start();
      LogUtils.Debug($"START-GetTechnicals");

      var ret = 0;
      var symbols = DB.SymbolMaster.OrderBy(s => s.Symbol).ToList();
      foreach (var symbol in symbols)
      {
        LogUtils.Debug($"START-{nameof(GetTechnicals)} {symbol.Symbol}");
        var models = new List<Technical>();
        var items = DB.StockPrice
          .Where(s => s.SymbolId == symbol.Id)
          .OrderBy(s => s.PriceDate)
          .ToList();
        if (items == null || items.Count == 0)
        {
          continue;
        }

        foreach (var item in items.Select((value, idx) => (value, idx)))
        {
          var smas = new SmaTechnicalService().Calculate(
            item.idx,
            Consts.SMA_PERIODS,
            items);
          smas.ForEach(s => models.Add(s));

          var emas = new EmaTechnicalService().Calculate(
            item.idx,
            Consts.EMA_PERIODS,
            items);
          emas.ForEach(e => models.Add(e));

          var adtvs = new AdtvTechnicalService().Calculate(
            item.idx,
            Consts.ADTV_PERIODS,
            items);
          adtvs.ForEach(a => models.Add(a));

          var adpvs = new AdpvTechnicalService().Calculate(
            item.idx,
            Consts.ADPV_PERIODS,
            items);
          adpvs.ForEach(a => models.Add(a));
        }

        var rsis = new RsiTechnicalService().Calculate(
          Consts.SMA_PERIODS, 
          items);
        rsis.ForEach(r => models.Add(r));

        // TO DO Check if it exists already.
        ret += Insert<Technical>(models);
      }

      sp.Stop();
      LogUtils.Debug($"END-GetTechnicals={sp.Elapsed}");

      return ret;
    }

    public int GetYActuals()
    {
      var ret = 0;
      var symbols = DB.SymbolMaster.OrderBy(s => s.Symbol).ToList();
      foreach (var symbol in symbols)
      {
        LogUtils.Debug($"START-{nameof(GetYActuals)} {symbol.Symbol}");
        var models = new List<Yactual>();
        var items = DB.StockPrice
          .Where(s => s.SymbolId == symbol.Id)
          .OrderBy(s => s.PriceDate)
          .ToList();
        if (items == null || items.Count == 0)
        {
          continue;
        }
        foreach (var item in items.Select((value, idx) => (value, idx)))
        {
          if (items.ElementAtOrDefault(item.idx + Consts.YACTUAL_MAX_FUTURE + 1) == null)
          {
            continue;
          }
          var noOfFutureDays = 1;
          while (noOfFutureDays <= Consts.YACTUAL_MAX_FUTURE)
          {
            var currIdx = item.idx;
            var priceDiff = items[currIdx + noOfFutureDays].ClosePrice - items[currIdx].ClosePrice;
            models.Add(new Yactual()
            {
              StockPriceId = item.value.Id,
              Category = (priceDiff > 0) ? Consts.CLASS_UP : Consts.CLASS_DOWN,
              NoOfFutureDays = noOfFutureDays,
            });
            noOfFutureDays += 1;
          }
        }

        // TO DO Check if it exists already.
        //ret += Insert<Yactual>(models);
      }
      return ret;
    }
  }
}
