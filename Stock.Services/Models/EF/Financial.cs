using System;
using System.Collections.Generic;

namespace Stock.Services.Models.EF
{
    public partial class Financial
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public int? Year { get; set; }
        public int? Quarter { get; set; }
        public DateTime? ReportDate { get; set; }
        public DateTime? PublishDate { get; set; }
        public int? Shares { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? GrossProfit { get; set; }
        public decimal? NetIncome { get; set; }
        public decimal? CurrentAssets { get; set; }
        public decimal? TtlAssets { get; set; }
        public decimal? CurrentLiabilities { get; set; }
        public decimal? TtlLiabilities { get; set; }
        public decimal? LongTermDebt { get; set; }
        public decimal? TtlEquity { get; set; }
        public decimal? FreeCashFlow { get; set; }
        public decimal? DivPayoutRatio { get; set; }
    }
}
