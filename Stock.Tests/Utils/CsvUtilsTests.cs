using NUnit.Framework;
using Stock.Services.Models;
using Stock.Services.Models.CsvHelper;
using Stock.Services.Models.EF;
using Stock.Services.Services;
using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace Stock.Tests.Utils
{
  [TestFixture()]
  public class CsvUtilsTestsTests : BaseTest
  {
    [Test()]
    public void WriteCsv_Should_Write_Csv()
    {
      // ARRANGE
      var fileName = $"{DOWNLOAD_PATH}{DateTime.Now:yyyyMMdd_HHmmss}.csv";
      var records = new List<SymbolMaster>();
      for(var i = 0; i < 50; i++)
      {
        records.Add(new SymbolMaster() { 
          Id = i,
          Symbol = "MSFT",
          Name = "Microsoft"
        });
      }

      // ACT
      CsvUtils.WriteCsv(fileName, records);

      // ASSERT
      Assert.IsTrue(File.Exists(fileName));
    }

    [Test()]
    public void ParseCsv_Should_Return_Data()
    {
      // ARRANGE
      var fileName = $"{DOWNLOAD_PATH}^DJI.csv.csv";

      // ACT
      var records = CsvUtils.ParseCsv<StockPrice, MapYahooStockPrice>(fileName);

      // ASSERT
      Assert.IsNotNull(records);
      Assert.IsTrue(records.Count > 0);
    }
  }
}
