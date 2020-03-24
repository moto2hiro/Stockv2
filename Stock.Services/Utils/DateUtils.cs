using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Stock.Services.Utils
{
  public class DateUtils
  {
    public const string Culture_Us = "en-US";
    public const string Format_Mdyyyy = "M/d/yyyy";
    public const string Format_yyyyMddHHmmss = "yyyy-M-dd HH:mm:ss +0000";

    public static DateTime ToDateTime(
        string src,
        string format = Format_Mdyyyy,
        string culture = Culture_Us,
        DateTimeStyles styles = DateTimeStyles.None)
    {
      var ret = DateTime.MinValue;
      if (!string.IsNullOrEmpty(src) && !string.IsNullOrEmpty(format))
      {
        var isValid = DateTime.TryParseExact(
            src,
            format,
            new CultureInfo(culture),
            DateTimeStyles.None,
            out ret);
        if (!isValid) ret = DateTime.MinValue;
      }
      return ret;
    }

    public static DateTime ToUtc(DateTime src, string timezone)
    {
      var ret = src;
      if (!string.IsNullOrEmpty(timezone))
      {
        var zoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezone);
        ret = TimeZoneInfo.ConvertTimeToUtc(src, zoneInfo);
      }
      return ret;
    }

    public static DateTime AddBusinessDays(DateTime src, int numberOfDays)
    {
      var ret = src;
      var isAdd = numberOfDays > 0;
      numberOfDays = Math.Abs(numberOfDays);
      while (numberOfDays > 0)
      {
        if (isAdd && ret != DateTime.MaxValue)
          ret.AddDays(1);
        else if (!isAdd && ret != DateTime.MinValue)
          ret.AddDays(-1);

        if (ret.DayOfWeek < DayOfWeek.Saturday &&
            ret.DayOfWeek > DayOfWeek.Sunday)
          numberOfDays--;
      }
      return ret;
    }

    private static readonly DateTime EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    public static DateTime ToDateTime(long unixTime)
    {
      return EPOCH.AddMilliseconds(unixTime);
    }
  }
}
