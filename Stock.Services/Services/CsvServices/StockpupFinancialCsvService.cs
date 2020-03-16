using CsvHelper.Configuration;
using Stock.Services.Models.CsvHelper;
using Stock.Services.Models.EF;
using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Stock.Services.Services.CsvServices
{
  public class StockpupFinancialCsvService : BaseCsvService<Financial, MapStockpupFinancial>
  {
    protected override int Save(string fileName, List<Financial> records)
    {
      var ret = 0;
      //if (!string.IsNullOrEmpty(fileName) && records != null)
      //{
      //  var symbol = Path.GetFileNameWithoutExtension(fileName).Split('_')[0];
      //  LogUtils.Debug($"{symbol} {records.Count}");
      //  foreach (var record in records)
      //  {
      //    var financial = DB.Financial.FirstOrDefault(f => f.Symbol == symbol && record.ReportDate == f.ReportDate);
      //    if (financial != null)
      //    {
      //      financial.FreeCashFlow = record.FreeCashFlow;
      //      financial.DivPayoutRatio = record.DivPayoutRatio;
      //    }
      //    Update(financial);
      //  }
      //}
      return ret;
    }
  }
}
