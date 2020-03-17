using Stock.Services.Utils;
using System;

namespace Stock.Services.Models.CsvHelper
{
  public class MapIntrinioFinancialBalance : MapSimfinFinancialBase
  {
    public MapIntrinioFinancialBalance()
    {
      Map(row => row.Symbol).Name("ticker");
      Map(row => row.CompanyName).Name("name");
      Map(row => row.Year).ConvertUsing(row => StringUtils.ToInt(row.GetField("fiscal_year")));
      Map(row => row.FiscalPeriod).Name("fiscal_period");
      Map(row => row.QuarterEndDate).Name("end_date");
      Map(row => row.FileDate).ConvertUsing(row => DateUtils.ToDateTime(row.GetField("filing_date"), DateUtils.Format_yyyyMddHHmmss));
      Map(row => row.CurrentAssets).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("totalcurrentassets")));
      Map(row => row.TtlAssets).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("totalassets")));
      Map(row => row.CurrentLiabilities).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("totalcurrentliabilities")));
      Map(row => row.TtlLiabilities).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("totalliabilities")));
      Map(row => row.TtlEquity).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("totalequity")));
    }
  }
}
