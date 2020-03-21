using CsvHelper.Configuration;
using Stock.Services.Models.EF;
using Stock.Services.Utils;
using System.Globalization;

namespace Stock.Services.Models.CsvHelper
{
  public class MapDukascopyWorldPrice : ClassMap<WorldPrice>
  {
    public MapDukascopyWorldPrice()
    {
      Map(row => row.OpenPrice).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("Open")));
      Map(row => row.HighPrice).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("High")));
      Map(row => row.LowPrice).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("Low")));
      Map(row => row.ClosePrice).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("Close")));
      Map(row => row.PriceDate).ConvertUsing(row => 
        DateUtils.ToDateTime(
          row.GetField("Gmt time").Replace(".000", "").Replace(".", "-"), 
          "dd-MM-yyyy HH:mm:ss"));
    }
  }
}
