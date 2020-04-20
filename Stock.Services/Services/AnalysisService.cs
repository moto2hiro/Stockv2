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
      symbols = symbols.OrderBy(s => Guid.NewGuid()).ToList();
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

        #region ADPV

        //var avgAdpv = items.Average(i => i.ClosePrice * i.Volume);
        //if (avgAdpv < 300000) // 200,000,000 / 7 hours / 12 * 5 minutes
        //{
        //  LogUtils.Debug($"ADPV = {avgAdpv}");
        //  continue;
        //}

        #endregion


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

        // Polynomial Fitting
        // https://github.com/tristcoil/detect-double-bottom-in-stocks/blob/master/Detect%20double%20bottom%20in%20stocks%20with%20python.ipynb
        // Fit polynomial
        // Max = f' == 0 && f'' < 0
        // Min = f' == 0 && f'' > 0
        #region Polynomial Fitting

        var x_values = items.Select((value, index) => (double)index).ToArray();
        var y_values = items.Select((value, index) => (double)value.ClosePrice).ToArray();
        var polynomial = MathNet.Numerics.Polynomial.Fit(x_values, y_values, req.PolynomialOrder);
        var firstDerivs = polynomial.Differentiate();
        var secondDerivs = firstDerivs.Differentiate();

        currIdx = 0;
        foreach (var item in items)
        {
          var prevItem = items.ElementAtOrDefault(currIdx - 1);
          var nextItem = items.ElementAtOrDefault(currIdx + 1);
          item.PolynomialPrice = (decimal)polynomial.Evaluate(currIdx);
          item.FirstDerivative = (decimal)firstDerivs.Evaluate(currIdx);
          item.SecondDerivative = (decimal)secondDerivs.Evaluate(currIdx);
          item.IsPolynomialMax =
            prevItem != null &&
            prevItem.FirstDerivative > 0 &&
            item.FirstDerivative <= 0;
          item.IsPolynomialMin =
            prevItem != null &&
            prevItem.FirstDerivative < 0 &&
            item.FirstDerivative >= 0;
          currIdx += 1;
        }

        #endregion

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
            item.IsLocalMax =
              (item.SmoothedPrice > prevItem.SmoothedPrice && item.SmoothedPrice > nextItem.SmoothedPrice) ||
              (item.SmoothedPrice > prevItem.SmoothedPrice && item.SmoothedPrice == nextItem.SmoothedPrice);
            item.IsLocalMin =
              (item.SmoothedPrice < prevItem.SmoothedPrice && item.SmoothedPrice < nextItem.SmoothedPrice) ||
              (item.SmoothedPrice < prevItem.SmoothedPrice && item.SmoothedPrice == nextItem.SmoothedPrice);
          }

          #endregion

          currIdx += 1;
        }

        #region Support/Resistance

        var resistanceDict = new Dictionary<int, decimal>();
        var localMaxes = items.Where(s => s.IsPolynomialMax).ToList();
        foreach (var localMax in localMaxes)
        {
          var resistanceCnt = localMaxes
            .Where(m =>
              NumberUtils.PctChange(m.PolynomialPrice, localMax.PolynomialPrice) < req.SupportResistanceProximityPct &&
              m.PolynomialPrice < localMax.PolynomialPrice)
            .Count();
          if (resistanceDict.ContainsKey(resistanceCnt) && localMax.PolynomialPrice > resistanceDict[resistanceCnt])
            resistanceDict[resistanceCnt] = localMax.PolynomialPrice;
          else if (!resistanceDict.ContainsKey(resistanceCnt))
            resistanceDict.Add(resistanceCnt, localMax.PolynomialPrice);
        }

        var supportDict = new Dictionary<int, decimal>();
        var localMins = items.Where(s => s.IsPolynomialMin).ToList();
        foreach (var localMin in localMins)
        {
          var supportCnt = localMins
            .Where(m =>
              NumberUtils.PctChange(m.PolynomialPrice, localMin.PolynomialPrice) < req.SupportResistanceProximityPct &&
              m.PolynomialPrice > localMin.PolynomialPrice)
            .Count();
          if (supportDict.ContainsKey(supportCnt) && localMin.PolynomialPrice < supportDict[supportCnt])
            supportDict[supportCnt] = localMin.PolynomialPrice;
          else if (!supportDict.ContainsKey(supportCnt))
            supportDict.Add(supportCnt, localMin.PolynomialPrice);
        }

        var resistances = resistanceDict
          .Where(r => r.Key >= 2)
          .ToList()
          .OrderByDescending(r => r.Key)
          .Take(2)
          .ToList();
        var supports = supportDict
          .Where(s => s.Key >= 2)
          .ToList()
          .OrderByDescending(r => r.Key)
          .Take(2)
          .ToList();
        var resMax = resistanceDict.Max(r => r.Value);
        var resTop1 = resistances.Count > 0 ? resistances[0].Value : resMax;
        var resTop2 = resistances.Count > 1 ? resistances[1].Value : resMax;
        var suppMin = supportDict.Min(r => r.Value);
        var suppTop1 = supports.Count > 0 ? supports[0].Value : suppMin;
        var suppTop2 = supports.Count > 1 ? supports[1].Value : suppMin;
        foreach (var item in items)
        {
          item.ResistancePriceTop1 = resTop1;
          item.ResistancePriceTop2 = resTop2;
          item.SupportPriceTop1 = suppTop1;
          item.SupportPriceTop2 = suppTop2;
        }

        #endregion

        ret.Add(items);

        spPerSymbol.Stop();
        LogUtils.Debug($"END-{spPerSymbol.Elapsed}");


        if (ret.Count >= 50)
        {
          return ret;
        }

      }

      return ret;
    }
  }
}
