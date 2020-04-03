using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Stock.Services.Utils
{
  public class PropUtils
  {
    public static DataTable ToDataTable<T>(IEnumerable<T> models)
    {
      var props = TypeDescriptor.GetProperties(typeof(T));
      var propIndices = new List<int>();
      var table = new DataTable();
      for (int i = 0; i < props.Count; i++)
      {
        var hasNotMapped = props[i].Attributes.OfType<NotMappedAttribute>().Any();
        if (!hasNotMapped)
        {
          propIndices.Add(i);
          table.Columns.Add(
            props[i].Name,
            Nullable.GetUnderlyingType(props[i].PropertyType) ?? props[i].PropertyType);
        }
      }
      var values = new object[propIndices.Count];
      foreach (var model in models)
      {
        var valIdx = 0;
        foreach(var propIdx in propIndices)
        {
          values[valIdx] = props[propIdx].GetValue(model);
          valIdx += 1;
        }
        table.Rows.Add(values);
      }
      return table;
    }
  }
}
