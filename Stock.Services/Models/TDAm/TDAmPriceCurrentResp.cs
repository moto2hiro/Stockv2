using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Services.Models.TDAm
{
  public class TDAmPriceCurrentResp
  {
    // this property name is dynamic
    public TDAmPriceCurrentRespObj symbol { get; set; }
  }

  public class TDAmPriceCurrentRespObj
  {
    //public string assetType { get; set; }
    //public string assetMainType { get; set; }
    //public string cusip { get; set; }
    //public string assetSubType { get; set; }
    public string symbol { get; set; }
    //public string description { get; set; }
    //public decimal bidPrice { get; set; }
    //public int bidSize { get; set; }
    //public string bidId { get; set; }
    //public decimal askPrice { get; set; }
    //public int askSize { get; set; }
    //public string askId { get; set; }
    //public decimal lastPrice { get; set; }
    //public int lastSize { get; set; }
    //public string lastId { get; set; }
    public decimal openPrice { get; set; }
    public decimal highPrice { get; set; }
    public decimal lowPrice { get; set; }
    //public string bidTick { get; set; }
    public decimal closePrice { get; set; }
    //public decimal netChange { get; set; }
    public int totalVolume { get; set; }
    public int quoteTimeInLong { get; set; }
    public int tradeTimeInLong { get; set; }
    //public decimal mark { get; set; }
    //public string exchange { get; set; }
    //public string exchangeName { get; set; }
    //public bool marginable { get; set; }
    //public bool shortable { get; set; }
    //public decimal volatility { get; set; }
    //public int digits { get; set; }
    //public decimal peRatio { get; set; }
    //public decimal divAmount { get; set; }
    //public decimal divYield { get; set; }
    //public string securityStatus { get; set; }
    //public decimal regularMarketLastPrice { get; set; }
    //public int regularMarketLastSize { get; set; }
    //public decimal regularMarketNetChange { get; set; }
    //public int regularMarketTradeTimeInLong { get; set; }
    //public decimal netPercentChangeInDouble { get; set; }
    //public decimal markChangeInDouble { get; set; }
    //public decimal markPercentChangeInDouble { get; set; }
    //public decimal regularMarketPercentChangeInDouble { get; set; }
    //public bool delayed { get; set; }

    public DateTime PriceDate => DateUtils.ToDateTime(quoteTimeInLong);
    public DateTime PriceDateWithoutTime => new DateTime(PriceDate.Year, PriceDate.Month, PriceDate.Day);
  }
}
