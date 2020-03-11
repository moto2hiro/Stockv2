using CsvHelper.Configuration;
using Stock.Services.Models.EF;
using Stock.Services.Utils;

namespace Stock.Services.Models.CsvHelper
{
  public class MapStockpupFinancial : ClassMap<Financial>
  {
    public MapStockpupFinancial()
    {
      Map(row => row.ReportDate).Name("Quarter end");
      Map(row => row.FreeCashFlow).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("Free cash flow per share")));
      Map(row => row.DivPayoutRatio).ConvertUsing(row => StringUtils.ToDecimal(row.GetField("Dividend payout ratio")));
    }
  }
}
