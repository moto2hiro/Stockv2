using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Services.Models.Wtd
{
  public class WtdPriceIntraReq
  {
    public string symbol { get; set; }
    public string api_token { get; set; }
    public int interval { get; set; }
    public int range { get; set; }
  }
}
