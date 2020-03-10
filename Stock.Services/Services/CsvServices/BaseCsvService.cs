using CsvHelper.Configuration;
using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Stock.Services.Services.CsvServices
{
  public abstract class BaseCsvService<T, TMap> : BaseService where TMap : ClassMap
  {
    protected abstract int Save(List<T> records);

    public int SaveCsv(string fileName)
    {
      var ret = 0;
      if (File.Exists(fileName))
      {
        var records = CsvUtils.ParseCsv<T, TMap>(fileName);
        if (records != null)
        {
          ret += Save(records);
        }
      }
      return ret;
    }
  }
}
