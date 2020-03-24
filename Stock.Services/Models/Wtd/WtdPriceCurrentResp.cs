using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Services.Models.Wtd
{
  public class WtdPriceCurrentResp
  {
    public int symbols_requested { get; set; }
    public int symbols_returned { get; set; }
    public List<WtdPriceCurrentItem> data { get; set; }
  }

  public class WtdPriceCurrentItem
  {
    public string symbol { get; set; }
    public string name { get; set; }
    public decimal price { get; set; }
    public decimal price_open { get; set; }
    public decimal close_yesterday { get; set; }
  }
}
