﻿using NUnit.Framework;
using Stock.Services;
using Stock.Services.Services;
using Stock.Services.Utils;
using System;
using System.IO;

namespace Stock.Tests.JobRuns
{
  /// <summary>
  /// To manually run jobs
  /// </summary>
  [TestFixture()]
  public class JobRuns : BaseTest
  {
    [Test()]
    public void Run_AddStockPriceFromCsv()
    {
      var fileNames = Directory.GetFiles($"{DOWNLOAD_PATH}Temp");
      foreach (var fileName in fileNames)
      {
        var symbol = Path.GetFileNameWithoutExtension(fileName);
        LogUtils.Debug(symbol);
        new StockService().AddStockPriceFromCsv(symbol, fileName);
      }
    }

    [Test()]
    public void Run_GetRecentPriceHistoryForAllStocks()
    {
      var service = new StockService();
      service.GetRecentPriceHistoryForAllStocks();
    }

    [Test()]
    public void Run_SaveChartImagesV2()
    {
      var service = new StockService();
      service.CreateChartImagesV2();
    }

    [Test()]
    public void Run_CreateCsvForPrediction()
    {
      var fileName = $"{DOWNLOAD_PATH}v2_charts_1_50_wo2008.csv";
      var dateFrom = new DateTime(2008, 1, 1);
      var dateTo = new DateTime(2009, 1, 1);
      var isBetween = false;
      var version = Consts.CHART_V2;
      var isExcludeNullYActual = true;
      new StockService().CreateCsvForPrediction(
        fileName,
        dateFrom,
        dateTo,
        isBetween,
        version,
        isExcludeNullYActual);
    }

    [Test()]
    public void Run_SavePredictions()
    {
      var fileName = $"{DOWNLOAD_PATH}predictions_charts_resnet_1_50_20200305.csv";
      new StockService().SavePredictions(fileName);
    }

    #region Query
    [Test()]
    public void Run_GetPastAvgs()
    {
      var resp = new QueryService().GetPastAvgs(Consts.CHART_V1);
      if (resp != null && resp.PastAvgs != null)
      {
        foreach(var pastAvg in resp.PastAvgs)
        {
          LogUtils.Debug($"Past Avg {pastAvg.Period} days = {pastAvg.Avg}% correct.");
        }
      }
    }

    [Test()]
    public void Run_GetPastAvgs_Stock()
    {
      var symbol = "BIG";
      var resp = new QueryService().GetPastAvgs(Consts.CHART_V1, symbol);
      if (resp != null && resp.PastAvgs != null)
      {
        foreach (var pastAvg in resp.PastAvgs)
        {
          LogUtils.Debug($"{symbol} Past Avg {pastAvg.Period} days = {pastAvg.Avg}% correct.");
        }
      }
    }

    [Test()]
    public void Run_GetPredictions()
    {
      var dateFrom = DateTime.Now.AddDays(-10);
      var predictions = new QueryService().GetPredictions(dateFrom);
      if (predictions != null)
      {
        foreach (var prediction in predictions)
        {
          LogUtils.Debug($"{prediction.Symbol} on {prediction.PriceDate:yyyy-MM-dd} Prediction = {prediction.YPredicted} at {prediction.YPredictedProbability}% probability.");
        }
      }
    }
    #endregion
  }
}
