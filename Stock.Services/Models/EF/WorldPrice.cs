﻿using System;
using System.Collections.Generic;

namespace Stock.Services.Models.EF
{
    public partial class WorldPrice
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public DateTime PriceDate { get; set; }
    }
}
