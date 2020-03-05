using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Services.Models.TDAm
{
  public class TDAmPriceHistoryResp
  {
    public string symbol { get; set; }
    public bool empty { get; set; }
    public List<TDAmPriceHistoryCandle> candles { get; set; }
  }

  public class TDAmPriceHistoryCandle
  {
    public decimal open { get; set; }
    public decimal high { get; set; }
    public decimal low { get; set; }
    public decimal close { get; set; }
    public int volume { get; set; }
    public long datetime { get; set; }

    public DateTime PriceDate => DateUtils.ToDateTime(datetime);
    public DateTime PriceDateWithoutTime => new DateTime(PriceDate.Year, PriceDate.Month, PriceDate.Day);
  }
}
