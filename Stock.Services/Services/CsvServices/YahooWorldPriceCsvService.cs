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
  public class YahooWorldPriceCsvService : BaseCsvService<WorldPrice, MapYahooWorldPrice>
  {
    protected override int Save(string fileName, List<WorldPrice> records)
    {
      var ret = 0;
      if (records == null)
      {
        return ret;
      }
      var symbol = Path.GetFileNameWithoutExtension(fileName);
      if (string.IsNullOrEmpty(symbol))
      {
        return ret;
      }

      records.ForEach(r => r.Symbol = symbol);

      //ret += Insert<WorldPrice>(records);

      return ret;
    }
  }
}
