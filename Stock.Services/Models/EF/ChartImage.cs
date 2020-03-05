using System;
using System.Collections.Generic;

namespace Stock.Services.Models.EF
{
    public partial class ChartImage
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public DateTime PriceDate { get; set; }
        public int? YActual { get; set; }
        public int? YPredicted { get; set; }
        public decimal? YPredictedProbability { get; set; }
        public string XImage { get; set; }
        public int Version { get; set; }
    }
}
