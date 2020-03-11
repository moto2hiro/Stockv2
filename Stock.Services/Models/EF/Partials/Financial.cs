using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Services.Models.EF
{
  public partial class Financial
  {
    [NotMapped]
    public decimal CurrentRatio => (CurrentLiabilities.GetValueOrDefault() > 0) ?
      CurrentAssets.GetValueOrDefault() / CurrentLiabilities.GetValueOrDefault() : 0;

    [NotMapped]
    public decimal DebtToEquityRatio => (TtlEquity.GetValueOrDefault() > 0) ?
      LongTermDebt.GetValueOrDefault() / TtlEquity.GetValueOrDefault() : 0;

    [NotMapped]
    public decimal Roe => (TtlEquity.GetValueOrDefault() > 0) ?
      NetIncome.GetValueOrDefault() / TtlEquity.GetValueOrDefault() : 0;
  }
}
