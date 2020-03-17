using Stock.Services.Utils;
using System;

namespace Stock.Services.Models.CsvHelper
{
  public class MapIntrinioFinancialIncome : MapSimfinFinancialBase
  {
    public MapIntrinioFinancialIncome()
    {
      Map(row => row.Symbol).Name("ticker");
      Map(row => row.CompanyName).Name("name");
      Map(row => row.Year).ConvertUsing(row => StringUtils.ToInt(row.GetField("fiscal_year")));
      Map(row => row.FiscalPeriod).Name("fiscal_period");
      Map(row => row.QuarterEndDate).Name("end_date");
      Map(row => row.FileDate).ConvertUsing(row => DateUtils.ToDateTime(row.GetField("filing_date"), DateUtils.Format_yyyyMddHHmmss));
      Map(row => row.Revenue).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("totalrevenue")));
      Map(row => row.GrossProfit).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("totalgrossprofit")));
      Map(row => row.OperatingIncome).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("totaloperatingincome")));
      Map(row => row.NetIncome).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("netincome")));
    }
  }
}
