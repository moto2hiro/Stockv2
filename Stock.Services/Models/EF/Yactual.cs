using System;
using System.Collections.Generic;

namespace Stock.Services.Models.EF
{
    public partial class Yactual
    {
        public int Id { get; set; }
        public int StockPriceId { get; set; }
        public int NoOfFutureDays { get; set; }
    }
}
