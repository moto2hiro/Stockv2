using CsvHelper.Configuration;
using Stock.Services.Models.EF;
using Stock.Services.Utils;

namespace Stock.Services.Models.CsvHelper
{
  public class MapPrediction : ClassMap<ChartImage>
  {
    public MapPrediction()
    {
      Map(row => row.Symbol).Name("Symbol");
      Map(row => row.PriceDate).Name("PriceDate");
      Map(row => row.YPredicted).ConvertUsing(row => StringUtils.ToInt(row.GetField("YPredicted")));
      Map(row => row.YPredictedProbability).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("YPredictedProbability")));
      Map(row => row.Version).ConvertUsing(row => StringUtils.ToInt(row.GetField("Version")));
    }
  }
}
