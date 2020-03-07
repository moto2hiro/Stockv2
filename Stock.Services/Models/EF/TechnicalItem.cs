using System;
using System.Collections.Generic;

namespace Stock.Services.Models.EF
{
    public partial class TechnicalItem
    {
        public int Id { get; set; }
        public string LogType { get; set; }
        public int Yactual { get; set; }
        public string Symbol { get; set; }
        public DateTime PriceDate { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal Rsi10 { get; set; }
        public decimal Rsi14 { get; set; }
        public decimal Sma50 { get; set; }
        public decimal Sma200 { get; set; }
        public decimal Ema5 { get; set; }
        public decimal Ema9 { get; set; }
        public decimal Ema10 { get; set; }
        public decimal Ema12 { get; set; }
        public decimal Ema26 { get; set; }
        public bool IsLocalMax50 { get; set; }
        public bool IsLocalMin50 { get; set; }
        public decimal BollingerUpperStvDev120 { get; set; }
        public decimal BollingerLowerStvDev120 { get; set; }
        public decimal BollingerUpperStvDev220 { get; set; }
        public decimal BollingerLowerStvDev220 { get; set; }
    }
}
