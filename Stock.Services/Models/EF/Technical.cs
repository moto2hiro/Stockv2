using System;
using System.Collections.Generic;

namespace Stock.Services.Models.EF
{
    public partial class Technical
    {
        public int Id { get; set; }
        public int StockPriceId { get; set; }
        public string CalcType { get; set; }
        public int CalcPeriod { get; set; }
        public string CalcValue { get; set; }
    }
}
