using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Services.Models.TDAm
{
  public class TDAmPriceCurrentReq
  {
    public string symbol { get; set; }
    public string apikey { get; set; }
  }
}
