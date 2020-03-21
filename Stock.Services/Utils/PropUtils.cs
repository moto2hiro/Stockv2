using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace Stock.Services.Utils
{
  public class PropUtils
  {
    public static DataTable ToDataTable<T>(IEnumerable<T> models)
    {
      var props = TypeDescriptor.GetProperties(typeof(T));
      var table = new DataTable();
      for (int i = 0; i < props.Count; i++)
      {
        table.Columns.Add(props[i].Name, 
          Nullable.GetUnderlyingType(props[i].PropertyType) ?? props[i].PropertyType);
      }
      var values = new object[props.Count];
      foreach (var model in models)
      {
        for (int i = 0; i < values.Length; i++)
        {
          values[i] = props[i].GetValue(model);
        }
        table.Rows.Add(values);
      }
      return table;
    }
  }
}
