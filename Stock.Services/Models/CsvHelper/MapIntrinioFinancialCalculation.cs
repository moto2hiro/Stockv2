using Stock.Services.Utils;
using System;

namespace Stock.Services.Models.CsvHelper
{
  public class MapIntrinioFinancialCalculation : MapSimfinFinancialBase
  {
    public MapIntrinioFinancialCalculation()
    {
      Map(row => row.Symbol).Name("ticker");
      Map(row => row.CompanyName).Name("name");
      Map(row => row.Year).ConvertUsing(row => StringUtils.ToInt(row.GetField("fiscal_year")));
      Map(row => row.FiscalPeriod).Name("fiscal_period");
      //Map(row => row.QuarterEndDate).Name("end_date");
      //Map(row => row.FileDate).ConvertUsing(row => DateUtils.ToDateTime(row.GetField("filing_date"), DateUtils.Format_yyyyMddHHmmss));
      Map(row => row.MarketCap).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("marketcap")));
      Map(row => row.RevenueGrowth).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("revenuegrowth")));
      Map(row => row.RevenueQqGrowth).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("revenueqoqgrowth")));
      Map(row => row.NopatGrowth).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("nopatgrowth")));
      Map(row => row.NopatQqGrowth).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("nopatqoqgrowth")));
      Map(row => row.NetIncomeGrowth).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("netincomegrowth")));
      Map(row => row.NetIncomeQqGrowth).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("netincomeqoqgrowth")));
      Map(row => row.FreeCashFlow).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("freecashflow")));
      Map(row => row.CurrentRatio).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("currentratio")));
      Map(row => row.DebtToEquityRatio).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("debttoequity")));
      Map(row => row.PeRatio).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("pricetoearnings")));
      Map(row => row.PbRatio).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("pricetobook")));
      Map(row => row.DivPayoutRatio).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("divpayoutratio")));
      Map(row => row.Roe).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("roe")));
      Map(row => row.Roa).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("roa")));
    }
  }
}
