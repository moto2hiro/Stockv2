using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Services.Models
{
  public class GetPeriodsOfStockPricesReq
  {
    public string Symbol { get; set; }
    public int NoOfPeriods { get; set; }
    public int Take { get; set; }
    public int Version { get; set; }
  }
}
