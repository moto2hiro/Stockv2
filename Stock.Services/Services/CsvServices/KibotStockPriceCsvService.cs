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
  public class KibotStockPriceCsvService : BaseCsvService<StockPrice, MapKibotStockPrice>
  {
    protected override bool HasHeaderRecord => false;

    protected override int Save(string fileName, List<StockPrice> records)
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

      //var org = new SymbolMaster()
      //{
      //  org.Symbol = symbol,
      //  org.Name = "",
      //  org.Status = Consts.STATUS_ACTIVE,
      //  org.Instrument = Consts.ins
      //});
      //ret += Insert(org);

      ////var org = DB.SymbolMaster.FirstOrDefault(s => s.Symbol == symbol);
      ////if (org == null)
      ////{
      ////  LogUtils.Debug($"{symbol} not found.");
      ////  return ret;
      ////}

      //records.ForEach(r => r.SymbolId = org.Id);

      //ret += Insert<StockPrice>(records);

      return ret;
    }
  }
}
