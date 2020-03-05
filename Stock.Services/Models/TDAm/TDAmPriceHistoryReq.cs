using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Services.Models.TDAm
{
  public class TDAmPriceHistoryReq
  {
    public string Symbol { get; set; }
    public string apikey { get; set; }
    public string periodType { get; set; }
    public int? period { get; set; }
    public string frequencyType { get; set; }
    public int? frequency { get; set; }
    public int? endDate { get; set; }
    public int? startDate { get; set; }
    public bool? needExtendedHoursData { get; set; }
  }
}
