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
    public void Run_SaveCsv_IntrinioFinancialIncomeCsvService()
    {
      var fileName = $"{DOWNLOAD_PATH}US_INDU_INCOME_STATEMENT.csv";
      new StockService().SaveCsv<Financial, MapIntrinioFinancialIncome>(fileName);
    }

    [Test()]
    public void Run_SaveCsv_IntrinioFinancialBalanceCsvService()
    {
      var fileName = $"{DOWNLOAD_PATH}US_INDU_BALANCE_SHEET_STATEMENT.csv";
      new StockService().SaveCsv<Financial, MapIntrinioFinancialBalance>(fileName);
    }

    [Test()]
    public void Run_SaveCsv_IntrinioFinancialCalculationCsvService()
    {
      var fileName = $"{DOWNLOAD_PATH}US_INDU_CALCULATIONS.csv";
      new StockService().SaveCsv<Financial, MapIntrinioFinancialCalculation>(fileName);
    }

    [Test()]
    public void Run_SaveCsv_DukascopyWorldPriceCsvService()
    {
      var fileName = $"{DOWNLOAD_PATH}^SSMI.csv";
      new StockService().SaveCsv<WorldPrice, MapDukascopyWorldPrice>(fileName);
    }

    [Test()]
    public void Run_YahooStockPriceCsvService()
    {
      var fileName = $"{DOWNLOAD_PATH}DIA.csv";
      new StockService().SaveCsv<StockPrice, MapYahooStockPrice>(fileName);
    }

    [Test()]
    public void Run_KibotStockPriceCsvService()
    {
      var fileName = $"{DOWNLOAD_PATH}A.txt";
      new StockService().SaveCsv<StockPrice, MapKibotStockPrice>(fileName);
    }
  }
}
