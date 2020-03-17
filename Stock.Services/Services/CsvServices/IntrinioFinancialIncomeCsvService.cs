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
        records = records.Where(r =>
          !string.IsNullOrEmpty(r.Symbol) &&
          !string.IsNullOrEmpty(r.CompanyName) &&
          !string.IsNullOrEmpty(r.FiscalPeriod) &&
          !r.FiscalPeriod.EndsWith(Consts.INTRINIO_PERIOD_SUFFIX_YTD) &&
          !r.FiscalPeriod.EndsWith(Consts.INTRINIO_PERIOD_SUFFIX_TTM) &&
          !r.FiscalPeriod.EndsWith(Consts.INTRINIO_PERIOD_SUFFIX_FY)).ToList();

        var skip = 0;
        while (skip < records.Count)
        {
          //var models = records.Skip(skip).Take(Consts.DEFAULT_SKIP).ToList();
          //foreach(var model in models)
          //{
          //  var symbol = DB.SymbolMaster.FirstOrDefault(s => s.Symbol == model.Symbol);
          //  model.SymbolId = symbol.Id;
          //  model.Quarter = StringUtils.ToInt(model.FiscalPeriod.Replace(Consts.INTRINIO_PERIOD_PREFIX, ""));
          //}
          //ret += Insert<Financial>(models);
          //skip += Consts.DEFAULT_SKIP;
          LogUtils.Debug($"skip count = {skip}");
        }
      }
      return ret;
    }
  }
}
