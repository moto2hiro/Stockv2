using CsvHelper.Configuration;
using Stock.Services.Models.EF;
using Stock.Services.Utils;

namespace Stock.Services.Models.CsvHelper
{
  public class MapYahooWorldPrice : ClassMap<WorldPrice>
  {
    public MapYahooWorldPrice()
    {
      Map(row => row.OpenPrice).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("Open")));
      Map(row => row.ClosePrice).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("Close")));
      Map(row => row.PriceDate).Name("Date");
    }
  }
}
