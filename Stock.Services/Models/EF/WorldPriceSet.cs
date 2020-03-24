using System;
using System.Collections.Generic;

namespace Stock.Services.Models.EF
{
    public partial class WorldPriceSet
    {
        public int Id { get; set; }
        public DateTime PriceDate { get; set; }
        public decimal? YSpydiffActual { get; set; }
        public int? YSpyactual { get; set; }
        public int? YSpypredicted { get; set; }
        public decimal? YSpypredictedProb { get; set; }
        public decimal? YDiadiffActual { get; set; }
        public int? YDiaactual { get; set; }
        public int? YDiapredicted { get; set; }
        public decimal? YDiapredictedProb { get; set; }
        public decimal? XFtsediffNorm { get; set; }
        public decimal? XStoxxdiffNorm { get; set; }
        public decimal? XGdaxidiffNorm { get; set; }
        public decimal? XN225diffNorm { get; set; }
        public decimal? XAxjodiffNorm { get; set; }
        public decimal? XHsidiffNorm { get; set; }
    }
}
