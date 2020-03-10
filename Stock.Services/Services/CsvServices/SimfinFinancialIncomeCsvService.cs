using CsvHelper.Configuration;
using Stock.Services.Models.CsvHelper;
using Stock.Services.Models.EF;
using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Stock.Services.Services.CsvServices
{
  public class SimfinFinancialIncomeCsvService : BaseCsvService<Financial, MapSimfinFinancialIncome>
  {
    protected override int Save(List<Financial> records)
    {
      LogUtils.Debug($"{records.Count}");
      return 0;
    }
  }
}
