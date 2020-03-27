using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Stock.Services.Utils
{
  public class NumberUtils
  {
    public static decimal Round(decimal src, int noOfDecimals = 2)
    {
      return Math.Round(src, noOfDecimals);
    }
    public static decimal Pow(decimal src, decimal noOfPower)
    {
      return (decimal)Math.Pow((double)src, (double)noOfPower);
    }
    public static int Floor(decimal src)
    {
      return (int)Math.Floor(src);
    }
    public static decimal Pct(int cnt, int total, int noOfDecimals = 2)
    {
      return Round(((decimal)cnt / total) * 100, noOfDecimals);
    }
    public static decimal PctChange(decimal start, decimal final, int noOfDecimals = 2)
    {
      return Round(((final / start) - 1) * 100, noOfDecimals);
    }
  }
}
