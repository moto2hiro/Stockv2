using NUnit.Framework;
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
    //[Test()]
    //public void Run_AddStockPriceFromCsv()
    //{
    //  var fileNames = Directory.GetFiles($"{DOWNLOAD_PATH}Temp");
    //  foreach (var fileName in fileNames)
    //  {
    //    var symbol = Path.GetFileNameWithoutExtension(fileName);
    //    LogUtils.Debug(symbol);
    //    new StockService().AddStockPriceFromCsv(symbol, fileName);
    //  }
    //}

    //[Test()]
    //public void Run_GetRecentPriceHistoryForAllStocks()
    //{
    //  var service = new StockService();
    //  service.GetRecentPriceHistoryForAllStocks();
    //}

    //[Test()]
    //public void Run_SaveChartImagesV2()
    //{
    //  var service = new StockService();
    //  service.CreateChartImagesV2();
    //}

    //[Test()]
    //public void Run_CreateCsvForPrediction()
    //{
    //  var fileName = $"{DOWNLOAD_PATH}v1_charts_1_25.csv";
    //  var dateFrom = new DateTime(1800, 1, 1);
    //  var dateTo = new DateTime(2022, 1, 1);
    //  var isBetween = true;
    //  var version = Consts.CHART_V1;
    //  var isExcludeNullYActual = true;
    //  new StockService().CreateCsvForPrediction(
    //    fileName,
    //    dateFrom,
    //    dateTo,
    //    isBetween,
    //    version,
    //    isExcludeNullYActual);
    //}

    //[Test()]
    //public void Run_SavePredictions()
    //{
    //  var fileName = $"{DOWNLOAD_PATH}predictions_charts_resnet_1_50_20200305.csv";
    //  new StockService().SavePredictions(fileName);
    //}

    //#region Query
    //[Test()]
    //public void Run_GetPastAvgs()
    //{
    //  var resp = new QueryService().GetPastAvgs(Consts.CHART_V1);
    //  if (resp != null && resp.PastAvgs != null)
    //  {
    //    foreach (var pastAvg in resp.PastAvgs)
    //    {
    //      var text = string.Format("Past {0} days = {1}% correct for {2}.",
    //        pastAvg.Period,
    //        pastAvg.PctCorrect,
    //        pastAvg.AvgType);
    //      LogUtils.Debug(text);
    //    }
    //  }
    //}

    //[Test()]
    //public void Run_GetPastAvgs_Stock()
    //{
    //  var symbol = "BIG";
    //  var resp = new QueryService().GetPastAvgs(Consts.CHART_V1, symbol);
    //  if (resp != null && resp.PastAvgs != null)
    //  {
    //    foreach (var pastAvg in resp.PastAvgs)
    //    {
    //      var text = string.Format("{0} Past {1} days = {2}% correct for {3}.",
    //        symbol,
    //        pastAvg.Period,
    //        pastAvg.PctCorrect,
    //        pastAvg.AvgType);
    //      LogUtils.Debug(text);
    //    }
    //  }
    //}

    //[Test()]
    //public void Run_GetPredictions()
    //{
    //  var dateFrom = DateTime.Now.AddDays(-10);
    //  var predictions = new QueryService().GetPredictions(dateFrom);
    //  if (predictions != null)
    //  {
    //    foreach (var prediction in predictions)
    //    {
    //      var text = string.Format("{0} {1:yyyy-MM-dd} prediction = {2} at {3}% probability.",
    //        prediction.Symbol,
    //        prediction.PriceDate,
    //        prediction.YPredicted,
    //        prediction.YPredictedProbability);
    //      LogUtils.Debug(text);
    //    }
    //  }
    //}
    //#endregion
  }
}
