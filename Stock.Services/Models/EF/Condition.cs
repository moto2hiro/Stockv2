using System;
using System.Collections.Generic;

namespace Stock.Services.Models.EF
{
    public partial class Condition
    {
        public int Id { get; set; }
        public int StockPriceId { get; set; }
        public string Condition1 { get; set; }
    }
}
