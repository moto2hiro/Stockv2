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
    [Test()]
    public void Run_AddStockPriceFromCsv()
    {
      var filePaths = Directory.GetFiles($"{DOWNLOAD_PATH}Temp");
      foreach(var filePath in filePaths)
      {
        var symbol = Path.GetFileNameWithoutExtension(filePath);
        LogUtils.Debug(symbol);
        new StockService().AddStockPriceFromCsv(symbol, filePath);
      }
    }

    [Test()]
    public void Run_GetRecentPriceHistoryForAllStocks()
    {
      var service = new StockService();
      service.GetRecentPriceHistoryForAllStocks();
    }

    [Test()]
    public void Run_CreateCsvForPrediction()
    {
      var fileName = $"{DOWNLOAD_PATH}charts_1_50_o20200215.csv";
      var dateFrom = new DateTime(2020, 2, 15);
      var dateTo = new DateTime(2023, 1, 1);
      var isBetween = true;
      var version = Consts.CHART_V1;
      new StockService().CreateCsvForPrediction(
        fileName, 
        dateFrom, 
        dateTo,
        isBetween,
        version);
    }
  }
}
