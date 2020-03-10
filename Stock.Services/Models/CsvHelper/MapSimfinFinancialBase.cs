using CsvHelper.Configuration;
using Stock.Services.Models.EF;
using Stock.Services.Utils;

namespace Stock.Services.Models.CsvHelper
{
  public abstract class MapSimfinFinancialBase : ClassMap<Financial>
  {
    protected string CleanQuarter(string quarter)
    {
      return (!string.IsNullOrEmpty(quarter)) ? quarter.Replace("Q", "") : "";
    }
  }
}
