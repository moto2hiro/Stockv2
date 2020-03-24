using CsvHelper.Configuration;
using Stock.Services.Models.EF;
using Stock.Services.Utils;

namespace Stock.Services.Models.CsvHelper
{
  public class MapInvestingWorldPrice : ClassMap<WorldPrice>
  {
    public MapInvestingWorldPrice()
    {
      Map(row => row.OpenPrice).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("Open").Replace(",", "")));
      Map(row => row.ClosePrice).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("Price").Replace(",", "")));
      Map(row => row.PriceDate).Name("Date");
    }
  }
}
