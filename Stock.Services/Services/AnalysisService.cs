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
    void Transform();
  }

  public class AnalysisService : BaseService, IAnalysisService
  {
    public void Transform()
    {
      //ebay
      //DeleteAll<JsonItem>();

      var symbols = DB.SymbolMaster.OrderBy(s => s.Symbol).ToList();
      foreach (var symbolMaster in symbols)
      {
        var models = new List<JsonItem>();
        var symbol = symbolMaster.Symbol;
        var items = DB.StockPrice.Where(s => s.Symbol == symbol).OrderBy(s => s.PriceDate).ToList();
        var itemCount = items.Count();
        foreach (var item in items.Select((model, idx) => (model, idx)))
        {
          var currIdx = item.idx;
          var prevIdx = currIdx - 1;
          var nextIdx = currIdx + 1;
          // Need at least 201 days for calculation
          if (currIdx < Consts.MAX_CALC_PERIOD)
          {
            continue;
          }
          // Need at least 60 days of future
          if (currIdx > itemCount - Consts.MAX_FUTURE_PERIOD - 1)
          {
            continue;
          }

          #region YActual
          item.model.YActual = GetClass(items[currIdx].ClosePrice, items[currIdx + 1].ClosePrice);
          item.model.YActual2 = GetClass(items[currIdx].ClosePrice, items[currIdx + 2].ClosePrice);
          item.model.YActual3 = GetClass(items[currIdx].ClosePrice, items[currIdx + 3].ClosePrice);
          item.model.YActual4 = GetClass(items[currIdx].ClosePrice, items[currIdx + 4].ClosePrice);
          item.model.YActual5 = GetClass(items[currIdx].ClosePrice, items[currIdx + 5].ClosePrice);
          item.model.YActual6 = GetClass(items[currIdx].ClosePrice, items[currIdx + 6].ClosePrice);
          item.model.YActual7 = GetClass(items[currIdx].ClosePrice, items[currIdx + 7].ClosePrice);
          item.model.YActual8 = GetClass(items[currIdx].ClosePrice, items[currIdx + 8].ClosePrice);
          item.model.YActual9 = GetClass(items[currIdx].ClosePrice, items[currIdx + 9].ClosePrice);
          item.model.YActual10 = GetClass(items[currIdx].ClosePrice, items[currIdx + 10].ClosePrice);
          item.model.YActual11 = GetClass(items[currIdx].ClosePrice, items[currIdx + 11].ClosePrice);
          item.model.YActual12 = GetClass(items[currIdx].ClosePrice, items[currIdx + 12].ClosePrice);
          item.model.YActual13 = GetClass(items[currIdx].ClosePrice, items[currIdx + 13].ClosePrice);
          item.model.YActual14 = GetClass(items[currIdx].ClosePrice, items[currIdx + 14].ClosePrice);
          item.model.YActual15 = GetClass(items[currIdx].ClosePrice, items[currIdx + 15].ClosePrice);
          item.model.YActual16 = GetClass(items[currIdx].ClosePrice, items[currIdx + 16].ClosePrice);
          item.model.YActual17 = GetClass(items[currIdx].ClosePrice, items[currIdx + 17].ClosePrice);
          item.model.YActual18 = GetClass(items[currIdx].ClosePrice, items[currIdx + 18].ClosePrice);
          item.model.YActual19 = GetClass(items[currIdx].ClosePrice, items[currIdx + 19].ClosePrice);
          item.model.YActual20 = GetClass(items[currIdx].ClosePrice, items[currIdx + 20].ClosePrice);
          item.model.YActual21 = GetClass(items[currIdx].ClosePrice, items[currIdx + 21].ClosePrice);
          item.model.YActual22 = GetClass(items[currIdx].ClosePrice, items[currIdx + 22].ClosePrice);
          item.model.YActual23 = GetClass(items[currIdx].ClosePrice, items[currIdx + 23].ClosePrice);
          item.model.YActual24 = GetClass(items[currIdx].ClosePrice, items[currIdx + 24].ClosePrice);
          item.model.YActual25 = GetClass(items[currIdx].ClosePrice, items[currIdx + 25].ClosePrice);
          item.model.YActual26 = GetClass(items[currIdx].ClosePrice, items[currIdx + 26].ClosePrice);
          item.model.YActual27 = GetClass(items[currIdx].ClosePrice, items[currIdx + 27].ClosePrice);
          item.model.YActual28 = GetClass(items[currIdx].ClosePrice, items[currIdx + 28].ClosePrice);
          item.model.YActual29 = GetClass(items[currIdx].ClosePrice, items[currIdx + 29].ClosePrice);
          item.model.YActual30 = GetClass(items[currIdx].ClosePrice, items[currIdx + 30].ClosePrice);
          item.model.YActual31 = GetClass(items[currIdx].ClosePrice, items[currIdx + 31].ClosePrice);
          item.model.YActual32 = GetClass(items[currIdx].ClosePrice, items[currIdx + 32].ClosePrice);
          item.model.YActual33 = GetClass(items[currIdx].ClosePrice, items[currIdx + 33].ClosePrice);
          item.model.YActual34 = GetClass(items[currIdx].ClosePrice, items[currIdx + 34].ClosePrice);
          item.model.YActual35 = GetClass(items[currIdx].ClosePrice, items[currIdx + 35].ClosePrice);
          item.model.YActual36 = GetClass(items[currIdx].ClosePrice, items[currIdx + 36].ClosePrice);
          item.model.YActual37 = GetClass(items[currIdx].ClosePrice, items[currIdx + 37].ClosePrice);
          item.model.YActual38 = GetClass(items[currIdx].ClosePrice, items[currIdx + 38].ClosePrice);
          item.model.YActual39 = GetClass(items[currIdx].ClosePrice, items[currIdx + 39].ClosePrice);
          item.model.YActual40 = GetClass(items[currIdx].ClosePrice, items[currIdx + 40].ClosePrice);
          item.model.YActual41 = GetClass(items[currIdx].ClosePrice, items[currIdx + 41].ClosePrice);
          item.model.YActual42 = GetClass(items[currIdx].ClosePrice, items[currIdx + 42].ClosePrice);
          item.model.YActual43 = GetClass(items[currIdx].ClosePrice, items[currIdx + 43].ClosePrice);
          item.model.YActual44 = GetClass(items[currIdx].ClosePrice, items[currIdx + 44].ClosePrice);
          item.model.YActual45 = GetClass(items[currIdx].ClosePrice, items[currIdx + 45].ClosePrice);
          item.model.YActual46 = GetClass(items[currIdx].ClosePrice, items[currIdx + 46].ClosePrice);
          item.model.YActual47 = GetClass(items[currIdx].ClosePrice, items[currIdx + 47].ClosePrice);
          item.model.YActual48 = GetClass(items[currIdx].ClosePrice, items[currIdx + 48].ClosePrice);
          item.model.YActual49 = GetClass(items[currIdx].ClosePrice, items[currIdx + 49].ClosePrice);
          item.model.YActual50 = GetClass(items[currIdx].ClosePrice, items[currIdx + 50].ClosePrice);
          item.model.YActual51 = GetClass(items[currIdx].ClosePrice, items[currIdx + 51].ClosePrice);
          item.model.YActual52 = GetClass(items[currIdx].ClosePrice, items[currIdx + 52].ClosePrice);
          item.model.YActual53 = GetClass(items[currIdx].ClosePrice, items[currIdx + 53].ClosePrice);
          item.model.YActual54 = GetClass(items[currIdx].ClosePrice, items[currIdx + 54].ClosePrice);
          item.model.YActual55 = GetClass(items[currIdx].ClosePrice, items[currIdx + 55].ClosePrice);
          item.model.YActual56 = GetClass(items[currIdx].ClosePrice, items[currIdx + 56].ClosePrice);
          item.model.YActual57 = GetClass(items[currIdx].ClosePrice, items[currIdx + 57].ClosePrice);
          item.model.YActual58 = GetClass(items[currIdx].ClosePrice, items[currIdx + 58].ClosePrice);
          item.model.YActual59 = GetClass(items[currIdx].ClosePrice, items[currIdx + 59].ClosePrice);
          item.model.YActual60 = GetClass(items[currIdx].ClosePrice, items[currIdx + 60].ClosePrice);
          #endregion

          #region Declarations
          var sumClosePrice10 = 0m;
          var sumClosePrice20 = 0m;
          var sumClosePrice50 = 0m;
          var sumClosePrice100 = 0m;
          var sumClosePrice200 = 0m;
          var sumVolume20 = 0m;
          var sumVolume30 = 0m;
          var sumVolume50 = 0m;
          var sumPriceVolume20 = 0m;
          var sumPriceVolume30 = 0m;
          var sumPriceVolume50 = 0m;
          var sumClosePriceGain6 = 0m;
          var sumClosePriceGain10 = 0m;
          var sumClosePriceGain14 = 0m;
          var sumClosePriceLoss6 = 0m;
          var sumClosePriceLoss10 = 0m;
          var sumClosePriceLoss14 = 0m;
          var maxClosePrice10 = 0m;
          var maxClosePrice20 = 0m;
          var maxClosePrice50 = 0m;
          var maxClosePrice200 = 0m;
          var minClosePrice10 = 0m;
          var minClosePrice20 = 0m;
          var minClosePrice50 = 0m;
          var minClosePrice200 = 0m;
          #endregion

          #region Loop past data for metrics
          // Assume Calculation is done at Close; thus inclusive.
          var startIdx = currIdx - Consts.MAX_CALC_PERIOD + 1;
          for (var i = startIdx; i <= currIdx; i++)
          {
            #region SMA
            sumClosePrice200 += items[i].ClosePrice;
            if (i > currIdx - Consts.SMA_PERIOD_10)
            {
              sumClosePrice10 += items[i].ClosePrice;
            }
            if (i > currIdx - Consts.SMA_PERIOD_20)
            {
              sumClosePrice20 += items[i].ClosePrice;
            }
            if (i > currIdx - Consts.SMA_PERIOD_50)
            {
              sumClosePrice50 += items[i].ClosePrice;
            }
            if (i > currIdx - Consts.SMA_PERIOD_100)
            {
              sumClosePrice100 += items[i].ClosePrice;
            }
            #endregion

            #region ADTV, ADPV
            if (i > currIdx - Consts.ADTV_PERIOD_20)
            {
              sumVolume20 += items[i].Volume;
            }
            if (i > currIdx - Consts.ADTV_PERIOD_30)
            {
              sumVolume30 += items[i].Volume;
            }
            if (i > currIdx - Consts.ADTV_PERIOD_50)
            {
              sumVolume50 += items[i].Volume;
            }
            if (i > currIdx - Consts.ADPV_PERIOD_20)
            {
              sumPriceVolume20 += items[i].PriceVolume;
            }
            if (i > currIdx - Consts.ADPV_PERIOD_30)
            {
              sumPriceVolume30 += items[i].PriceVolume;
            }
            if (i > currIdx - Consts.ADPV_PERIOD_50)
            {
              sumPriceVolume50 += items[i].PriceVolume;
            }
            #endregion

            #region RSI
            var diff = items[i].ClosePrice - items[i - 1].ClosePrice;
            if (i > currIdx - Consts.RSI_PERIOD_6)
            {
              if (diff > 0)
                sumClosePriceGain6 += diff;
              else if (diff < 0)
                sumClosePriceLoss6 += Math.Abs(diff);
            }
            if (i > currIdx - Consts.RSI_PERIOD_10)
            {
              if (diff > 0)
                sumClosePriceGain10 += diff;
              else if (diff < 0)
                sumClosePriceLoss10 += Math.Abs(diff);
            }
            if (i > currIdx - Consts.RSI_PERIOD_14)
            {
              if (diff > 0)
                sumClosePriceGain14 += diff;
              else if (diff < 0)
                sumClosePriceLoss14 += Math.Abs(diff);
            }
            #endregion

            #region Local Max, Min
            if (i > currIdx - Consts.LOCAL_MAX_PERIOD_10)
            {
              maxClosePrice10 = (maxClosePrice10 < items[i].ClosePrice) ? items[i].ClosePrice : maxClosePrice10;
            }
            if (i > currIdx - Consts.LOCAL_MAX_PERIOD_20)
            {
              maxClosePrice20 = (maxClosePrice20 < items[i].ClosePrice) ? items[i].ClosePrice : maxClosePrice20;
            }
            if (i > currIdx - Consts.LOCAL_MAX_PERIOD_50)
            {
              maxClosePrice50 = (maxClosePrice50 < items[i].ClosePrice) ? items[i].ClosePrice : maxClosePrice50;
            }
            if (i > currIdx - Consts.LOCAL_MAX_PERIOD_200)
            {
              maxClosePrice200 = (maxClosePrice200 < items[i].ClosePrice) ? items[i].ClosePrice : maxClosePrice200;
            }
            if (i > currIdx - Consts.LOCAL_MIN_PERIOD_10)
            {
              minClosePrice10 = (minClosePrice10 > items[i].ClosePrice) ? items[i].ClosePrice : minClosePrice10;
            }
            if (i > currIdx - Consts.LOCAL_MIN_PERIOD_20)
            {
              minClosePrice20 = (minClosePrice20 > items[i].ClosePrice) ? items[i].ClosePrice : minClosePrice20;
            }
            if (i > currIdx - Consts.LOCAL_MIN_PERIOD_50)
            {
              minClosePrice50 = (minClosePrice50 > items[i].ClosePrice) ? items[i].ClosePrice : minClosePrice50;
            }
            if (i > currIdx - Consts.LOCAL_MIN_PERIOD_200)
            {
              minClosePrice200 = (minClosePrice200 > items[i].ClosePrice) ? items[i].ClosePrice : minClosePrice200;
            }
            #endregion
          }
          #endregion

          #region Calculate SMA
          item.model.SMA_10 = NumberUtils.Round(sumClosePrice10 / Consts.SMA_PERIOD_10);
          item.model.SMA_20 = NumberUtils.Round(sumClosePrice20 / Consts.SMA_PERIOD_20);
          item.model.SMA_50 = NumberUtils.Round(sumClosePrice50 / Consts.SMA_PERIOD_50);
          item.model.SMA_100 = NumberUtils.Round(sumClosePrice100 / Consts.SMA_PERIOD_100);
          item.model.SMA_200 = NumberUtils.Round(sumClosePrice200 / Consts.SMA_PERIOD_200);
          item.model.HasSMA10CrossAbove20 = items[prevIdx].SMA_10 < items[prevIdx].SMA_20 && items[currIdx].SMA_10 > items[currIdx].SMA_20;
          item.model.HasSMA10CrossBelow20 = items[prevIdx].SMA_10 > items[prevIdx].SMA_20 && items[currIdx].SMA_10 < items[currIdx].SMA_20;
          item.model.HasSMA10CrossAbove50 = items[prevIdx].SMA_10 < items[prevIdx].SMA_50 && items[currIdx].SMA_10 > items[currIdx].SMA_50;
          item.model.HasSMA10CrossBelow50 = items[prevIdx].SMA_10 > items[prevIdx].SMA_50 && items[currIdx].SMA_10 < items[currIdx].SMA_50;
          item.model.HasSMA10CrossAbove100 = items[prevIdx].SMA_10 < items[prevIdx].SMA_100 && items[currIdx].SMA_10 > items[currIdx].SMA_100;
          item.model.HasSMA10CrossBelow100 = items[prevIdx].SMA_10 > items[prevIdx].SMA_100 && items[currIdx].SMA_10 < items[currIdx].SMA_100;
          item.model.HasSMA10CrossAbove200 = items[prevIdx].SMA_10 < items[prevIdx].SMA_200 && items[currIdx].SMA_10 > items[currIdx].SMA_200;
          item.model.HasSMA10CrossBelow200 = items[prevIdx].SMA_10 > items[prevIdx].SMA_200 && items[currIdx].SMA_10 < items[currIdx].SMA_200;
          item.model.HasSMA20CrossAbove50 = items[prevIdx].SMA_20 < items[prevIdx].SMA_50 && items[currIdx].SMA_20 > items[currIdx].SMA_50;
          item.model.HasSMA20CrossBelow50 = items[prevIdx].SMA_20 > items[prevIdx].SMA_50 && items[currIdx].SMA_20 < items[currIdx].SMA_50;
          item.model.HasSMA20CrossAbove100 = items[prevIdx].SMA_20 < items[prevIdx].SMA_100 && items[currIdx].SMA_20 > items[currIdx].SMA_100;
          item.model.HasSMA20CrossBelow100 = items[prevIdx].SMA_20 > items[prevIdx].SMA_100 && items[currIdx].SMA_20 < items[currIdx].SMA_100;
          item.model.HasSMA20CrossAbove200 = items[prevIdx].SMA_20 < items[prevIdx].SMA_200 && items[currIdx].SMA_20 > items[currIdx].SMA_200;
          item.model.HasSMA20CrossBelow200 = items[prevIdx].SMA_20 > items[prevIdx].SMA_200 && items[currIdx].SMA_20 < items[currIdx].SMA_200;
          item.model.HasSMA50CrossAbove100 = items[prevIdx].SMA_50 < items[prevIdx].SMA_100 && items[currIdx].SMA_50 > items[currIdx].SMA_100;
          item.model.HasSMA50CrossBelow100 = items[prevIdx].SMA_50 > items[prevIdx].SMA_100 && items[currIdx].SMA_50 < items[currIdx].SMA_100;
          item.model.HasSMA50CrossAbove200 = items[prevIdx].SMA_50 < items[prevIdx].SMA_200 && items[currIdx].SMA_50 > items[currIdx].SMA_200;
          item.model.HasSMA50CrossBelow200 = items[prevIdx].SMA_50 > items[prevIdx].SMA_200 && items[currIdx].SMA_50 < items[currIdx].SMA_200;
          item.model.HasSMA100CrossAbove200 = items[prevIdx].SMA_100 < items[prevIdx].SMA_200 && items[currIdx].SMA_100 > items[currIdx].SMA_200;
          item.model.HasSMA100CrossBelow200 = items[prevIdx].SMA_100 > items[prevIdx].SMA_200 && items[currIdx].SMA_100 < items[currIdx].SMA_200;
          #endregion

          #region Calculate EMA
          // EMA for t1 = Price of t1
          // EMA for > t1 = Alpha * Price + (1 - Alpha) * Previous EMA
          // where Alpha = 2 / (N + 1)
          var alpha5 = 2m / (Consts.EMA_PERIOD_5 + 1);
          var alpha9 = 2m / (Consts.EMA_PERIOD_9 + 1);
          var alpha10 = 2m / (Consts.EMA_PERIOD_10 + 1);
          var alpha12 = 2m / (Consts.EMA_PERIOD_12 + 1);
          var alpha13 = 2m / (Consts.EMA_PERIOD_13 + 1);
          var alpha26 = 2m / (Consts.EMA_PERIOD_26 + 1);
          var alpha48 = 2m / (Consts.EMA_PERIOD_48 + 1);
          var prevEma5 = (items[prevIdx].EMA_5 > 0) ? items[prevIdx].EMA_5 : items[currIdx].ClosePrice;
          var prevEma9 = (items[prevIdx].EMA_9 > 0) ? items[prevIdx].EMA_9 : items[currIdx].ClosePrice;
          var prevEma10 = (items[prevIdx].EMA_10 > 0) ? items[prevIdx].EMA_10 : items[currIdx].ClosePrice;
          var prevEma12 = (items[prevIdx].EMA_12 > 0) ? items[prevIdx].EMA_12 : items[currIdx].ClosePrice;
          var prevEma13 = (items[prevIdx].EMA_13 > 0) ? items[prevIdx].EMA_13 : items[currIdx].ClosePrice;
          var prevEma26 = (items[prevIdx].EMA_26 > 0) ? items[prevIdx].EMA_26 : items[currIdx].ClosePrice;
          var prevEma48 = (items[prevIdx].EMA_48 > 0) ? items[prevIdx].EMA_48 : items[currIdx].ClosePrice;
          item.model.EMA_5 = NumberUtils.Round((alpha5 * item.model.ClosePrice) + ((1 - alpha5) * prevEma5));
          item.model.EMA_9 = NumberUtils.Round((alpha9 * item.model.ClosePrice) + ((1 - alpha9) * prevEma9));
          item.model.EMA_10 = NumberUtils.Round((alpha10 * item.model.ClosePrice) + ((1 - alpha10) * prevEma10));
          item.model.EMA_12 = NumberUtils.Round((alpha12 * item.model.ClosePrice) + ((1 - alpha12) * prevEma12));
          item.model.EMA_13 = NumberUtils.Round((alpha13 * item.model.ClosePrice) + ((1 - alpha13) * prevEma13));
          item.model.EMA_26 = NumberUtils.Round((alpha26 * item.model.ClosePrice) + ((1 - alpha26) * prevEma26));
          item.model.EMA_48 = NumberUtils.Round((alpha48 * item.model.ClosePrice) + ((1 - alpha48) * prevEma48));
          item.model.HasEMA5CrossAbove9 = items[prevIdx].EMA_5 < items[prevIdx].EMA_9 && items[currIdx].EMA_5 > items[currIdx].EMA_9;
          item.model.HasEMA5CrossBelow9 = items[prevIdx].EMA_5 > items[prevIdx].EMA_9 && items[currIdx].EMA_5 < items[currIdx].EMA_9;
          item.model.HasEMA5CrossAbove10 = items[prevIdx].EMA_5 < items[prevIdx].EMA_10 && items[currIdx].EMA_5 > items[currIdx].EMA_10;
          item.model.HasEMA5CrossBelow10 = items[prevIdx].EMA_5 > items[prevIdx].EMA_10 && items[currIdx].EMA_5 < items[currIdx].EMA_10;
          item.model.HasEMA5CrossAbove12 = items[prevIdx].EMA_5 < items[prevIdx].EMA_12 && items[currIdx].EMA_5 > items[currIdx].EMA_12;
          item.model.HasEMA5CrossBelow12 = items[prevIdx].EMA_5 > items[prevIdx].EMA_12 && items[currIdx].EMA_5 < items[currIdx].EMA_12;
          item.model.HasEMA5CrossAbove13 = items[prevIdx].EMA_5 < items[prevIdx].EMA_13 && items[currIdx].EMA_5 > items[currIdx].EMA_13;
          item.model.HasEMA5CrossBelow13 = items[prevIdx].EMA_5 > items[prevIdx].EMA_13 && items[currIdx].EMA_5 < items[currIdx].EMA_13;
          item.model.HasEMA5CrossAbove26 = items[prevIdx].EMA_5 < items[prevIdx].EMA_26 && items[currIdx].EMA_5 > items[currIdx].EMA_26;
          item.model.HasEMA5CrossBelow26 = items[prevIdx].EMA_5 > items[prevIdx].EMA_26 && items[currIdx].EMA_5 < items[currIdx].EMA_26;
          item.model.HasEMA5CrossAbove48 = items[prevIdx].EMA_5 < items[prevIdx].EMA_48 && items[currIdx].EMA_5 > items[currIdx].EMA_48;
          item.model.HasEMA5CrossBelow48 = items[prevIdx].EMA_5 > items[prevIdx].EMA_48 && items[currIdx].EMA_5 < items[currIdx].EMA_48;
          item.model.HasEMA9CrossAbove10 = items[prevIdx].EMA_9 < items[prevIdx].EMA_10 && items[currIdx].EMA_9 > items[currIdx].EMA_10;
          item.model.HasEMA9CrossBelow10 = items[prevIdx].EMA_9 > items[prevIdx].EMA_10 && items[currIdx].EMA_9 < items[currIdx].EMA_10;
          item.model.HasEMA9CrossAbove12 = items[prevIdx].EMA_9 < items[prevIdx].EMA_12 && items[currIdx].EMA_9 > items[currIdx].EMA_12;
          item.model.HasEMA9CrossBelow12 = items[prevIdx].EMA_9 > items[prevIdx].EMA_12 && items[currIdx].EMA_9 < items[currIdx].EMA_12;
          item.model.HasEMA9CrossAbove13 = items[prevIdx].EMA_9 < items[prevIdx].EMA_13 && items[currIdx].EMA_9 > items[currIdx].EMA_13;
          item.model.HasEMA9CrossBelow13 = items[prevIdx].EMA_9 > items[prevIdx].EMA_13 && items[currIdx].EMA_9 < items[currIdx].EMA_13;
          item.model.HasEMA9CrossAbove26 = items[prevIdx].EMA_9 < items[prevIdx].EMA_26 && items[currIdx].EMA_9 > items[currIdx].EMA_26;
          item.model.HasEMA9CrossBelow26 = items[prevIdx].EMA_9 > items[prevIdx].EMA_26 && items[currIdx].EMA_9 < items[currIdx].EMA_26;
          item.model.HasEMA9CrossAbove48 = items[prevIdx].EMA_9 < items[prevIdx].EMA_48 && items[currIdx].EMA_9 > items[currIdx].EMA_48;
          item.model.HasEMA9CrossBelow48 = items[prevIdx].EMA_9 > items[prevIdx].EMA_48 && items[currIdx].EMA_9 < items[currIdx].EMA_48;
          item.model.HasEMA10CrossAbove12 = items[prevIdx].EMA_10 < items[prevIdx].EMA_12 && items[currIdx].EMA_10 > items[currIdx].EMA_12;
          item.model.HasEMA10CrossBelow12 = items[prevIdx].EMA_10 > items[prevIdx].EMA_12 && items[currIdx].EMA_10 < items[currIdx].EMA_12;
          item.model.HasEMA10CrossAbove13 = items[prevIdx].EMA_10 < items[prevIdx].EMA_13 && items[currIdx].EMA_10 > items[currIdx].EMA_13;
          item.model.HasEMA10CrossBelow13 = items[prevIdx].EMA_10 > items[prevIdx].EMA_13 && items[currIdx].EMA_10 < items[currIdx].EMA_13;
          item.model.HasEMA10CrossAbove26 = items[prevIdx].EMA_10 < items[prevIdx].EMA_26 && items[currIdx].EMA_10 > items[currIdx].EMA_26;
          item.model.HasEMA10CrossBelow26 = items[prevIdx].EMA_10 > items[prevIdx].EMA_26 && items[currIdx].EMA_10 < items[currIdx].EMA_26;
          item.model.HasEMA10CrossAbove48 = items[prevIdx].EMA_10 < items[prevIdx].EMA_48 && items[currIdx].EMA_10 > items[currIdx].EMA_48;
          item.model.HasEMA10CrossBelow48 = items[prevIdx].EMA_10 > items[prevIdx].EMA_48 && items[currIdx].EMA_10 < items[currIdx].EMA_48;
          item.model.HasEMA12CrossAbove13 = items[prevIdx].EMA_12 < items[prevIdx].EMA_13 && items[currIdx].EMA_12 > items[currIdx].EMA_13;
          item.model.HasEMA12CrossBelow13 = items[prevIdx].EMA_12 > items[prevIdx].EMA_13 && items[currIdx].EMA_12 < items[currIdx].EMA_13;
          item.model.HasEMA12CrossAbove26 = items[prevIdx].EMA_12 < items[prevIdx].EMA_26 && items[currIdx].EMA_12 > items[currIdx].EMA_26;
          item.model.HasEMA12CrossBelow26 = items[prevIdx].EMA_12 > items[prevIdx].EMA_26 && items[currIdx].EMA_12 < items[currIdx].EMA_26;
          item.model.HasEMA12CrossAbove48 = items[prevIdx].EMA_12 < items[prevIdx].EMA_48 && items[currIdx].EMA_12 > items[currIdx].EMA_48;
          item.model.HasEMA12CrossBelow48 = items[prevIdx].EMA_12 > items[prevIdx].EMA_48 && items[currIdx].EMA_12 < items[currIdx].EMA_48;
          item.model.HasEMA13CrossAbove26 = items[prevIdx].EMA_13 < items[prevIdx].EMA_26 && items[currIdx].EMA_13 > items[currIdx].EMA_26;
          item.model.HasEMA13CrossBelow26 = items[prevIdx].EMA_13 > items[prevIdx].EMA_26 && items[currIdx].EMA_13 < items[currIdx].EMA_26;
          item.model.HasEMA13CrossAbove48 = items[prevIdx].EMA_13 < items[prevIdx].EMA_48 && items[currIdx].EMA_13 > items[currIdx].EMA_48;
          item.model.HasEMA13CrossBelow48 = items[prevIdx].EMA_13 > items[prevIdx].EMA_48 && items[currIdx].EMA_13 < items[currIdx].EMA_48;
          item.model.HasEMA26CrossAbove48 = items[prevIdx].EMA_26 < items[prevIdx].EMA_48 && items[currIdx].EMA_26 > items[currIdx].EMA_48;
          item.model.HasEMA26CrossBelow48 = items[prevIdx].EMA_26 > items[prevIdx].EMA_48 && items[currIdx].EMA_26 < items[currIdx].EMA_48;
          #endregion

          #region Calculate ADTV, ADPV
          item.model.ADTV_20 = NumberUtils.Round(sumVolume20 / Consts.ADTV_PERIOD_20);
          item.model.ADTV_30 = NumberUtils.Round(sumVolume30 / Consts.ADTV_PERIOD_30);
          item.model.ADTV_50 = NumberUtils.Round(sumVolume50 / Consts.ADTV_PERIOD_50);
          item.model.ADPV_20 = NumberUtils.Round(sumPriceVolume20 / Consts.ADPV_PERIOD_20);
          item.model.ADPV_30 = NumberUtils.Round(sumPriceVolume30 / Consts.ADPV_PERIOD_30);
          item.model.ADPV_50 = NumberUtils.Round(sumPriceVolume50 / Consts.ADPV_PERIOD_50);
          #endregion

          #region Calculate RSI
          // RSI = 100 - (100 / (1 + RS))
          // RS = AvgGain / AvgLoss
          // RS = 0 if AvgLoss = 0
          // First AvgGain = Sum / 14
          // First AvgLoss = Sum / 14
          // Next AvgGain = (Prev AvgGain * 13 + CurrentGain) / 14
          // Next AvgLoss = (Prev AvgLoss * 13 + CurrentLoss) / 14
          var avgGain6 = sumClosePriceGain6 / Consts.RSI_PERIOD_6;
          var avgLoss6 = sumClosePriceLoss6 / Consts.RSI_PERIOD_6;
          var avgGain10 = sumClosePriceGain10 / Consts.RSI_PERIOD_10;
          var avgLoss10 = sumClosePriceLoss10 / Consts.RSI_PERIOD_10;
          var avgGain14 = sumClosePriceGain14 / Consts.RSI_PERIOD_14;
          var avgLoss14 = sumClosePriceLoss14 / Consts.RSI_PERIOD_14;
          var priceDiff = items[currIdx].ClosePrice - items[prevIdx].ClosePrice;
          var currGain = (priceDiff > 0) ? priceDiff : 0;
          var currLoss = (priceDiff < 0) ? Math.Abs(priceDiff) : 0;
          if (items[prevIdx].AvgGain_6 != null && items[prevIdx].AvgLoss_6 != null)
          {
            avgGain6 = (items[prevIdx].AvgGain_6.Value * (Consts.RSI_PERIOD_6 - 1) + currGain) / Consts.RSI_PERIOD_6;
            avgLoss6 = (items[prevIdx].AvgLoss_6.Value * (Consts.RSI_PERIOD_6 - 1) + currLoss) / Consts.RSI_PERIOD_6;
          }
          if (items[prevIdx].AvgGain_10 != null && items[prevIdx].AvgLoss_10 != null)
          {
            avgGain10 = (items[prevIdx].AvgGain_10.Value * (Consts.RSI_PERIOD_10 - 1) + currGain) / Consts.RSI_PERIOD_10;
            avgLoss10 = (items[prevIdx].AvgLoss_10.Value * (Consts.RSI_PERIOD_10 - 1) + currLoss) / Consts.RSI_PERIOD_10;
          }
          if (items[prevIdx].AvgGain_14 != null && items[prevIdx].AvgLoss_14 != null)
          {
            avgGain14 = (items[prevIdx].AvgGain_14.Value * (Consts.RSI_PERIOD_14 - 1) + currGain) / Consts.RSI_PERIOD_14;
            avgLoss14 = (items[prevIdx].AvgLoss_14.Value * (Consts.RSI_PERIOD_14 - 1) + currLoss) / Consts.RSI_PERIOD_14;
          }
          var rs6 = (avgLoss6 > 0) ? avgGain6 / avgLoss6 : 0;
          var rs10 = (avgLoss10 > 0) ? avgGain10 / avgLoss10 : 0;
          var rs14 = (avgLoss14 > 0) ? avgGain14 / avgLoss14 : 0;
          var rsi6 = NumberUtils.Round(100 - (100 / (1 + rs6)));
          var rsi10 = NumberUtils.Round(100 - (100 / (1 + rs10)));
          var rsi14 = NumberUtils.Round(100 - (100 / (1 + rs14)));
          item.model.AvgGain_6 = avgGain6;
          item.model.AvgLoss_6 = avgLoss6;
          item.model.AvgGain_10 = avgGain10;
          item.model.AvgLoss_10 = avgLoss10;
          item.model.AvgGain_14 = avgGain14;
          item.model.AvgLoss_14 = avgLoss14;
          item.model.RSI_6 = rsi6;
          item.model.RSI_10 = rsi10;
          item.model.RSI_14 = rsi14;
          item.model.HasRSI6CrossAbove70 = items[prevIdx].RSI_6 < Consts.RSI_70 && items[currIdx].RSI_6 > Consts.RSI_70;
          item.model.HasRSI6CrossAbove75 = items[prevIdx].RSI_6 < Consts.RSI_75 && items[currIdx].RSI_6 > Consts.RSI_75;
          item.model.HasRSI6CrossAbove80 = items[prevIdx].RSI_6 < Consts.RSI_80 && items[currIdx].RSI_6 > Consts.RSI_80;
          item.model.HasRSI6CrossAbove85 = items[prevIdx].RSI_6 < Consts.RSI_85 && items[currIdx].RSI_6 > Consts.RSI_85;
          item.model.HasRSI6CrossBelow30 = items[prevIdx].RSI_6 > Consts.RSI_30 && items[currIdx].RSI_6 < Consts.RSI_30;
          item.model.HasRSI6CrossBelow25 = items[prevIdx].RSI_6 > Consts.RSI_25 && items[currIdx].RSI_6 < Consts.RSI_25;
          item.model.HasRSI6CrossBelow20 = items[prevIdx].RSI_6 > Consts.RSI_20 && items[currIdx].RSI_6 < Consts.RSI_20;
          item.model.HasRSI6CrossBelow15 = items[prevIdx].RSI_6 > Consts.RSI_15 && items[currIdx].RSI_6 < Consts.RSI_15;
          item.model.HasRSI10CrossAbove70 = items[prevIdx].RSI_10 < Consts.RSI_70 && items[currIdx].RSI_10 > Consts.RSI_70;
          item.model.HasRSI10CrossAbove75 = items[prevIdx].RSI_10 < Consts.RSI_75 && items[currIdx].RSI_10 > Consts.RSI_75;
          item.model.HasRSI10CrossAbove80 = items[prevIdx].RSI_10 < Consts.RSI_80 && items[currIdx].RSI_10 > Consts.RSI_80;
          item.model.HasRSI10CrossAbove85 = items[prevIdx].RSI_10 < Consts.RSI_85 && items[currIdx].RSI_10 > Consts.RSI_85;
          item.model.HasRSI10CrossBelow30 = items[prevIdx].RSI_10 > Consts.RSI_30 && items[currIdx].RSI_10 < Consts.RSI_30;
          item.model.HasRSI10CrossBelow25 = items[prevIdx].RSI_10 > Consts.RSI_25 && items[currIdx].RSI_10 < Consts.RSI_25;
          item.model.HasRSI10CrossBelow20 = items[prevIdx].RSI_10 > Consts.RSI_20 && items[currIdx].RSI_10 < Consts.RSI_20;
          item.model.HasRSI10CrossBelow15 = items[prevIdx].RSI_10 > Consts.RSI_15 && items[currIdx].RSI_10 < Consts.RSI_15;
          item.model.HasRSI14CrossAbove70 = items[prevIdx].RSI_14 < Consts.RSI_70 && items[currIdx].RSI_14 > Consts.RSI_70;
          item.model.HasRSI14CrossAbove75 = items[prevIdx].RSI_14 < Consts.RSI_75 && items[currIdx].RSI_14 > Consts.RSI_75;
          item.model.HasRSI14CrossAbove80 = items[prevIdx].RSI_14 < Consts.RSI_80 && items[currIdx].RSI_14 > Consts.RSI_80;
          item.model.HasRSI14CrossAbove85 = items[prevIdx].RSI_14 < Consts.RSI_85 && items[currIdx].RSI_14 > Consts.RSI_85;
          item.model.HasRSI14CrossBelow30 = items[prevIdx].RSI_14 > Consts.RSI_30 && items[currIdx].RSI_14 < Consts.RSI_30;
          item.model.HasRSI14CrossBelow25 = items[prevIdx].RSI_14 > Consts.RSI_25 && items[currIdx].RSI_14 < Consts.RSI_25;
          item.model.HasRSI14CrossBelow20 = items[prevIdx].RSI_14 > Consts.RSI_20 && items[currIdx].RSI_14 < Consts.RSI_20;
          item.model.HasRSI14CrossBelow15 = items[prevIdx].RSI_14 > Consts.RSI_15 && items[currIdx].RSI_14 < Consts.RSI_15;
          #endregion

          #region Calculate Bollinger Bands
          // Middle Band = SMA_20
          // Upper Band = SMA_20 + (standard deviation x 2) 
          // Lower Band = SMA_20 - (standard deviation x 2)
          var sumClosePriceDevSqrd20 = 0m;
          startIdx = currIdx - Consts.BOLLINGER_PERIOD_20 + 1;
          for (var i = startIdx; i <= currIdx; i++)
          {
            var deviation = items[i].ClosePrice - item.model.SMA_20;
            sumClosePriceDevSqrd20 += NumberUtils.Pow(deviation, 2);
          }
          var stdDev20 = NumberUtils.Pow(sumClosePriceDevSqrd20 / Consts.BOLLINGER_PERIOD_20, 0.5m);
          item.model.BollingerUpperStvDev1_20 = item.model.SMA_20 + stdDev20;
          item.model.BollingerLowerStvDev1_20 = item.model.SMA_20 - stdDev20;
          item.model.BollingerUpperStvDev2_20 = item.model.SMA_20 + stdDev20 * 2;
          item.model.BollingerLowerStvDev2_20 = item.model.SMA_20 - stdDev20 * 2;
          item.model.BollingerUpperStvDev25_20 = item.model.SMA_20 + stdDev20 * 2.5m;
          item.model.BollingerLowerStvDev25_20 = item.model.SMA_20 - stdDev20 * 2.5m;
          item.model.HasCrossAboveBollingerUpperStvDev2_20 = items[prevIdx].ClosePrice < items[prevIdx].BollingerUpperStvDev2_20 && items[currIdx].ClosePrice > items[currIdx].BollingerUpperStvDev2_20;
          item.model.HasCrossBelowBollingerLowerStvDev2_20 = items[prevIdx].ClosePrice > items[prevIdx].BollingerLowerStvDev2_20 && items[currIdx].ClosePrice < items[currIdx].BollingerLowerStvDev2_20;
          item.model.HasCrossAboveBollingerUpperStvDev25_20 = items[prevIdx].ClosePrice < items[prevIdx].BollingerUpperStvDev25_20 && items[currIdx].ClosePrice > items[currIdx].BollingerUpperStvDev25_20;
          item.model.HasCrossBelowBollingerLowerStvDev25_20 = items[prevIdx].ClosePrice > items[prevIdx].BollingerLowerStvDev25_20 && items[currIdx].ClosePrice < items[currIdx].BollingerLowerStvDev25_20;
          #endregion

          models.Add(new JsonItem() { JsonStr = StringUtils.Serialize(item.model) });
        }

        LogUtils.Debug($"INSERT START={symbol}");
        var skip = 0;
        while (skip < models.Count)
        {
          var query = models.Skip(skip).Take(500);
          Insert<JsonItem>(query.ToList());
          skip += 500;
        }
        LogUtils.Debug($"INSERT END={symbol}");
      }
    }

    public int GetClass(decimal pastPrice, decimal futurePrice)
    {
      return (futurePrice > pastPrice) ? Consts.CLASS_UP : Consts.CLASS_DOWN;
    }
  }
}
