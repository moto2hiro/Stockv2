using Stock.Services.Models;
using Stock.Services.Models.EF;
using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Stock.Services.Services
{
  public interface IAnalysisService
  {
    List<List<StockPrice>> GetPricesForCharts(BackTestReq req);
  }

  public class AnalysisService : BaseService, IAnalysisService
  {
    // TO DO: Do injection.
    private StockService _stockService = new StockService();

    public List<List<StockPrice>> GetPricesForCharts(BackTestReq req)
    {
      var ret = new List<List<StockPrice>>();
      var symbols = _stockService.GetSymbols();
      if (symbols == null)
      {
        return ret;
      }

      #region FOR TESTING
      // Get random symbols
      symbols = symbols.OrderBy(s => Guid.NewGuid()).Take(10).ToList();
      LogUtils.Debug($"Symbols = {symbols.Count} records.");
      #endregion

      foreach (var symbol in symbols)
      {
        var spPerSymbol = new Stopwatch();
        spPerSymbol.Start();
        LogUtils.Debug($"START-{symbol.Symbol}");

        var query = DB.StockPrice.AsQueryable();
        query = query.Where(s => s.SymbolId == symbol.Id);

        #region FOR TESTING
        var randIdx = new Random().Next(500000);
        query = query.Skip(randIdx);
        query = query.Take(req.LookBack);
        #endregion

        query = query.OrderBy(s => s.PriceDate);
        var items = query.ToList();
        var itemCnt = items.Count;

        LogUtils.Debug($"Index={randIdx}, Count = {itemCnt}");
        if (itemCnt < req.LookBack)
        {
          continue;
        }

        var currIdx = 0;
        foreach (var item in items)
        {
          var prevItem = items.ElementAtOrDefault(currIdx - 1);
          var nextItem = items.ElementAtOrDefault(currIdx + 1);

          // Price Smoothing
          // https://en.wikipedia.org/wiki/Exponential_smoothing
          // s(t) = x(t), t = 0
          // s(t) = alpha * x(t) + (1 - alpha) * s(t - 1), t > 1
          // where 0 < alpha < 1
          #region Price Smoothing to reduce Noise

          if (prevItem == null)
          {
            item.SmoothedPrice = item.ClosePrice;
          }
          else
          {
            var smoothed = req.SmoothingAlpha * item.ClosePrice + (1 - req.SmoothingAlpha) * prevItem.SmoothedPrice;
            item.SmoothedPrice = NumberUtils.Round(smoothed);
          }

          #endregion

          currIdx += 1;
        }

        currIdx = 0;
        foreach (var item in items)
        {
          var prevItem = items.ElementAtOrDefault(currIdx - 1);
          var nextItem = items.ElementAtOrDefault(currIdx + 1);

          // Local Max/Min
          // https://stackoverflow.com/questions/8587047/support-resistance-algorithm-technical-analysis
          // Local Max = Lower/Higher/Lower
          // Local Min = Higher/Lower/Higher
          #region Local Max/Min

          if (prevItem != null && nextItem != null)
          {
            item.IsLocalMax = item.SmoothedPrice > prevItem.SmoothedPrice && item.SmoothedPrice > nextItem.SmoothedPrice;
            item.IsLocalMin = item.SmoothedPrice < prevItem.SmoothedPrice && item.SmoothedPrice < nextItem.SmoothedPrice;
          }

          #endregion

          currIdx += 1;
        }

        ret.Add(items);

        spPerSymbol.Stop();
        LogUtils.Debug($"END-{spPerSymbol.Elapsed}");
      }

      return ret;
    }
  }
}
