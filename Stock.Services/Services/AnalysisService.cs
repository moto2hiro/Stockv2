using Stock.Services.Models;
using Stock.Services.Models.EF;
using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stock.Services.Services
{
  public interface IAnalysisService
  {
    int GetTechnicals();
    int GetYActuals();
    //void Transform();
    //void Analyze();
  }

  public class AnalysisService : BaseService, IAnalysisService
  {
    public int GetTechnicals()
    {
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
          var currIdx = item.idx;
          var nextIdx = item.idx + 1;
          var prevIdx = item.idx - 1;
          if (items.ElementAtOrDefault(currIdx - Consts.SMA_PERIODS.Max()) == null)
          {
            continue;
          }
          if (items.ElementAtOrDefault(currIdx - Consts.EMA_PERIODS.Max()) == null)
          {
            continue;
          }
          if (items.ElementAtOrDefault(currIdx - Consts.RSI_PERIODS.Max()) == null)
          {
            continue;
          }
          if (items.ElementAtOrDefault(currIdx - Consts.ADTV_PERIODS.Max()) == null)
          {
            continue;
          }
          if (items.ElementAtOrDefault(currIdx - Consts.ADPV_PERIODS.Max()) == null)
          {
            continue;
          }





        }
        ret += Insert<Technical>(models);
      }
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
        //ret += Insert<Yactual>(models);
      }
      return ret;
    }

    //public void Transform()
    //{
    //  var symbols = DB.SymbolMaster.OrderBy(s => s.Symbol).ToList();
    //  foreach (var symbolMaster in symbols)
    //  {
    //    var financials = DB.Financial.Where(f =>
    //      f.Symbol == symbolMaster.Symbol &&
    //      f.FreeCashFlow != null).ToList();
    //    if (financials == null || financials.Count == 0)
    //    {
    //      continue;
    //    }

    //    var models = new List<Analysis>();
    //    var symbol = symbolMaster.Symbol;
    //    var items = DB.StockPrice.Where(s => s.Symbol == symbol).OrderBy(s => s.PriceDate).ToList();
    //    var itemCount = items.Count();
    //    foreach (var item in items.Select((model, idx) => (model, idx)))
    //    {
    //      var currIdx = item.idx;
    //      var prevIdx = currIdx - 1;
    //      var nextIdx = currIdx + 1;
    //      // Need at least 201 days for calculation
    //      if (currIdx < Consts.MAX_CALC_PERIOD)
    //      {
    //        continue;
    //      }
    //      // Need at least 60 days of future
    //      if (currIdx > itemCount - Consts.MAX_FUTURE_PERIOD - 1)
    //      {
    //        continue;
    //      }
    //      var model = new Analysis();
    //      model.Symbol = item.model.Symbol;
    //      model.PriceDate = item.model.PriceDate;
    //      model.OpenPrice = item.model.OpenPrice;
    //      model.HighPrice = item.model.HighPrice;
    //      model.LowPrice = item.model.LowPrice;
    //      model.ClosePrice = item.model.ClosePrice;
    //      model.Volume = item.model.Volume;
    //      model.PriceVolume = item.model.PriceVolume;

    //      #region Declarations
    //      var sumClosePrice10 = 0m;
    //      var sumClosePrice20 = 0m;
    //      var sumClosePrice50 = 0m;
    //      var sumClosePrice100 = 0m;
    //      var sumClosePrice200 = 0m;
    //      var sumVolume20 = 0m;
    //      var sumVolume30 = 0m;
    //      var sumVolume50 = 0m;
    //      var sumPriceVolume20 = 0m;
    //      var sumPriceVolume30 = 0m;
    //      var sumPriceVolume50 = 0m;
    //      var sumClosePriceGain6 = 0m;
    //      var sumClosePriceGain10 = 0m;
    //      var sumClosePriceGain14 = 0m;
    //      var sumClosePriceLoss6 = 0m;
    //      var sumClosePriceLoss10 = 0m;
    //      var sumClosePriceLoss14 = 0m;
    //      #endregion

    //      #region Loop past data for metrics
    //      // Assume Calculation is done at Close; thus inclusive.
    //      var startIdx = currIdx - Consts.MAX_CALC_PERIOD + 1;
    //      for (var i = startIdx; i <= currIdx; i++)
    //      {
    //        #region SMA
    //        sumClosePrice200 += items[i].ClosePrice;
    //        if (i > currIdx - Consts.SMA_PERIOD_10)
    //        {
    //          sumClosePrice10 += items[i].ClosePrice;
    //        }
    //        if (i > currIdx - Consts.SMA_PERIOD_20)
    //        {
    //          sumClosePrice20 += items[i].ClosePrice;
    //        }
    //        if (i > currIdx - Consts.SMA_PERIOD_50)
    //        {
    //          sumClosePrice50 += items[i].ClosePrice;
    //        }
    //        if (i > currIdx - Consts.SMA_PERIOD_100)
    //        {
    //          sumClosePrice100 += items[i].ClosePrice;
    //        }
    //        #endregion

    //        #region ADTV, ADPV
    //        if (i > currIdx - Consts.ADTV_PERIOD_20)
    //        {
    //          sumVolume20 += items[i].Volume;
    //        }
    //        if (i > currIdx - Consts.ADTV_PERIOD_30)
    //        {
    //          sumVolume30 += items[i].Volume;
    //        }
    //        if (i > currIdx - Consts.ADTV_PERIOD_50)
    //        {
    //          sumVolume50 += items[i].Volume;
    //        }
    //        if (i > currIdx - Consts.ADPV_PERIOD_20)
    //        {
    //          sumPriceVolume20 += items[i].PriceVolume;
    //        }
    //        if (i > currIdx - Consts.ADPV_PERIOD_30)
    //        {
    //          sumPriceVolume30 += items[i].PriceVolume;
    //        }
    //        if (i > currIdx - Consts.ADPV_PERIOD_50)
    //        {
    //          sumPriceVolume50 += items[i].PriceVolume;
    //        }
    //        #endregion

    //        #region RSI
    //        var diff = items[i].ClosePrice - items[i - 1].ClosePrice;
    //        if (i > currIdx - Consts.RSI_PERIOD_6)
    //        {
    //          if (diff > 0)
    //            sumClosePriceGain6 += diff;
    //          else if (diff < 0)
    //            sumClosePriceLoss6 += Math.Abs(diff);
    //        }
    //        if (i > currIdx - Consts.RSI_PERIOD_10)
    //        {
    //          if (diff > 0)
    //            sumClosePriceGain10 += diff;
    //          else if (diff < 0)
    //            sumClosePriceLoss10 += Math.Abs(diff);
    //        }
    //        if (i > currIdx - Consts.RSI_PERIOD_14)
    //        {
    //          if (diff > 0)
    //            sumClosePriceGain14 += diff;
    //          else if (diff < 0)
    //            sumClosePriceLoss14 += Math.Abs(diff);
    //        }
    //        #endregion
    //      }
    //      #endregion

    //      #region Calculate SMA
    //      item.model.SMA_10 = NumberUtils.Round(sumClosePrice10 / Consts.SMA_PERIOD_10);
    //      item.model.SMA_20 = NumberUtils.Round(sumClosePrice20 / Consts.SMA_PERIOD_20);
    //      item.model.SMA_50 = NumberUtils.Round(sumClosePrice50 / Consts.SMA_PERIOD_50);
    //      item.model.SMA_100 = NumberUtils.Round(sumClosePrice100 / Consts.SMA_PERIOD_100);
    //      item.model.SMA_200 = NumberUtils.Round(sumClosePrice200 / Consts.SMA_PERIOD_200);
    //      model.Sma10 = item.model.SMA_10;
    //      model.Sma20 = item.model.SMA_20;
    //      model.Sma50 = item.model.SMA_50;
    //      model.Sma100 = item.model.SMA_100;
    //      model.Sma200 = item.model.SMA_200;
    //      model.HasSma10crossAbove20 = items[prevIdx].SMA_10 < items[prevIdx].SMA_20 && items[currIdx].SMA_10 > items[currIdx].SMA_20;
    //      model.HasSma10crossBelow20 = items[prevIdx].SMA_10 > items[prevIdx].SMA_20 && items[currIdx].SMA_10 < items[currIdx].SMA_20;
    //      model.HasSma10crossAbove50 = items[prevIdx].SMA_10 < items[prevIdx].SMA_50 && items[currIdx].SMA_10 > items[currIdx].SMA_50;
    //      model.HasSma10crossBelow50 = items[prevIdx].SMA_10 > items[prevIdx].SMA_50 && items[currIdx].SMA_10 < items[currIdx].SMA_50;
    //      model.HasSma10crossAbove100 = items[prevIdx].SMA_10 < items[prevIdx].SMA_100 && items[currIdx].SMA_10 > items[currIdx].SMA_100;
    //      model.HasSma10crossBelow100 = items[prevIdx].SMA_10 > items[prevIdx].SMA_100 && items[currIdx].SMA_10 < items[currIdx].SMA_100;
    //      model.HasSma10crossAbove200 = items[prevIdx].SMA_10 < items[prevIdx].SMA_200 && items[currIdx].SMA_10 > items[currIdx].SMA_200;
    //      model.HasSma10crossBelow200 = items[prevIdx].SMA_10 > items[prevIdx].SMA_200 && items[currIdx].SMA_10 < items[currIdx].SMA_200;
    //      model.HasSma20crossAbove50 = items[prevIdx].SMA_20 < items[prevIdx].SMA_50 && items[currIdx].SMA_20 > items[currIdx].SMA_50;
    //      model.HasSma20crossBelow50 = items[prevIdx].SMA_20 > items[prevIdx].SMA_50 && items[currIdx].SMA_20 < items[currIdx].SMA_50;
    //      model.HasSma20crossAbove100 = items[prevIdx].SMA_20 < items[prevIdx].SMA_100 && items[currIdx].SMA_20 > items[currIdx].SMA_100;
    //      model.HasSma20crossBelow100 = items[prevIdx].SMA_20 > items[prevIdx].SMA_100 && items[currIdx].SMA_20 < items[currIdx].SMA_100;
    //      model.HasSma20crossAbove200 = items[prevIdx].SMA_20 < items[prevIdx].SMA_200 && items[currIdx].SMA_20 > items[currIdx].SMA_200;
    //      model.HasSma20crossBelow200 = items[prevIdx].SMA_20 > items[prevIdx].SMA_200 && items[currIdx].SMA_20 < items[currIdx].SMA_200;
    //      model.HasSma50crossAbove100 = items[prevIdx].SMA_50 < items[prevIdx].SMA_100 && items[currIdx].SMA_50 > items[currIdx].SMA_100;
    //      model.HasSma50crossBelow100 = items[prevIdx].SMA_50 > items[prevIdx].SMA_100 && items[currIdx].SMA_50 < items[currIdx].SMA_100;
    //      model.HasSma50crossAbove200 = items[prevIdx].SMA_50 < items[prevIdx].SMA_200 && items[currIdx].SMA_50 > items[currIdx].SMA_200;
    //      model.HasSma50crossBelow200 = items[prevIdx].SMA_50 > items[prevIdx].SMA_200 && items[currIdx].SMA_50 < items[currIdx].SMA_200;
    //      model.HasSma100crossAbove200 = items[prevIdx].SMA_100 < items[prevIdx].SMA_200 && items[currIdx].SMA_100 > items[currIdx].SMA_200;
    //      model.HasSma100crossBelow200 = items[prevIdx].SMA_100 > items[prevIdx].SMA_200 && items[currIdx].SMA_100 < items[currIdx].SMA_200;
    //      #endregion

    //      #region Calculate EMA
    //      // EMA for t1 = Price of t1
    //      // EMA for > t1 = Alpha * Price + (1 - Alpha) * Previous EMA
    //      // where Alpha = 2 / (N + 1)
    //      var alpha5 = 2m / (Consts.EMA_PERIOD_5 + 1);
    //      var alpha9 = 2m / (Consts.EMA_PERIOD_9 + 1);
    //      var alpha10 = 2m / (Consts.EMA_PERIOD_10 + 1);
    //      var alpha12 = 2m / (Consts.EMA_PERIOD_12 + 1);
    //      var alpha13 = 2m / (Consts.EMA_PERIOD_13 + 1);
    //      var alpha26 = 2m / (Consts.EMA_PERIOD_26 + 1);
    //      var alpha48 = 2m / (Consts.EMA_PERIOD_48 + 1);
    //      var prevEma5 = (items[prevIdx].EMA_5 > 0) ? items[prevIdx].EMA_5 : items[currIdx].ClosePrice;
    //      var prevEma9 = (items[prevIdx].EMA_9 > 0) ? items[prevIdx].EMA_9 : items[currIdx].ClosePrice;
    //      var prevEma10 = (items[prevIdx].EMA_10 > 0) ? items[prevIdx].EMA_10 : items[currIdx].ClosePrice;
    //      var prevEma12 = (items[prevIdx].EMA_12 > 0) ? items[prevIdx].EMA_12 : items[currIdx].ClosePrice;
    //      var prevEma13 = (items[prevIdx].EMA_13 > 0) ? items[prevIdx].EMA_13 : items[currIdx].ClosePrice;
    //      var prevEma26 = (items[prevIdx].EMA_26 > 0) ? items[prevIdx].EMA_26 : items[currIdx].ClosePrice;
    //      var prevEma48 = (items[prevIdx].EMA_48 > 0) ? items[prevIdx].EMA_48 : items[currIdx].ClosePrice;
    //      item.model.EMA_5 = NumberUtils.Round((alpha5 * item.model.ClosePrice) + ((1 - alpha5) * prevEma5));
    //      item.model.EMA_9 = NumberUtils.Round((alpha9 * item.model.ClosePrice) + ((1 - alpha9) * prevEma9));
    //      item.model.EMA_10 = NumberUtils.Round((alpha10 * item.model.ClosePrice) + ((1 - alpha10) * prevEma10));
    //      item.model.EMA_12 = NumberUtils.Round((alpha12 * item.model.ClosePrice) + ((1 - alpha12) * prevEma12));
    //      item.model.EMA_13 = NumberUtils.Round((alpha13 * item.model.ClosePrice) + ((1 - alpha13) * prevEma13));
    //      item.model.EMA_26 = NumberUtils.Round((alpha26 * item.model.ClosePrice) + ((1 - alpha26) * prevEma26));
    //      item.model.EMA_48 = NumberUtils.Round((alpha48 * item.model.ClosePrice) + ((1 - alpha48) * prevEma48));
    //      model.Ema5 = item.model.EMA_5;
    //      model.Ema9 = item.model.EMA_9;
    //      model.Ema10 = item.model.EMA_10;
    //      model.Ema12 = item.model.EMA_12;
    //      model.Ema13 = item.model.EMA_13;
    //      model.Ema26 = item.model.EMA_26;
    //      model.Ema48 = item.model.EMA_48;
    //      model.HasEma5crossAbove9 = items[prevIdx].EMA_5 < items[prevIdx].EMA_9 && items[currIdx].EMA_5 > items[currIdx].EMA_9;
    //      model.HasEma5crossBelow9 = items[prevIdx].EMA_5 > items[prevIdx].EMA_9 && items[currIdx].EMA_5 < items[currIdx].EMA_9;
    //      model.HasEma5crossAbove10 = items[prevIdx].EMA_5 < items[prevIdx].EMA_10 && items[currIdx].EMA_5 > items[currIdx].EMA_10;
    //      model.HasEma5crossBelow10 = items[prevIdx].EMA_5 > items[prevIdx].EMA_10 && items[currIdx].EMA_5 < items[currIdx].EMA_10;
    //      model.HasEma5crossAbove12 = items[prevIdx].EMA_5 < items[prevIdx].EMA_12 && items[currIdx].EMA_5 > items[currIdx].EMA_12;
    //      model.HasEma5crossBelow12 = items[prevIdx].EMA_5 > items[prevIdx].EMA_12 && items[currIdx].EMA_5 < items[currIdx].EMA_12;
    //      model.HasEma5crossAbove13 = items[prevIdx].EMA_5 < items[prevIdx].EMA_13 && items[currIdx].EMA_5 > items[currIdx].EMA_13;
    //      model.HasEma5crossBelow13 = items[prevIdx].EMA_5 > items[prevIdx].EMA_13 && items[currIdx].EMA_5 < items[currIdx].EMA_13;
    //      model.HasEma5crossAbove26 = items[prevIdx].EMA_5 < items[prevIdx].EMA_26 && items[currIdx].EMA_5 > items[currIdx].EMA_26;
    //      model.HasEma5crossBelow26 = items[prevIdx].EMA_5 > items[prevIdx].EMA_26 && items[currIdx].EMA_5 < items[currIdx].EMA_26;
    //      model.HasEma5crossAbove48 = items[prevIdx].EMA_5 < items[prevIdx].EMA_48 && items[currIdx].EMA_5 > items[currIdx].EMA_48;
    //      model.HasEma5crossBelow48 = items[prevIdx].EMA_5 > items[prevIdx].EMA_48 && items[currIdx].EMA_5 < items[currIdx].EMA_48;
    //      model.HasEma9crossAbove10 = items[prevIdx].EMA_9 < items[prevIdx].EMA_10 && items[currIdx].EMA_9 > items[currIdx].EMA_10;
    //      model.HasEma9crossBelow10 = items[prevIdx].EMA_9 > items[prevIdx].EMA_10 && items[currIdx].EMA_9 < items[currIdx].EMA_10;
    //      model.HasEma9crossAbove12 = items[prevIdx].EMA_9 < items[prevIdx].EMA_12 && items[currIdx].EMA_9 > items[currIdx].EMA_12;
    //      model.HasEma9crossBelow12 = items[prevIdx].EMA_9 > items[prevIdx].EMA_12 && items[currIdx].EMA_9 < items[currIdx].EMA_12;
    //      model.HasEma9crossAbove13 = items[prevIdx].EMA_9 < items[prevIdx].EMA_13 && items[currIdx].EMA_9 > items[currIdx].EMA_13;
    //      model.HasEma9crossBelow13 = items[prevIdx].EMA_9 > items[prevIdx].EMA_13 && items[currIdx].EMA_9 < items[currIdx].EMA_13;
    //      model.HasEma9crossAbove26 = items[prevIdx].EMA_9 < items[prevIdx].EMA_26 && items[currIdx].EMA_9 > items[currIdx].EMA_26;
    //      model.HasEma9crossBelow26 = items[prevIdx].EMA_9 > items[prevIdx].EMA_26 && items[currIdx].EMA_9 < items[currIdx].EMA_26;
    //      model.HasEma9crossAbove48 = items[prevIdx].EMA_9 < items[prevIdx].EMA_48 && items[currIdx].EMA_9 > items[currIdx].EMA_48;
    //      model.HasEma9crossBelow48 = items[prevIdx].EMA_9 > items[prevIdx].EMA_48 && items[currIdx].EMA_9 < items[currIdx].EMA_48;
    //      model.HasEma10crossAbove12 = items[prevIdx].EMA_10 < items[prevIdx].EMA_12 && items[currIdx].EMA_10 > items[currIdx].EMA_12;
    //      model.HasEma10crossBelow12 = items[prevIdx].EMA_10 > items[prevIdx].EMA_12 && items[currIdx].EMA_10 < items[currIdx].EMA_12;
    //      model.HasEma10crossAbove13 = items[prevIdx].EMA_10 < items[prevIdx].EMA_13 && items[currIdx].EMA_10 > items[currIdx].EMA_13;
    //      model.HasEma10crossBelow13 = items[prevIdx].EMA_10 > items[prevIdx].EMA_13 && items[currIdx].EMA_10 < items[currIdx].EMA_13;
    //      model.HasEma10crossAbove26 = items[prevIdx].EMA_10 < items[prevIdx].EMA_26 && items[currIdx].EMA_10 > items[currIdx].EMA_26;
    //      model.HasEma10crossBelow26 = items[prevIdx].EMA_10 > items[prevIdx].EMA_26 && items[currIdx].EMA_10 < items[currIdx].EMA_26;
    //      model.HasEma10crossAbove48 = items[prevIdx].EMA_10 < items[prevIdx].EMA_48 && items[currIdx].EMA_10 > items[currIdx].EMA_48;
    //      model.HasEma10crossBelow48 = items[prevIdx].EMA_10 > items[prevIdx].EMA_48 && items[currIdx].EMA_10 < items[currIdx].EMA_48;
    //      model.HasEma12crossAbove13 = items[prevIdx].EMA_12 < items[prevIdx].EMA_13 && items[currIdx].EMA_12 > items[currIdx].EMA_13;
    //      model.HasEma12crossBelow13 = items[prevIdx].EMA_12 > items[prevIdx].EMA_13 && items[currIdx].EMA_12 < items[currIdx].EMA_13;
    //      model.HasEma12crossAbove26 = items[prevIdx].EMA_12 < items[prevIdx].EMA_26 && items[currIdx].EMA_12 > items[currIdx].EMA_26;
    //      model.HasEma12crossBelow26 = items[prevIdx].EMA_12 > items[prevIdx].EMA_26 && items[currIdx].EMA_12 < items[currIdx].EMA_26;
    //      model.HasEma12crossAbove48 = items[prevIdx].EMA_12 < items[prevIdx].EMA_48 && items[currIdx].EMA_12 > items[currIdx].EMA_48;
    //      model.HasEma12crossBelow48 = items[prevIdx].EMA_12 > items[prevIdx].EMA_48 && items[currIdx].EMA_12 < items[currIdx].EMA_48;
    //      model.HasEma13crossAbove26 = items[prevIdx].EMA_13 < items[prevIdx].EMA_26 && items[currIdx].EMA_13 > items[currIdx].EMA_26;
    //      model.HasEma13crossBelow26 = items[prevIdx].EMA_13 > items[prevIdx].EMA_26 && items[currIdx].EMA_13 < items[currIdx].EMA_26;
    //      model.HasEma13crossAbove48 = items[prevIdx].EMA_13 < items[prevIdx].EMA_48 && items[currIdx].EMA_13 > items[currIdx].EMA_48;
    //      model.HasEma13crossBelow48 = items[prevIdx].EMA_13 > items[prevIdx].EMA_48 && items[currIdx].EMA_13 < items[currIdx].EMA_48;
    //      model.HasEma26crossAbove48 = items[prevIdx].EMA_26 < items[prevIdx].EMA_48 && items[currIdx].EMA_26 > items[currIdx].EMA_48;
    //      model.HasEma26crossBelow48 = items[prevIdx].EMA_26 > items[prevIdx].EMA_48 && items[currIdx].EMA_26 < items[currIdx].EMA_48;
    //      #endregion

    //      #region Calculate ADTV, ADPV
    //      model.Adtv20 = NumberUtils.Round(sumVolume20 / Consts.ADTV_PERIOD_20);
    //      model.Adtv30 = NumberUtils.Round(sumVolume30 / Consts.ADTV_PERIOD_30);
    //      model.Adtv50 = NumberUtils.Round(sumVolume50 / Consts.ADTV_PERIOD_50);
    //      model.Adtv20 = NumberUtils.Round(sumPriceVolume20 / Consts.ADPV_PERIOD_20);
    //      model.Adtv30 = NumberUtils.Round(sumPriceVolume30 / Consts.ADPV_PERIOD_30);
    //      model.Adtv50 = NumberUtils.Round(sumPriceVolume50 / Consts.ADPV_PERIOD_50);
    //      #endregion

    //      #region Calculate RSI
    //      // RSI = 100 - (100 / (1 + RS))
    //      // RS = AvgGain / AvgLoss
    //      // RS = 0 if AvgLoss = 0
    //      // First AvgGain = Sum / 14
    //      // First AvgLoss = Sum / 14
    //      // Next AvgGain = (Prev AvgGain * 13 + CurrentGain) / 14
    //      // Next AvgLoss = (Prev AvgLoss * 13 + CurrentLoss) / 14
    //      var avgGain6 = sumClosePriceGain6 / Consts.RSI_PERIOD_6;
    //      var avgLoss6 = sumClosePriceLoss6 / Consts.RSI_PERIOD_6;
    //      var avgGain10 = sumClosePriceGain10 / Consts.RSI_PERIOD_10;
    //      var avgLoss10 = sumClosePriceLoss10 / Consts.RSI_PERIOD_10;
    //      var avgGain14 = sumClosePriceGain14 / Consts.RSI_PERIOD_14;
    //      var avgLoss14 = sumClosePriceLoss14 / Consts.RSI_PERIOD_14;
    //      var priceDiff = items[currIdx].ClosePrice - items[prevIdx].ClosePrice;
    //      var currGain = (priceDiff > 0) ? priceDiff : 0;
    //      var currLoss = (priceDiff < 0) ? Math.Abs(priceDiff) : 0;
    //      if (items[prevIdx].AvgGain_6 != null && items[prevIdx].AvgLoss_6 != null)
    //      {
    //        avgGain6 = (items[prevIdx].AvgGain_6.Value * (Consts.RSI_PERIOD_6 - 1) + currGain) / Consts.RSI_PERIOD_6;
    //        avgLoss6 = (items[prevIdx].AvgLoss_6.Value * (Consts.RSI_PERIOD_6 - 1) + currLoss) / Consts.RSI_PERIOD_6;
    //      }
    //      if (items[prevIdx].AvgGain_10 != null && items[prevIdx].AvgLoss_10 != null)
    //      {
    //        avgGain10 = (items[prevIdx].AvgGain_10.Value * (Consts.RSI_PERIOD_10 - 1) + currGain) / Consts.RSI_PERIOD_10;
    //        avgLoss10 = (items[prevIdx].AvgLoss_10.Value * (Consts.RSI_PERIOD_10 - 1) + currLoss) / Consts.RSI_PERIOD_10;
    //      }
    //      if (items[prevIdx].AvgGain_14 != null && items[prevIdx].AvgLoss_14 != null)
    //      {
    //        avgGain14 = (items[prevIdx].AvgGain_14.Value * (Consts.RSI_PERIOD_14 - 1) + currGain) / Consts.RSI_PERIOD_14;
    //        avgLoss14 = (items[prevIdx].AvgLoss_14.Value * (Consts.RSI_PERIOD_14 - 1) + currLoss) / Consts.RSI_PERIOD_14;
    //      }
    //      var rs6 = (avgLoss6 > 0) ? avgGain6 / avgLoss6 : 0;
    //      var rs10 = (avgLoss10 > 0) ? avgGain10 / avgLoss10 : 0;
    //      var rs14 = (avgLoss14 > 0) ? avgGain14 / avgLoss14 : 0;
    //      var rsi6 = NumberUtils.Round(100 - (100 / (1 + rs6)));
    //      var rsi10 = NumberUtils.Round(100 - (100 / (1 + rs10)));
    //      var rsi14 = NumberUtils.Round(100 - (100 / (1 + rs14)));
    //      item.model.AvgGain_6 = avgGain6;
    //      item.model.AvgLoss_6 = avgLoss6;
    //      item.model.AvgGain_10 = avgGain10;
    //      item.model.AvgLoss_10 = avgLoss10;
    //      item.model.AvgGain_14 = avgGain14;
    //      item.model.AvgLoss_14 = avgLoss14;
    //      item.model.RSI_6 = rsi6;
    //      item.model.RSI_10 = rsi10;
    //      item.model.RSI_14 = rsi14;
    //      model.AvgGain6 = item.model.AvgGain_6;
    //      model.AvgLoss6 = item.model.AvgLoss_6;
    //      model.AvgGain10 = item.model.AvgGain_10;
    //      model.AvgLoss10 = item.model.AvgLoss_10;
    //      model.AvgGain14 = item.model.AvgGain_14;
    //      model.AvgLoss14 = item.model.AvgLoss_14;
    //      model.Rsi6 = item.model.RSI_6;
    //      model.Rsi10 = item.model.RSI_10;
    //      model.Rsi14 = item.model.RSI_14;
    //      model.HasRsi6crossAbove70 = items[prevIdx].RSI_6 < Consts.RSI_70 && items[currIdx].RSI_6 > Consts.RSI_70;
    //      model.HasRsi6crossAbove75 = items[prevIdx].RSI_6 < Consts.RSI_75 && items[currIdx].RSI_6 > Consts.RSI_75;
    //      model.HasRsi6crossAbove80 = items[prevIdx].RSI_6 < Consts.RSI_80 && items[currIdx].RSI_6 > Consts.RSI_80;
    //      model.HasRsi6crossAbove85 = items[prevIdx].RSI_6 < Consts.RSI_85 && items[currIdx].RSI_6 > Consts.RSI_85;
    //      model.HasRsi6crossBelow30 = items[prevIdx].RSI_6 > Consts.RSI_30 && items[currIdx].RSI_6 < Consts.RSI_30;
    //      model.HasRsi6crossBelow25 = items[prevIdx].RSI_6 > Consts.RSI_25 && items[currIdx].RSI_6 < Consts.RSI_25;
    //      model.HasRsi6crossBelow20 = items[prevIdx].RSI_6 > Consts.RSI_20 && items[currIdx].RSI_6 < Consts.RSI_20;
    //      model.HasRsi6crossBelow15 = items[prevIdx].RSI_6 > Consts.RSI_15 && items[currIdx].RSI_6 < Consts.RSI_15;
    //      model.HasRsi10crossAbove70 = items[prevIdx].RSI_10 < Consts.RSI_70 && items[currIdx].RSI_10 > Consts.RSI_70;
    //      model.HasRsi10crossAbove75 = items[prevIdx].RSI_10 < Consts.RSI_75 && items[currIdx].RSI_10 > Consts.RSI_75;
    //      model.HasRsi10crossAbove80 = items[prevIdx].RSI_10 < Consts.RSI_80 && items[currIdx].RSI_10 > Consts.RSI_80;
    //      model.HasRsi10crossAbove85 = items[prevIdx].RSI_10 < Consts.RSI_85 && items[currIdx].RSI_10 > Consts.RSI_85;
    //      model.HasRsi10crossBelow30 = items[prevIdx].RSI_10 > Consts.RSI_30 && items[currIdx].RSI_10 < Consts.RSI_30;
    //      model.HasRsi10crossBelow25 = items[prevIdx].RSI_10 > Consts.RSI_25 && items[currIdx].RSI_10 < Consts.RSI_25;
    //      model.HasRsi10crossBelow20 = items[prevIdx].RSI_10 > Consts.RSI_20 && items[currIdx].RSI_10 < Consts.RSI_20;
    //      model.HasRsi10crossBelow15 = items[prevIdx].RSI_10 > Consts.RSI_15 && items[currIdx].RSI_10 < Consts.RSI_15;
    //      model.HasRsi14crossAbove70 = items[prevIdx].RSI_14 < Consts.RSI_70 && items[currIdx].RSI_14 > Consts.RSI_70;
    //      model.HasRsi14crossAbove75 = items[prevIdx].RSI_14 < Consts.RSI_75 && items[currIdx].RSI_14 > Consts.RSI_75;
    //      model.HasRsi14crossAbove80 = items[prevIdx].RSI_14 < Consts.RSI_80 && items[currIdx].RSI_14 > Consts.RSI_80;
    //      model.HasRsi14crossAbove85 = items[prevIdx].RSI_14 < Consts.RSI_85 && items[currIdx].RSI_14 > Consts.RSI_85;
    //      model.HasRsi14crossBelow30 = items[prevIdx].RSI_14 > Consts.RSI_30 && items[currIdx].RSI_14 < Consts.RSI_30;
    //      model.HasRsi14crossBelow25 = items[prevIdx].RSI_14 > Consts.RSI_25 && items[currIdx].RSI_14 < Consts.RSI_25;
    //      model.HasRsi14crossBelow20 = items[prevIdx].RSI_14 > Consts.RSI_20 && items[currIdx].RSI_14 < Consts.RSI_20;
    //      model.HasRsi14crossBelow15 = items[prevIdx].RSI_14 > Consts.RSI_15 && items[currIdx].RSI_14 < Consts.RSI_15;
    //      #endregion

    //      #endregion
    //    }

    //    LogUtils.Debug($"INSERT START={symbol} {models.Count}");
    //    var skip = 0;
    //    while (skip < models.Count)
    //    {
    //      var query = models.Skip(skip).Take(100);
    //      Insert<Analysis>(query.ToList());
    //      skip += 100;
    //      LogUtils.Debug($"Current skip = {skip}");
    //    }
    //    LogUtils.Debug($"INSERT END={symbol}");
    //  }
    //}
  }
}
