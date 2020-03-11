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
  public class SimfinFinancialIncomeCsvService : BaseCsvService<Financial, MapSimfinFinancialIncome>
  {
    protected override int Save(string fileName, List<Financial> records)
    {
      var ret = 0;
      if (records != null)
      {
        LogUtils.Debug($"Save Financial Income = {records.Count}");
        //var skip = 0;
        //while(skip < records.Count)
        //{
        //  var models = records.Skip(skip).Take(Consts.DEFAULT_SKIP).ToList();
        //  ret += Insert<Financial>(models);
        //  skip += Consts.DEFAULT_SKIP;
        //  LogUtils.Debug($"skip count = {skip}");
        //}
      }
      return ret;
    }
  }
}
