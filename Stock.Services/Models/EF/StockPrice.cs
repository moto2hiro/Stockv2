﻿using System;
using System.Collections.Generic;

namespace Stock.Services.Models.EF
{
    public partial class StockPrice
    {
        public int Id { get; set; }
        public int SymbolId { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public int Volume { get; set; }
        public DateTime PriceDate { get; set; }
    }
}
