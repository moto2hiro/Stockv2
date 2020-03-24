using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Services.Models.Wtd
{
  public class WtdPriceCurrentReq
  {
    public string symbol { get; set; }
    public string api_token { get; set; }
  }
}
