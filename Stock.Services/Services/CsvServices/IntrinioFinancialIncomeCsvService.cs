using CsvHelper.Configuration;
using Stock.Services.Models.CsvHelper;
using Stock.Services.Models.EF;
using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Stock.Services.Services.CsvServices
{
  public class IntrinioFinancialIncomeCsvService : BaseCsvService<Financial, MapIntrinioFinancialIncome>
  {
    protected override int Save(string fileName, List<Financial> records)
    {
      var ret = 0;
      if (records != null)
      {
        LogUtils.Debug($"Save Financial Income = {records.Count}");
        var query = from r in records
                    join s in DB.SymbolMaster
                      on r.Symbol equals s.Symbol into rs
                    from s in rs.DefaultIfEmpty()
                    where
                      !string.IsNullOrEmpty(r.Symbol) &&
                      !string.IsNullOrEmpty(r.CompanyName) &&
                      !string.IsNullOrEmpty(r.FiscalPeriod) &&
                      !r.FiscalPeriod.EndsWith(Consts.INTRINIO_PERIOD_SUFFIX_YTD) &&
                      !r.FiscalPeriod.EndsWith(Consts.INTRINIO_PERIOD_SUFFIX_TTM) &&
                      !r.FiscalPeriod.EndsWith(Consts.INTRINIO_PERIOD_SUFFIX_FY) &&
                      s != null
                    select new Financial
                    {
                      SymbolId = s.Id,
                      Symbol = r.Symbol,
                      Year = r.Year,
                      Quarter = StringUtils.ToInt(r.FiscalPeriod.Replace(Consts.INTRINIO_PERIOD_PREFIX, "")),
                      FiscalPeriod = r.FiscalPeriod,
                      QuarterEndDate = r.QuarterEndDate,
                      FileDate = r.FileDate,
                      Revenue = r.Revenue,
                      GrossProfit = r.GrossProfit,
                      OperatingIncome = r.OperatingIncome,
                      NetIncome = r.NetIncome,
                    };
        var items = query.ToList();

        LogUtils.Debug($"record count = {items.Count}");
        ret += Insert<Financial>(items);
      }
      return ret;
    }
  }
}
