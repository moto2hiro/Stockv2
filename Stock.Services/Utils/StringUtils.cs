using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

    public static string Serialize<T>(T model)
    {
      if (model == null) return "";
      return JsonConvert.SerializeObject(model);
    }

    public static T Deserialize<T>(string src, Dictionary<string, string> replacements = null)
    {
      if(!string.IsNullOrEmpty(src))
      {
        var jObj = JObject.Parse(src);
        if(replacements != null)
        {
          foreach (var replacement in replacements)
          {
            //if (!string.IsNullOrEmpty(replacement.Key) && !string.IsNullOrEmpty(replacement.Value))
            //{
            //  var org = jObj[(string)jObj[replacement.Key]];
            //  jObj.Remove((string)jObj[replacement.Key]);
            //  jObj[replacement.Value] = org;
            //}
          }
        }
        return jObj.ToObject<T>();
      }
      return default(T);
    }
  }
}
