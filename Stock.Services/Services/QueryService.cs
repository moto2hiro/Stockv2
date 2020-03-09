using Stock.Services.Models;
using Stock.Services.Models.EF;
using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stock.Services.Services
{
  public interface IQueryService
  {
    List<ChartImage> GetRandomCharts(int version);
    PastAvgResp GetPastAvgs(int version);
    PastAvgResp GetPastAvgs(int version, string symbol);
    PastAvgResp GetTopPastAvgs(int take);
    List<ChartImage> GetPredictions(DateTime dateFrom);
  }

  public class QueryService : BaseService, IQueryService
  {
    public List<ChartImage> GetRandomCharts(int version)
    {
      var query = DB.ChartImage.Where(c => c.Version == version);
      query = query.Where(c => c.Symbol != Consts.SYMBOL_DOW_ETF && c.Symbol != Consts.SYMBOL_DOW_JONES);
      var randomIndex = new Random().Next(0, query.Count() - 1);
      return query.Skip(randomIndex).Take(100).ToList();
    }

    public PastAvgResp GetPastAvgs(int version)
    {
      return GetPastAvgs(version, "");
    }

    public PastAvgResp GetPastAvgs(int version, string symbol)
    {
      var ret = new PastAvgResp() { };
      var pastAvgs = new List<PastAvg>();
      foreach (var period in Consts.AVG_PERIODS)
      {
        foreach (var avgType in Consts.AVG_TYPES)
        {
          var date = DateTime.Now.AddDays(-1 * period);
          var query = DB.ChartImage.AsQueryable();
          query = query.Where(c => c.YActual != null && c.YPredicted != null);
          query = query.Where(c => c.PriceDate > date);
          query = query.Where(c => c.Version == version);
          if (!string.IsNullOrEmpty(symbol))
          {
            query = query.Where(c => c.Symbol == symbol);
          }
          var noCorrect = 0;
          var noIncorrect = 0;
          if (avgType == Consts.AVG_TYPE_UP)
          {
            query = query.Where(c => c.YActual.Value == Consts.CLASS_UP);
          }
          else if (avgType == Consts.AVG_TYPE_DOWN)
          {
            query = query.Where(c => c.YActual.Value == Consts.CLASS_DOWN);
          }
          noCorrect = query.Where(c => c.YActual.Value == c.YPredicted.Value).Count();
          noIncorrect = query.Where(c => c.YActual.Value != c.YPredicted.Value).Count();
          pastAvgs.Add(new PastAvg()
          {
            AvgType = avgType,
            Period = period,
            NoCorrect = noCorrect,
            NoIncorrect = noIncorrect
          });
        }
      }
      ret.PastAvgs = pastAvgs;
      return ret;
    }

    public PastAvgResp GetTopPastAvgs(int take)
    {
      return null;
    }

    public List<ChartImage> GetPredictions(DateTime dateFrom)
    {
      return DB.ChartImage
        .Where(c => c.PriceDate >= dateFrom && c.YPredicted != null)
        .Select(c => new ChartImage()
        {
          Symbol = c.Symbol,
          PriceDate = c.PriceDate,
          YPredicted = c.YPredicted,
          YPredictedProbability = c.YPredictedProbability
        }).ToList();
    }


    public void Test()
    {
      var items = DB
        .TechnicalItem
        .OrderBy(t => t.Symbol)
        .ThenBy(t => t.PriceDate).ToList();

      // RSI10
      var cntCurrentRSI10 = 0;
      var cntRSI10_70 = 0;
      var cntRSI10_68to70 = 0;
      var cntRSI10_65to70 = 0;
      var cntRSI10_65to68 = 0;
      var cntRSI10_60to70 = 0;
      var cntRSI10_60to65 = 0;
      var cntRSI10_40to60 = 0;
      var cntRSI10_40to30 = 0;
      var cntRSI10_40to35 = 0;
      var cntRSI10_35to30 = 0;
      var cntRSI10_33to30 = 0;
      var cntRSI10_30 = 0;

      // RSI14
      var cntCurrentRSI14 = 0;
      var cntRSI14_70 = 0;
      var cntRSI14_68to70 = 0;
      var cntRSI14_65to70 = 0;
      var cntRSI14_65to68 = 0;
      var cntRSI14_60to70 = 0;
      var cntRSI14_60to65 = 0;
      var cntRSI14_40to60 = 0;
      var cntRSI14_40to30 = 0;
      var cntRSI14_40to35 = 0;
      var cntRSI14_35to30 = 0;
      var cntRSI14_33to30 = 0;
      var cntRSI14_30 = 0;

      // SMA
      var cntSMADiff_a = 0;
      var cntSMADiff_b = 0;
      var cntIsSMACross = 0;

      // EMA
      var cntIsEMACross5_10 = 0;
      var cntIsEMACrossMACD = 0;

      // IsLocalMax/IsLocalMin
      var cntIsLocalMax50 = 0;
      var cntIsLocalMin50 = 0;
      var cntCurrentIsLocalMax50 = 0;
      var cntCurrentIsLocalMin50 = 0;

      // Bollinger
      var cntBollAboveUpperStd1 = 0;
      var cntBollAboveUpperStd2 = 0;
      var cntBollBelowLowerStd1 = 0;
      var cntBollBelowLowerStd2 = 0;
      var cntBollBetStd1Std1 = 0;

      var index = 0;
      foreach (var item in items)
      {
        var nextIndex = index + 1;
        var isPrev = item.LogType == "previous";
        if (isPrev)
        {
          #region RSI10
          if (item.Rsi10 >= 70) cntRSI10_70 += 1;
          if (item.Rsi10 >= 68 && item.Rsi10 < 70) cntRSI10_68to70 += 1;
          if (item.Rsi10 >= 65 && item.Rsi10 < 70) cntRSI10_65to70 += 1;
          if (item.Rsi10 >= 65 && item.Rsi10 <= 68) cntRSI10_65to68 += 1;
          if (item.Rsi10 >= 60 && item.Rsi10 < 70) cntRSI10_60to70 += 1;
          if (item.Rsi10 >= 60 && item.Rsi10 <= 65) cntRSI10_60to65 += 1;
          if (item.Rsi10 > 40 && item.Rsi10 < 60) cntRSI10_40to60 += 1;
          if (item.Rsi10 >= 30 && item.Rsi10 <= 40) cntRSI10_40to30 += 1;
          if (item.Rsi10 >= 35 && item.Rsi10 <= 40) cntRSI10_40to35 += 1;
          if (item.Rsi10 > 30 && item.Rsi10 <= 35) cntRSI10_35to30 += 1;
          if (item.Rsi10 > 30 && item.Rsi10 <= 33) cntRSI10_33to30 += 1;
          if (item.Rsi10 <= 30) cntRSI10_30 += 1;
          #endregion

          #region RSI14
          if (item.Rsi14 >= 70) cntRSI14_70 += 1;
          if (item.Rsi14 >= 68 && item.Rsi14 < 70) cntRSI14_68to70 += 1;
          if (item.Rsi14 >= 65 && item.Rsi14 < 70) cntRSI14_65to70 += 1;
          if (item.Rsi14 >= 65 && item.Rsi14 <= 68) cntRSI14_65to68 += 1;
          if (item.Rsi14 >= 60 && item.Rsi14 < 70) cntRSI14_60to70 += 1;
          if (item.Rsi14 >= 60 && item.Rsi14 <= 65) cntRSI14_60to65 += 1;
          if (item.Rsi14 > 40 && item.Rsi14 < 60) cntRSI14_40to60 += 1;
          if (item.Rsi14 >= 30 && item.Rsi14 <= 40) cntRSI14_40to30 += 1;
          if (item.Rsi14 >= 35 && item.Rsi14 <= 40) cntRSI14_40to35 += 1;
          if (item.Rsi14 > 30 && item.Rsi14 <= 35) cntRSI14_35to30 += 1;
          if (item.Rsi14 > 30 && item.Rsi14 <= 33) cntRSI14_33to30 += 1;
          if (item.Rsi14 <= 30) cntRSI14_30 += 1;
          #endregion

          #region  IsLocalMax/IsLocalMin
          if (item.IsLocalMax50) cntIsLocalMax50 += 1;
          if (item.IsLocalMin50) cntIsLocalMin50 += 1;
          #endregion

          #region Bollinger
          if (item.ClosePrice > item.BollingerUpperStvDev120) cntBollAboveUpperStd1 += 1;
          if (item.ClosePrice > item.BollingerUpperStvDev220) cntBollAboveUpperStd2 += 1;
          if (item.ClosePrice < item.BollingerLowerStvDev120) cntBollBelowLowerStd1 += 1;
          if (item.ClosePrice < item.BollingerLowerStvDev220) cntBollBelowLowerStd2 += 1;
          if (item.ClosePrice < item.BollingerUpperStvDev120 && item.ClosePrice > item.BollingerLowerStvDev120) cntBollBetStd1Std1 += 1;
          #endregion

          #region SMA/EMA
          var isSmaCross = false;
          var isEmaCross5_10 = false;
          var isEmaCrossMACD = false;
          if (nextIndex < items.Count - 1)
          {
            // SMA
            var isSma50CrossAbove = item.Sma50 < item.Sma200 && items[nextIndex].Sma50 > items[nextIndex].Sma200;
            var isSma50CrossBelow = item.Sma50 > item.Sma200 && items[nextIndex].Sma50 < items[nextIndex].Sma200;
            if (isSma50CrossAbove || isSma50CrossBelow)
            {
              isSmaCross = true;
              cntIsSMACross += 1;
            }

            // EMA
            var isEma5CrossAbove = item.Ema5 < item.Ema10 && items[nextIndex].Ema5 > items[nextIndex].Ema10;
            var isEma5CrossBelow = item.Ema5 > item.Ema10 && items[nextIndex].Ema5 < items[nextIndex].Ema10;
            if (isSma50CrossAbove || isSma50CrossBelow)
            {
              isEmaCross5_10 = true;
              cntIsEMACross5_10 += 1;
            }

            if (isSmaCross)
            {
              var closePct = item.ClosePrice * 0.005m;
              var smaDiff = Math.Abs(item.Sma200 - item.Sma50);
              if (smaDiff > closePct)
                cntSMADiff_a += 1;
              else if (smaDiff < closePct)
                cntSMADiff_b += 1;
              LogUtils.Debug($"{NumberUtils.Round((decimal)smaDiff / item.ClosePrice * 100)}");
            }
          }
          #endregion
        }
        else
        {
          #region RSI
          var isRSI10 = item.Rsi10 >= 70 || item.Rsi10 <= 30;
          var isRSI14 = item.Rsi14 >= 70 || item.Rsi14 <= 30;
          if (isRSI10) cntCurrentRSI10 += 1;
          if (isRSI14) cntCurrentRSI14 += 1;
          #endregion

          #region IsLocalMax/IsLocalMin
          if (item.IsLocalMax50) cntCurrentIsLocalMax50 += 1;
          if (item.IsLocalMin50) cntCurrentIsLocalMin50 += 1;
          #endregion
        }

        index += 1;
      }
      var total = NumberUtils.Round(items.Count / 2, 0);
      LogUtils.Debug($"Total Count={total}");

      // RSI10 = (65, 70) or (30, 35)
      LogUtils.Debug($"");
      LogUtils.Debug($"cntCurrentRSI10 {Pct(cntCurrentRSI10, total)}");
      LogUtils.Debug($"cntRSI10_70 {Pct(cntRSI10_70, total)}");
      LogUtils.Debug($"cntRSI10_68to70 {Pct(cntRSI10_68to70, total)}");
      LogUtils.Debug($"cntRSI10_65to70 {Pct(cntRSI10_65to70, total)}");
      LogUtils.Debug($"cntRSI10_65to68 {Pct(cntRSI10_65to68, total)}");
      LogUtils.Debug($"cntRSI10_60to70 {Pct(cntRSI10_60to70, total)}");
      LogUtils.Debug($"cntRSI10_60to65 {Pct(cntRSI10_60to65, total)}");
      LogUtils.Debug($"cntRSI10_40to60 {Pct(cntRSI10_40to60, total)}");
      LogUtils.Debug($"cntRSI10_40to30 {Pct(cntRSI10_40to30, total)}");
      LogUtils.Debug($"cntRSI10_40to35 {Pct(cntRSI10_40to35, total)}");
      LogUtils.Debug($"cntRSI10_35to30 {Pct(cntRSI10_35to30, total)}");
      LogUtils.Debug($"cntRSI10_33to30 {Pct(cntRSI10_33to30, total)}");
      LogUtils.Debug($"cntRSI10_30 {Pct(cntRSI10_30, total)}");
      LogUtils.Debug($"");

      // RSI14 was (60, 65] or [35, 40), but is negligible.
      LogUtils.Debug($"cntCurrentRSI14 {Pct(cntCurrentRSI14, total)}");
      LogUtils.Debug($"cntRSI14_70 {Pct(cntRSI14_70, total)}");
      LogUtils.Debug($"cntRSI14_68to70 {Pct(cntRSI14_68to70, total)}");
      LogUtils.Debug($"cntRSI14_65to70 {Pct(cntRSI14_65to70, total)}");
      LogUtils.Debug($"cntRSI14_65to68 {Pct(cntRSI14_65to68, total)}");
      LogUtils.Debug($"cntRSI14_60to70 {Pct(cntRSI14_60to70, total)}");
      LogUtils.Debug($"cntRSI14_60to65 {Pct(cntRSI14_60to65, total)}");
      LogUtils.Debug($"cntRSI14_40to60 {Pct(cntRSI14_40to60, total)}");
      LogUtils.Debug($"cntRSI14_40to30 {Pct(cntRSI14_40to30, total)}");
      LogUtils.Debug($"cntRSI14_40to35 {Pct(cntRSI14_40to35, total)}");
      LogUtils.Debug($"cntRSI14_35to30 {Pct(cntRSI14_35to30, total)}");
      LogUtils.Debug($"cntRSI14_33to30 {Pct(cntRSI14_33to30, total)}");
      LogUtils.Debug($"cntRSI14_30 {Pct(cntRSI14_30, total)}");
      LogUtils.Debug($"");

      // SMA = SMA Cross is Negligible
      LogUtils.Debug($"cntIsSMACross {Pct(cntIsSMACross, total)}");
      LogUtils.Debug($"cntSMADiff_a {cntSMADiff_a}");
      LogUtils.Debug($"cntSMADiff_b {cntSMADiff_b}");
      LogUtils.Debug($"cntIsSMACross {cntIsSMACross}");
      LogUtils.Debug($"");

      // EMA
      LogUtils.Debug($"cntIsEMACross5_10 {Pct(cntIsEMACross5_10, total)}");
      LogUtils.Debug($"");

      // IsLocalMax/IsLocalMin = Negligible
      LogUtils.Debug($"cntIsLocalMax50 {Pct(cntIsLocalMax50, total)}");
      LogUtils.Debug($"cntIsLocalMin50 {Pct(cntIsLocalMin50, total)}");
      LogUtils.Debug($"");
      LogUtils.Debug($"cntCurrentIsLocalMax50 {Pct(cntCurrentIsLocalMax50, total)}");
      LogUtils.Debug($"cntCurrentIsLocalMin50 {Pct(cntCurrentIsLocalMin50, total)}");
      LogUtils.Debug($"");

      // Bollinger = Above Std1 or Below Std1
      LogUtils.Debug($"cntBollAboveUpperStd1 {Pct(cntBollAboveUpperStd1, total)}");
      LogUtils.Debug($"cntBollAboveUpperStd2 {Pct(cntBollAboveUpperStd2, total)}");
      LogUtils.Debug($"cntBollBelowLowerStd1 {Pct(cntBollBelowLowerStd1, total)}");
      LogUtils.Debug($"cntBollBelowLowerStd2 {Pct(cntBollBelowLowerStd2, total)}");
      LogUtils.Debug($"cntBollBetStd1Std1 {Pct(cntBollBetStd1Std1, total)}");
      LogUtils.Debug($"");
    }

    public decimal Pct(decimal cnt, decimal total)
    {
      return Math.Round((cnt / total) * 100, 2);
    }
  }
}
