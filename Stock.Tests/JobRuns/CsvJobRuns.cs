using NUnit.Framework;
using Stock.Services;
using Stock.Services.Models.CsvHelper;
using Stock.Services.Models.EF;
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
  public class CsvJobRuns : BaseTest
  {
    [Test()]
    public void Run_SaveCsv_For_SimfinFinancialIncome()
    {
      var fileName = $"{DOWNLOAD_PATH}us-income-quarterly.csv";
      new StockService().SaveCsv<Financial, MapSimfinFinancialIncome>(fileName);
    }

    [Test()]
    public void Run_SaveCsv_For_SimfinFinancialBalance()
    {
      var fileName = $"{DOWNLOAD_PATH}us-balance-quarterly.csv";
      new StockService().SaveCsv<Financial, MapSimfinFinancialBalance>(fileName);
    }

    [Test()]
    public void Run_SaveCsv_For_MapStockpupFinancial()
    {
      var fileNames = Directory.GetFiles($"{DOWNLOAD_PATH}financial");
      foreach (var fileName in fileNames)
      {
        new StockService().SaveCsv<Financial, MapStockpupFinancial>(fileName);
      }
    }
  }
}
