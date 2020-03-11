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
  public class SimfinFinancialBalanceCsvService : BaseCsvService<Financial, MapSimfinFinancialBalance>
  {
    protected override int Save(string fileName, List<Financial> records)
    {
      var ret = 0;
      if (records != null)
      {
        LogUtils.Debug($"{records.Count}");
        var financials = DB.Financial.ToList();
        var index = 0;
        foreach (var financial in financials)
        {
          var record = records.FirstOrDefault(r =>
            r.Symbol == financial.Symbol &&
            r.Year == financial.Year &&
            r.Quarter == financial.Quarter);
          if (record != null)
          {
            financial.CurrentAssets = record.CurrentAssets;
            financial.TtlAssets = record.TtlAssets;
            financial.CurrentLiabilities = record.CurrentLiabilities;
            financial.TtlLiabilities = record.TtlLiabilities;
            financial.LongTermDebt = record.LongTermDebt;
            financial.TtlEquity = record.TtlEquity;
          }
          if(index % Consts.DEFAULT_SKIP == 0)
          {
            Update<Financial>(new List<Financial>());
            LogUtils.Debug($"Current index = {index}");
          }
          index += 1;
        }
      }
      return ret;
    }
  }
}
