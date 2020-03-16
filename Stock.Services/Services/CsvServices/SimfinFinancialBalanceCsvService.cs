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
  public class SimfinFinancialBalanceCsvService : BaseCsvService<Financial, MapSimfinFinancialBalance>
  {
    protected override int Save(string fileName, List<Financial> records)
    {
      return 0;
    }
  }
}
