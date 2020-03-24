using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Services.Models.Wtd
{
  public class WtdPriceDailyResp
  {
    public string symbol { get; set; }
    public decimal open { get; set; }
    public decimal close { get; set; }
    public string date { get; set; } // yyyy-MM-dd
  }
}
