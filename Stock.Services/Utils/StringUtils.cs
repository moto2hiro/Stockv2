using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Stock.Services.Utils
{
  public class StringUtils
  {
    public static int ToInt(string src)
    {
      var ret = 0;
      if (!string.IsNullOrEmpty(src) && Int32.TryParse(src, out ret))
      {
        return ret;
      }
      return ret;
    }

    public static decimal ToDecimal(string src)
    {
      var ret = 0m;
      if (!string.IsNullOrEmpty(src) && Decimal.TryParse(src, out ret))
      {
        return ret;
      }
      return ret;
    }

    public static byte[] ToByteArray(string src)
    {
      if (!string.IsNullOrEmpty(src))
      {
        return Convert.FromBase64String(src);
      }
      return null;
    }

    public static string ToString(byte[] src)
    {
      if (src != null)
      {
        return Convert.ToBase64String(src);
      }
      return null;
    }

    public static T Deserialize<T>(string src)
    {
      if(!string.IsNullOrEmpty(src))
      {
        return JsonConvert.DeserializeObject<T>(src);
      }
      return default(T);
    }
  }
}
