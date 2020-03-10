using CsvHelper.Configuration;
using Stock.Services.Models.EF;
using Stock.Services.Utils;

namespace Stock.Services.Models.CsvHelper
{
  public class MapSimfinFinancialBalance : MapSimfinFinancialBase
  {
    public MapSimfinFinancialBalance()
    {
      Map(row => row.Symbol).Name("Ticker");
      Map(row => row.Year).ConvertUsing(row => StringUtils.ToInt(row.GetField("Fiscal Year")));
      Map(row => row.Quarter).ConvertUsing(row => StringUtils.ToInt(CleanQuarter(row.GetField("Fiscal Period"))));
      Map(row => row.ReportDate).Name("Report Date");
      Map(row => row.PublishDate).Name("Publish Date");
    }
  }
}
