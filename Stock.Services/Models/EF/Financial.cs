using System;
using System.Collections.Generic;

namespace Stock.Services.Models.EF
{
    public partial class Financial
    {
        public int Id { get; set; }
        public int SymbolId { get; set; }
        public int Year { get; set; }
        public int Quarter { get; set; }
        public DateTime QuarterEndDate { get; set; }
        public DateTime? FileDate { get; set; }
        public decimal? MarketCap { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? GrossProfit { get; set; }
        public decimal? OperatingIncome { get; set; }
        public decimal? NetIncome { get; set; }
        public decimal? CurrentAssets { get; set; }
        public decimal? TtlAssets { get; set; }
        public decimal? CurrentLiabilities { get; set; }
        public decimal? TtlLiabilities { get; set; }
        public decimal? TtlEquity { get; set; }
        public decimal? RevenueGrowth { get; set; }
        public decimal? RevenueQqGrowth { get; set; }
        public decimal? NopatGrowth { get; set; }
        public decimal? NopatQqGrowth { get; set; }
        public decimal? NetIncomeGrowth { get; set; }
        public decimal? NetIncomeQqGrowth { get; set; }
        public decimal? FreeCashFlow { get; set; }
        public decimal? CurrentRatio { get; set; }
        public decimal? DebtToEquityRatio { get; set; }
        public decimal? PeRatio { get; set; }
        public decimal? PbRatio { get; set; }
        public decimal? DivPayoutRatio { get; set; }
        public decimal? Roe { get; set; }
        public decimal? Roa { get; set; }
    }
}
