using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Stock.Services.Utils
{
  // https://joshclose.github.io/CsvHelper/getting-started
  public class CsvUtils
  {
    public static void WriteCsv<T>(string fileName, List<T> models)
    {
      if(!string.IsNullOrEmpty(fileName) && models != null)
      {
        using (var writer = new StreamWriter(fileName))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
          csv.WriteRecords(models);
        }
      }
    }

    public static List<T> ParseCsv<T, TMap>(string fileName, bool hasHeaderRecord = true) where TMap : ClassMap
    {
      var ret = new List<T>();
      if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
      {
        using (var reader = new StreamReader(fileName))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
          if (csv != null && csv.Configuration != null)
          {
            csv.Configuration.HasHeaderRecord = hasHeaderRecord;
            csv.Configuration.RegisterClassMap<TMap>();
            var records = csv.GetRecords<T>();
            if (records != null)
            {
              ret = records.ToList();
            }
          }
        }
      }
      return ret;
    }
  }
}
