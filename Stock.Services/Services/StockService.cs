using Stock.Services.Clients;
using Stock.Services.Models;
using Stock.Services.Models.CsvHelper;
using Stock.Services.Models.EF;
using Stock.Services.Models.TDAm;
using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Stock.Services.Services
{
  public interface IStockService
  {
    List<SymbolMaster> GetSymbols();
    StockPrice GetStockPrice(string symbol, DateTime priceDate);
    ChartImage GetChartImage(string symbol, DateTime priceDate, int version);
    int AddStockPriceFromCsv(string symbol, string filePath);
    int GetRecentPriceHistoryForAllStocks();
    int SaveChartImages(List<SaveChartImageReq> items);
    int CreateChartImagesV2();
    List<List<StockPrice>> GetPeriodsOfStockPrices(GetPeriodsOfStockPricesReq request);
    void CreateCsvForPrediction(string fileName, DateTime dateFrom, DateTime dateTo, bool isBetween, int version, bool isExcludeNullYActual);
    int SavePredictions(string fileName);
  }

  public class StockService : BaseService, IStockService
  {
    public List<SymbolMaster> GetSymbols()
    {
      return DB.SymbolMaster.ToList();
    }

    public StockPrice GetStockPrice(string symbol, DateTime priceDate)
    {
      if (string.IsNullOrEmpty(symbol)) return null;
      return DB.StockPrice.FirstOrDefault(c => c.Symbol.ToUpper() == symbol.ToUpper() && c.PriceDate == priceDate);
    }

    public ChartImage GetChartImage(string symbol, DateTime priceDate, int version)
    {
      if (string.IsNullOrEmpty(symbol)) return null;
      return DB.ChartImage.FirstOrDefault(c => c.Symbol.ToUpper() == symbol.ToUpper() && c.PriceDate == priceDate && c.Version == version);
    }

    public int AddStockPriceFromCsv(string symbol, string fileName)
    {
      var ret = 0;
      if (!string.IsNullOrEmpty(symbol) && File.Exists(fileName))
      {
        var models = new List<StockPrice>();
        var records = CsvUtils.ParseCsv<StockPrice, MapYahooStockPrice>(fileName);
        if (records != null)
        {
          foreach (var record in records)
          {
            var orgStockPrice = GetStockPrice(symbol, record.PriceDate);
            if (orgStockPrice == null)
            {
              record.Symbol = symbol;
              models.Add(record);
            }
          }
          Insert<StockPrice>(models);
        }
      }
      return ret;
    }

    /// <summary>
    /// Gets Recent Price History for all Stocks
    /// TO DO Refactor this out with other 
    /// Data Collection stuff
    /// </summary>
    /// <returns></returns>
    public int GetRecentPriceHistoryForAllStocks()
    {
      var ret = 0;
      var symbols = GetSymbols();
      if (symbols != null)
      {
        var requests = new List<TDAmPriceHistoryReq>();
        foreach (var symbol in symbols)
        {
          requests.Add(new TDAmPriceHistoryReq()
          {
            Symbol = symbol.Symbol,
            apikey = Configs.TDAmApiKey,
            periodType = Consts.TDAM_PERIOD_MONTH,
            period = 1,
            frequencyType = Consts.TDAM_FREQUENCY_DAILY,
            frequency = 1
          });
        }

        var responses = TDAmClient.GetPriceHistory(requests);
        if (responses != null)
        {
          var models = new List<StockPrice>();
          foreach (var response in responses)
          {
            if (response != null && response.candles != null && !string.IsNullOrEmpty(response.symbol))
            {
              LogUtils.Debug($"Prices for {response.symbol} found.");
              foreach (var candle in response.candles)
              {
                if (candle != null)
                {
                  var orgPrice = GetStockPrice(response.symbol, candle.PriceDateWithoutTime);
                  if (orgPrice == null)
                  {
                    // Insert
                    models.Add(new StockPrice()
                    {
                      Symbol = response.symbol.ToUpper(),
                      OpenPrice = candle.open,
                      HighPrice = candle.high,
                      LowPrice = candle.low,
                      ClosePrice = candle.close,
                      Volume = candle.volume,
                      PriceDate = candle.PriceDateWithoutTime
                    });
                  }
                }
              }
            }
          }
          ret += Insert<StockPrice>(models);
        }
      }
      return ret;
    }

    /// <summary>
    /// Saves Chart Images
    /// Combines Candle Stick Chart Image and Volume Chart Image
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    public int SaveChartImages(List<SaveChartImageReq> items)
    {
      var ret = 0;
      var models = new List<ChartImage>();
      if (items != null)
      {
        foreach (var item in items)
        {
          if (string.IsNullOrEmpty(item.CandleImage))
          {
            continue;
          }
          if (string.IsNullOrEmpty(item.VolumeImage))
          {
            continue;
          }
          var org = GetChartImage(item.Symbol, item.PriceDate, item.Version);
          if (org != null)
          {
            continue;
          }
          using (var candleMs = new MemoryStream(item.CandleByteArray))
          using (var volumeMs = new MemoryStream(item.VolumeByteArray))
          {
            using (var candleImage = Image.FromStream(candleMs))
            using (var volumeImage = Image.FromStream(volumeMs))
            {
              using (var bitMap = new Bitmap(Consts.CHART_IMAGE_WIDTH, Consts.CHART_IMAGE_HEIGHT))
              {
                using (var canvas = Graphics.FromImage(bitMap))
                {
                  canvas.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                  canvas.DrawImage(candleImage, 0, 0, candleImage.Width, candleImage.Height);
                  canvas.DrawImage(volumeImage, 0, candleImage.Height, volumeImage.Width, volumeImage.Height);
                  canvas.Save();
                  using (var newMs = new MemoryStream())
                  {
                    bitMap.Save(newMs, System.Drawing.Imaging.ImageFormat.Png);
                    models.Add(new ChartImage()
                    {
                      Symbol = item.Symbol,
                      PriceDate = item.PriceDate,
                      YActual = item.YActual,
                      XImage = StringUtils.ToString(newMs.ToArray()),
                      Version = item.Version
                    });
                  }
                }
              }
            }
          }
        }
        ret += Insert<ChartImage>(models);
      }
      return ret;
    }

    /// <summary>
    /// Save Chart Image for v2
    /// Combines v1 with Dow Jones Chart Image
    /// </summary>
    /// <returns></returns>
    public int CreateChartImagesV2()
    {
      var ret = 0;
      var models = new List<ChartImage>();
      var v1Items = DB.ChartImage.Where(c =>
        c.Symbol != Consts.SYMBOL_DOW_JONES &&
        c.Symbol != Consts.SYMBOL_DOW_ETF &&
        c.Version == Consts.CHART_V1).ToList();
      if (v1Items != null)
      {
        foreach (var v1Item in v1Items)
        {
          LogUtils.Debug($"Begin {v1Item.Symbol}");
          var orgV2 = GetChartImage(v1Item.Symbol, v1Item.PriceDate, Consts.CHART_V2);
          if (orgV2 != null)
          {
            continue;
          }

          var dowItem = GetChartImage(Consts.SYMBOL_DOW_JONES, v1Item.PriceDate, Consts.CHART_V1);
          if (dowItem == null)
          {
            dowItem = GetChartImage(Consts.SYMBOL_DOW_ETF, v1Item.PriceDate, Consts.CHART_V1);
            if (dowItem == null)
            {
              continue;
            }
          }

          var v1ByteArray = StringUtils.ToByteArray(v1Item.XImage);
          var dowByteArray = StringUtils.ToByteArray(dowItem.XImage);
          using (var v1Ms = new MemoryStream(v1ByteArray))
          using (var dowMs = new MemoryStream(dowByteArray))
          {
            using (var v1Image = Image.FromStream(v1Ms))
            using (var dowImage = Image.FromStream(dowMs))
            {
              using (var bitMap = new Bitmap(Consts.CHART_IMAGE_WIDTH_V2, Consts.CHART_IMAGE_HEIGHT_V2))
              {
                using (var canvas = Graphics.FromImage(bitMap))
                {
                  canvas.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                  canvas.DrawImage(v1Image, 0, 0, v1Image.Width, v1Image.Height);
                  canvas.DrawImage(dowImage, v1Image.Width, v1Image.Height, dowImage.Width, dowImage.Height);
                  canvas.Save();
                  using (var newMs = new MemoryStream())
                  {
                    bitMap.Save(newMs, System.Drawing.Imaging.ImageFormat.Png);
                    models.Add(new ChartImage()
                    {
                      Symbol = v1Item.Symbol,
                      PriceDate = v1Item.PriceDate,
                      YActual = v1Item.YActual,
                      XImage = StringUtils.ToString(newMs.ToArray()),
                      Version = Consts.CHART_V2
                    });
                  }
                }
              }
            }
          }
        }
        ret += Insert<ChartImage>(models);
      }
      return ret;
    }

    /// <summary>
    /// Gets Periods of Stock Prices
    /// Filtered by Business Logic
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="noOfPeriods"></param>
    /// <returns></returns>
    public List<List<StockPrice>> GetPeriodsOfStockPrices(GetPeriodsOfStockPricesReq request)
    {
      LogUtils.Debug($"START-{nameof(GetPeriodsOfStockPrices)}, Symbol={request.Symbol}, NoOfPeriods={request.NoOfPeriods}");
      var sp = new Stopwatch();
      sp.Start();



      // ========== Temp Log for Analysis
      var currentTechs = new List<TechnicalItem>();
      var prevTechs = new List<TechnicalItem>();


      var ret = new List<List<StockPrice>>();
      if (!string.IsNullOrEmpty(request.Symbol))
      {
        var lastAddedIndex = -1m;
        var query = DB.StockPrice.AsQueryable();
        query = query.Where(s => s.Symbol.ToUpper() == request.Symbol.ToUpper());
        query = query.OrderBy(s => s.PriceDate);
        var items = query.ToList();
        foreach (var item in items.Select((model, index) => (model, index)))
        {
          var currentIndex = item.index;
          var prevIndex = currentIndex - 1;
          var nextIndex = currentIndex + 1;
          if (currentIndex >= Consts.MAX_CALC_PERIOD)
          {
            // TO DO: OBV?
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
            var sumClosePriceGain10 = 0m;
            var sumClosePriceGain14 = 0m;
            var sumClosePriceLoss10 = 0m;
            var sumClosePriceLoss14 = 0m;
            var maxClosePrice10 = 0m;
            var maxClosePrice20 = 0m;
            var maxClosePrice50 = 0m;
            var minClosePrice10 = 0m;
            var minClosePrice20 = 0m;
            var minClosePrice50 = 0m;

            #region Loop past data for metrics
            var stopIndex = currentIndex - Consts.MAX_CALC_PERIOD;
            for (var i = prevIndex; i >= stopIndex; i--)
            {
              #region SMA
              sumClosePrice200 += items[i].ClosePrice;
              if (i >= currentIndex - Consts.SMA_PERIOD_10)
              {
                sumClosePrice10 += items[i].ClosePrice;
              }
              if (i >= currentIndex - Consts.SMA_PERIOD_20)
              {
                sumClosePrice20 += items[i].ClosePrice;
              }
              if (i >= currentIndex - Consts.SMA_PERIOD_50)
              {
                sumClosePrice50 += items[i].ClosePrice;
              }
              if (i >= currentIndex - Consts.SMA_PERIOD_100)
              {
                sumClosePrice100 += items[i].ClosePrice;
              }
              #endregion

              #region ADTV, ADPV
              if (i >= currentIndex - Consts.ADTV_PERIOD_20)
              {
                sumVolume20 += items[i].Volume;
              }
              if (i >= currentIndex - Consts.ADTV_PERIOD_30)
              {
                sumVolume30 += items[i].Volume;
              }
              if (i >= currentIndex - Consts.ADTV_PERIOD_50)
              {
                sumVolume50 += items[i].Volume;
              }
              if (i >= currentIndex - Consts.ADPV_PERIOD_20)
              {
                sumPriceVolume20 += items[i].PriceVolume;
              }
              if (i >= currentIndex - Consts.ADPV_PERIOD_30)
              {
                sumPriceVolume30 += items[i].PriceVolume;
              }
              if (i >= currentIndex - Consts.ADPV_PERIOD_50)
              {
                sumPriceVolume50 += items[i].PriceVolume;
              }
              #endregion

              #region RSI
              var priceDiff = (i - 1 >= 0) ? items[i].ClosePrice - items[i - 1].ClosePrice : 0;
              if (i >= currentIndex - Consts.RSI_PERIOD_10)
              {
                if (priceDiff > 0)
                  sumClosePriceGain10 += priceDiff;
                else if (priceDiff < 0)
                  sumClosePriceLoss10 += Math.Abs(priceDiff);
              }
              if (i >= currentIndex - Consts.RSI_PERIOD_14)
              {
                if (priceDiff > 0)
                  sumClosePriceGain14 += priceDiff;
                else if (priceDiff < 0)
                  sumClosePriceLoss14 += Math.Abs(priceDiff);
              }
              #endregion

              #region Local Max, Min
              if (i >= currentIndex - Consts.LOCAL_MAX_PERIOD_10)
              {
                maxClosePrice10 = (maxClosePrice10 < items[i].ClosePrice) ? items[i].ClosePrice : maxClosePrice10;
              }
              if (i >= currentIndex - Consts.LOCAL_MAX_PERIOD_20)
              {
                maxClosePrice20 = (maxClosePrice20 < items[i].ClosePrice) ? items[i].ClosePrice : maxClosePrice20;
              }
              if (i >= currentIndex - Consts.LOCAL_MAX_PERIOD_50)
              {
                maxClosePrice50 = (maxClosePrice50 < items[i].ClosePrice) ? items[i].ClosePrice : maxClosePrice50;
              }
              if (i >= currentIndex - Consts.LOCAL_MIN_PERIOD_10)
              {
                minClosePrice10 = (minClosePrice10 > items[i].ClosePrice) ? items[i].ClosePrice : minClosePrice10;
              }
              if (i >= currentIndex - Consts.LOCAL_MIN_PERIOD_20)
              {
                minClosePrice20 = (minClosePrice20 > items[i].ClosePrice) ? items[i].ClosePrice : minClosePrice20;
              }
              if (i >= currentIndex - Consts.LOCAL_MIN_PERIOD_50)
              {
                minClosePrice50 = (minClosePrice50 > items[i].ClosePrice) ? items[i].ClosePrice : minClosePrice50;
              }
              #endregion
            }
            #endregion

            if (currentIndex < items.Count - 1)
            {
              var priceDiff = items[nextIndex].ClosePrice - items[nextIndex].OpenPrice;
              item.model.YActual = (priceDiff > 0) ? Consts.CLASS_UP : Consts.CLASS_DOWN;
            }
            if (currentIndex > 0)
            {
              item.model.IsCloseGreaterToday = item.model.ClosePrice > items[prevIndex].ClosePrice;
            }

            #region Calculate SMA
            item.model.SMA_10 = NumberUtils.Round(sumClosePrice10 / Consts.SMA_PERIOD_10);
            item.model.SMA_20 = NumberUtils.Round(sumClosePrice20 / Consts.SMA_PERIOD_20);
            item.model.SMA_50 = NumberUtils.Round(sumClosePrice50 / Consts.SMA_PERIOD_50);
            item.model.SMA_100 = NumberUtils.Round(sumClosePrice100 / Consts.SMA_PERIOD_100);
            item.model.SMA_200 = NumberUtils.Round(sumClosePrice200 / Consts.SMA_PERIOD_200);
            #endregion

            #region Calculate EMA
            if (prevIndex > 0)
            {
              // EMA for t1 = Price of t1
              // EMA for > t1 = Alpha * Price + (1 - Alpha) * Previous EMA
              // where Alpha = 2 / (N + 1)
              var alpha5 = 2m / (Consts.EMA_PERIOD_5 + 1);
              var alpha9 = 2m / (Consts.EMA_PERIOD_9 + 1);
              var alpha10 = 2m / (Consts.EMA_PERIOD_10 + 1);
              var alpha12 = 2m / (Consts.EMA_PERIOD_12 + 1);
              var alpha26 = 2m / (Consts.EMA_PERIOD_26 + 1);
              var prevEma5 = (items[prevIndex].EMA_5 > 0) ? items[prevIndex].EMA_5 : items[currentIndex].ClosePrice;
              var prevEma9 = (items[prevIndex].EMA_9 > 0) ? items[prevIndex].EMA_9 : items[currentIndex].ClosePrice;
              var prevEma10 = (items[prevIndex].EMA_10 > 0) ? items[prevIndex].EMA_10 : items[currentIndex].ClosePrice;
              var prevEma12 = (items[prevIndex].EMA_12 > 0) ? items[prevIndex].EMA_12 : items[currentIndex].ClosePrice;
              var prevEma26 = (items[prevIndex].EMA_26 > 0) ? items[prevIndex].EMA_26 : items[currentIndex].ClosePrice;
              item.model.EMA_5 = (alpha5 * item.model.ClosePrice) + ((1 - alpha5) * prevEma5);
              item.model.EMA_9 = (alpha9 * item.model.ClosePrice) + ((1 - alpha9) * prevEma9);
              item.model.EMA_10 = (alpha10 * item.model.ClosePrice) + ((1 - alpha10) * prevEma10);
              item.model.EMA_12 = (alpha12 * item.model.ClosePrice) + ((1 - alpha12) * prevEma12);
              item.model.EMA_26 = (alpha26 * item.model.ClosePrice) + ((1 - alpha26) * prevEma26);
            }
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
            var avgGain10 = sumClosePriceGain10 / Consts.RSI_PERIOD_10;
            var avgLoss10 = sumClosePriceLoss10 / Consts.RSI_PERIOD_10;
            var avgGain14 = sumClosePriceGain14 / Consts.RSI_PERIOD_14;
            var avgLoss14 = sumClosePriceLoss14 / Consts.RSI_PERIOD_14;
            if (prevIndex > 0)
            {
              var priceDiff = items[currentIndex].ClosePrice - items[prevIndex].ClosePrice;
              var currentGain = (priceDiff > 0) ? priceDiff : 0;
              var currentLoss = (priceDiff < 0) ? Math.Abs(priceDiff) : 0;
              if (items[prevIndex].AvgGain_10 != null && items[prevIndex].AvgLoss_10 != null)
              {
                avgGain10 = (items[prevIndex].AvgGain_10.GetValueOrDefault() * (Consts.RSI_PERIOD_10 - 1) + currentGain) / Consts.RSI_PERIOD_10;
                avgLoss10 = (items[prevIndex].AvgLoss_10.GetValueOrDefault() * (Consts.RSI_PERIOD_10 - 1) + currentLoss) / Consts.RSI_PERIOD_10;
              }
              if (items[prevIndex].AvgGain_14 != null && items[prevIndex].AvgLoss_14 != null)
              {
                avgGain14 = (items[prevIndex].AvgGain_14.GetValueOrDefault() * (Consts.RSI_PERIOD_14 - 1) + currentGain) / Consts.RSI_PERIOD_14;
                avgLoss14 = (items[prevIndex].AvgLoss_14.GetValueOrDefault() * (Consts.RSI_PERIOD_14 - 1) + currentLoss) / Consts.RSI_PERIOD_14;
              }
            }
            var rs10 = (avgLoss10 > 0) ? avgGain10 / avgLoss10 : 0;
            var rs14 = (avgLoss14 > 0) ? avgGain14 / avgLoss14 : 0;
            var rsi10 = NumberUtils.Round(100 - (100 / (1 + rs10)));
            var rsi14 = NumberUtils.Round(100 - (100 / (1 + rs14)));
            item.model.AvgGain_10 = avgGain10;
            item.model.AvgLoss_10 = avgLoss10;
            item.model.AvgGain_14 = avgGain14;
            item.model.AvgLoss_14 = avgLoss14;
            item.model.RSI_10 = rsi10;
            item.model.RSI_14 = rsi14;
            #endregion

            #region Calculate Local Max, Min
            item.model.IsLocalMax_10 = (maxClosePrice10 == item.model.ClosePrice);
            item.model.IsLocalMax_20 = (maxClosePrice20 == item.model.ClosePrice);
            item.model.IsLocalMax_50 = (maxClosePrice50 == item.model.ClosePrice);
            item.model.IsLocalMin_10 = (minClosePrice10 == item.model.ClosePrice);
            item.model.IsLocalMin_20 = (minClosePrice20 == item.model.ClosePrice);
            item.model.IsLocalMin_50 = (minClosePrice50 == item.model.ClosePrice);
            #endregion

            #region Calculate Bollinger Bands
            // Middle Band = SMA_20
            // Upper Band = SMA_20 + (standard deviation x 2) 
            // Lower Band = SMA_20 - (standard deviation x 2)
            var sumClosePriceDevSqrd20 = 0m;
            stopIndex = currentIndex - Consts.BOLLINGER_PERIOD_20;
            for (var i = prevIndex; i >= stopIndex; i--)
            {
              var deviation = items[i].ClosePrice - item.model.SMA_20;
              sumClosePriceDevSqrd20 += NumberUtils.Pow(deviation, 2);
            }
            var stdDev20 = NumberUtils.Pow(sumClosePriceDevSqrd20 / Consts.BOLLINGER_PERIOD_20, 0.5m);
            item.model.BollingerUpperStvDev1_20 = item.model.SMA_20 + stdDev20;
            item.model.BollingerLowerStvDev1_20 = item.model.SMA_20 - stdDev20;
            item.model.BollingerUpperStvDev2_20 = item.model.SMA_20 + stdDev20 * 2;
            item.model.BollingerLowerStvDev2_20 = item.model.SMA_20 - stdDev20 * 2;
            #endregion

            #region Check criteria
            // Check on minimum criteria
            var isValid = true;
            isValid = isValid && currentIndex > Consts.MAX_CALC_PERIOD + request.NoOfPeriods;
            isValid = isValid && currentIndex > lastAddedIndex + request.NoOfPeriods; // To prevent data leaks
            isValid = isValid && item.model.ADTV_20 >= Consts.TRADING_VOLUME_THRESHOLD;
            isValid = isValid && item.model.ADPV_20 >= Consts.PRICE_VOLUME_THRESHOLD;
            isValid = isValid && (
                item.model.ClosePrice > item.model.BollingerUpperStvDev1_20 ||
                item.model.ClosePrice < item.model.BollingerLowerStvDev1_20);

            // Check on points of interest
            var hasSMA50CrossAboveSMA200 = items[prevIndex].SMA_50 < items[prevIndex].SMA_200 && items[currentIndex].SMA_50 > items[currentIndex].SMA_200;
            var hasSMA50CrossBelowSMA200 = items[prevIndex].SMA_50 > items[prevIndex].SMA_200 && items[currentIndex].SMA_50 < items[currentIndex].SMA_200;
            var hasHighRSI10 = items[currentIndex].RSI_10 > Consts.RSI_HIGH_THRESHOLD;
            var hasLowRSI10 = items[currentIndex].RSI_10 < Consts.RSI_LOW_THRESHOLD;
            var hasHighRSI14 = items[currentIndex].RSI_14 > Consts.RSI_HIGH_THRESHOLD;
            var hasLowRSI14 = items[currentIndex].RSI_14 < Consts.RSI_LOW_THRESHOLD;
            var hasHighLocal = items[currentIndex].IsLocalMax_50;
            var hasLowLocal = items[currentIndex].IsLocalMin_50;
            var hasMedHighRSI10 = items[currentIndex].RSI_10 >= Consts.RSI_MED_HIGH_THRESHOLD && items[currentIndex].RSI_10 < Consts.RSI_HIGH_THRESHOLD;
            var hasMedLowRSI10 = items[currentIndex].RSI_10 <= Consts.RSI_MED_LOW_THRESHOLD && items[currentIndex].RSI_10 > Consts.RSI_LOW_THRESHOLD;
            if (request.Version == Consts.CHART_V1)
            {
              // v1
              isValid = isValid && (
                hasSMA50CrossAboveSMA200 ||
                hasSMA50CrossBelowSMA200 ||
                hasHighRSI10 ||
                hasHighRSI14 ||
                hasLowRSI10 ||
                hasLowRSI14 ||
                hasHighLocal ||
                hasLowLocal);
            }
            else if (request.Version == Consts.CHART_V3)
            {
              // v3
              isValid = isValid && (hasMedHighRSI10 || hasMedLowRSI10);
            }

            // Force require Dow Jones if Charts for a stock exists for that day
            if (request.Symbol == Consts.SYMBOL_DOW_JONES || request.Symbol == Consts.SYMBOL_DOW_ETF)
            {
              var otherChart = DB.ChartImage.FirstOrDefault(c =>
                c.Symbol != Consts.SYMBOL_DOW_JONES &&
                c.Symbol != Consts.SYMBOL_DOW_ETF &&
                c.PriceDate == items[currentIndex].PriceDate);
              isValid = (otherChart != null);
            }

            // Check if ChartImage already exists
            if (isValid)
            {
              var orgChartImage = GetChartImage(request.Symbol, items[currentIndex].PriceDate, request.Version);
              isValid = isValid && orgChartImage == null;

              // To maintain sparsity of charts
              if (!isValid)
              {

                #region Temp Log for Analysis
                //var current = items[currentIndex];
                //currentTechs.Add(new TechnicalItem()
                //{
                //  LogType = "current",
                //  Yactual = current.YActual.GetValueOrDefault(),
                //  Symbol = current.Symbol,
                //  PriceDate = current.PriceDate,
                //  ClosePrice = current.ClosePrice,
                //  Rsi10 = current.RSI_10,
                //  Rsi14 = current.RSI_14,
                //  Sma50 = current.SMA_50,
                //  Sma200 = current.SMA_200,
                //  Ema5 = current.EMA_5,
                //  Ema9 = current.EMA_9,
                //  Ema10 = current.EMA_10,
                //  Ema12 = current.EMA_12,
                //  Ema26 = current.EMA_26,
                //  IsLocalMax50 = current.IsLocalMax_50,
                //  IsLocalMin50 = current.IsLocalMin_50,
                //  BollingerUpperStvDev120 = current.BollingerUpperStvDev1_20,
                //  BollingerLowerStvDev120 = current.BollingerLowerStvDev1_20,
                //  BollingerUpperStvDev220 = current.BollingerUpperStvDev2_20,
                //  BollingerLowerStvDev220 = current.BollingerLowerStvDev2_20
                //});
                //var previous = items[prevIndex];
                //prevTechs.Add(new TechnicalItem()
                //{
                //  LogType = "previous",
                //  Yactual = previous.YActual.GetValueOrDefault(),
                //  Symbol = previous.Symbol,
                //  PriceDate = previous.PriceDate,
                //  ClosePrice = previous.ClosePrice,
                //  Rsi10 = previous.RSI_10,
                //  Rsi14 = previous.RSI_14,
                //  Sma50 = previous.SMA_50,
                //  Sma200 = previous.SMA_200,
                //  Ema5 = previous.EMA_5,
                //  Ema9 = previous.EMA_9,
                //  Ema10 = previous.EMA_10,
                //  Ema12 = previous.EMA_12,
                //  Ema26 = previous.EMA_26,
                //  IsLocalMax50 = previous.IsLocalMax_50,
                //  IsLocalMin50 = previous.IsLocalMin_50,
                //  BollingerUpperStvDev120 = previous.BollingerUpperStvDev1_20,
                //  BollingerLowerStvDev120 = previous.BollingerLowerStvDev1_20,
                //  BollingerUpperStvDev220 = previous.BollingerUpperStvDev2_20,
                //  BollingerLowerStvDev220 = previous.BollingerLowerStvDev2_20
                //});
                #endregion

                lastAddedIndex = currentIndex;
              }
            }

            if (isValid)
            {
              lastAddedIndex = currentIndex;
              ret.Add(items.GetRange(currentIndex - request.NoOfPeriods + 1, request.NoOfPeriods));
            }
            #endregion
          }
        }
      }


      // ========== Temp Log for Analysis
      Insert<TechnicalItem>(currentTechs);
      Insert<TechnicalItem>(prevTechs);



      ret = ret.Take(request.Take).ToList();
      LogUtils.Debug($"END-{nameof(GetPeriodsOfStockPrices)}, Elasped={sp.Elapsed}, Count={ret.Count}, LastPriceDate={ret.LastOrDefault()?.LastOrDefault()?.PriceDate}");
      sp.Stop();

      return ret;
    }

    public void CreateCsvForPrediction(
      string fileName,
      DateTime dateFrom,
      DateTime dateTo,
      bool isBetween,
      int version,
      bool isExcludeNullYActual)
    {
      var query = DB.ChartImage.AsQueryable();
      query = query.Where(c => c.Version == version);
      query = query.Where(c => c.Symbol != Consts.SYMBOL_DOW_JONES && c.Symbol != Consts.SYMBOL_DOW_ETF);
      if (isBetween)
      {
        query = query.Where(c => c.PriceDate >= dateFrom && c.PriceDate <= dateTo);
      }
      else
      {
        query = query.Where(c => c.PriceDate <= dateFrom || c.PriceDate >= dateTo);
      }
      if (isExcludeNullYActual)
      {
        query = query.Where(c => c.YActual != null);
      }
      var badSymbols = new string[] {
        "JPM", "FISV", "CBRE", "OMC", "PFE", "APA", "CMA", "CMG", "MGM", "FIS", "AGEN",
        "TMO", "BGS", "EWBC", "NAVI", "PNC", "WYND", "ALL", "HRB", "SEAS", "SCHW",
        "NSP", "PFG", "RCL", "AIG", "FICO", "CVX", "AEP", "AMTD", "CDW", "DFS",
        "WDAY", "UNH", "AES", "AFL", "BAH", "ALLY", "ABT", "C", "GS", "NWSA",
        "AWK", "WFC", "BAC", "AMGN", "MO", "BX", "XOM", "IVV", "GEL", "BRK-B",
        "PGR", "BK", "PGRE", "CHK", "ETFC", "SABR", "PRU", "FRC", "SIX", "FNF",
        "MS", "COF", "EIX", "PMT", "KOS", "MET", "CIT", "USB", "NYT", "ANTM" };
      query = query.Where(c => !badSymbols.Contains(c.Symbol));

      CsvUtils.WriteCsv(fileName, query.ToList());
    }

    public int SavePredictions(string fileName)
    {
      var ret = 0;
      if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
      {
        var models = new List<ChartImage>();
        var predictions = CsvUtils.ParseCsv<ChartImage, MapPrediction>(fileName);
        if (predictions != null)
        {
          foreach (var prediction in predictions)
          {
            var org = GetChartImage(prediction.Symbol, prediction.PriceDate, prediction.Version);
            if (org != null)
            {
              org.YPredicted = prediction.YPredicted;
              org.YPredictedProbability = prediction.YPredictedProbability;
              models.Add(org);
            }
          }
          ret += Update<ChartImage>(models);
        }
      }
      return ret;
    }
  }
}
