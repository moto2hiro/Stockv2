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
  }
}
