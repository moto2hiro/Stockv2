using CsvHelper.Configuration;
using Stock.Services.Models.EF;
using Stock.Services.Utils;

namespace Stock.Services.Models.CsvHelper
{
  public class MapKibotStockPrice : ClassMap<StockPrice>
  {
    public MapKibotStockPrice()
    {
      Map(row => row.OpenPrice).Index(2);
      Map(row => row.HighPrice).Index(3);
      Map(row => row.LowPrice).Index(4);
      Map(row => row.ClosePrice).Index(5);
      Map(row => row.Volume).Index(6);
      Map(row => row.PriceDate).Index(0);
      Map(row => row.TimeStr).Index(1);
    }
  }
}
