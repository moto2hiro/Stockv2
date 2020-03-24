using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Services.Models.Wtd
{
  public class WtdPriceDailyReq
  {
    public string symbol { get; set; }
    public string api_token { get; set; }
    public string date_from { get; set; } // yyyy-MM-dd
  }
}
