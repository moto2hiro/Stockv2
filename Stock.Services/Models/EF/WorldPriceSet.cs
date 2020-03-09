using System;
using System.Collections.Generic;

namespace Stock.Services.Models.EF
{
    public partial class WorldPriceSet
    {
        public int Id { get; set; }
        public DateTime PriceDate { get; set; }
        public decimal? YGspcdiffNorm { get; set; }
        public decimal? YGspcdiffActual { get; set; }
        public int? YGspcactual { get; set; }
        public int? YGspcpredicted { get; set; }
        public decimal? YGspcpredictedProb { get; set; }
        public decimal? YSpydiffNorm { get; set; }
        public decimal? YSpydiffActual { get; set; }
        public int? YSpyactual { get; set; }
        public int? YSpypredicted { get; set; }
        public decimal? YSpypredictedProb { get; set; }
        public decimal? YDjidiffNorm { get; set; }
        public decimal? YDjidiffActual { get; set; }
        public int? YDjiactual { get; set; }
        public int? YDjipredicted { get; set; }
        public decimal? YDjipredictedProb { get; set; }
        public decimal? YDiadiffNorm { get; set; }
        public decimal? YDiadiffActual { get; set; }
        public int? YDiaactual { get; set; }
        public int? YDiapredicted { get; set; }
        public decimal? YDiapredictedProb { get; set; }
        public decimal? XN225diffNorm { get; set; }
        public decimal? XAxjodiffNorm { get; set; }
        public decimal? XHsidiffNorm { get; set; }
        public decimal? XSsecdiffNorm { get; set; }
        public decimal? XBsesndiffNorm { get; set; }
        public decimal? XNiftydiffNorm { get; set; }
        public decimal? XKs11diffNorm { get; set; }
        public decimal? XTwiidiffNorm { get; set; }
    }
}
