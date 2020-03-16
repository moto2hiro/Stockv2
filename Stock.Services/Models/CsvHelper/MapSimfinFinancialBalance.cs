using CsvHelper.Configuration;
using Stock.Services.Models.EF;
using Stock.Services.Utils;

namespace Stock.Services.Models.CsvHelper
{
  public class MapSimfinFinancialBalance : MapSimfinFinancialBase
  {
    public MapSimfinFinancialBalance()
    {
      //Map(row => row.Symbol).Name("Ticker");
      //Map(row => row.Year).ConvertUsing(row => StringUtils.ToInt(row.GetField("Fiscal Year")));
      //Map(row => row.Quarter).ConvertUsing(row => StringUtils.ToInt(CleanQuarter(row.GetField("Fiscal Period"))));
      //Map(row => row.ReportDate).Name("Report Date");
      //Map(row => row.PublishDate).Name("Publish Date");
      //Map(row => row.CurrentAssets).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("Total Current Assets")));
      //Map(row => row.TtlAssets).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("Total Assets")));
      //Map(row => row.CurrentLiabilities).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("Total Current Liabilities")));
      //Map(row => row.TtlLiabilities).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("Total Liabilities")));
      //Map(row => row.LongTermDebt).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("Long Term Debt")));
      //Map(row => row.TtlEquity).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("Total Equity")));
    }
  }
}
