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

      LogUtils.Debug(symbol);

      //var org = new SymbolMaster()
      //{
      //  Symbol = symbol,
      //  Name = "",
      //  Status = Consts.STATUS_ACTIVE,
      //  Instrument = Consts.INSTRUMENT_STOCK
      //};
      //ret += Insert(org);

      var orgSymbol = DB.SymbolMaster.FirstOrDefault(s => s.Symbol == symbol);
      if (orgSymbol == null)
      {
        return ret;
      }

      var orgPrice = DB.StockPrice.FirstOrDefault(s => s.SymbolId == orgSymbol.Id);
      if (orgPrice != null)
      {
        return ret;
      }

      foreach (var record in records)
      {
        record.SymbolId = orgSymbol.Id;
        record.PriceDate = record.PriceDate
          .AddHours(record.Hours)
          .AddMinutes(record.Minutes);
      }

      //ret += Insert<StockPrice>(records);

      return ret;
    }
  }
}
