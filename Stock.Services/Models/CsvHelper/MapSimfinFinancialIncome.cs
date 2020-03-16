using CsvHelper.Configuration;
using Stock.Services.Models.EF;
using Stock.Services.Utils;

namespace Stock.Services.Models.CsvHelper
{
  public class MapSimfinFinancialIncome : MapSimfinFinancialBase
  {
    public MapSimfinFinancialIncome()
    {
      //Map(row => row.Symbol).Name("Ticker");
      //Map(row => row.Year).ConvertUsing(row => StringUtils.ToInt(row.GetField("Fiscal Year")));
      //Map(row => row.Quarter).ConvertUsing(row => StringUtils.ToInt(CleanQuarter(row.GetField("Fiscal Period"))));
      //Map(row => row.ReportDate).Name("Report Date");
      //Map(row => row.PublishDate).Name("Publish Date");
      //Map(row => row.Shares).ConvertUsing(row => StringUtils.ToInt(row.GetField("Shares (Basic)")));
      //Map(row => row.Revenue).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("Revenue")));
      //Map(row => row.GrossProfit).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("Gross Profit")));
      //Map(row => row.NetIncome).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("Net Income")));
    }
  }
}
