using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Services.Models.EF
{
  public partial class StockPrice
  {
    [NotMapped]
    public decimal SmoothedPrice { get; set; }

    [NotMapped]
    public bool IsLocalMax { get; set; }

    [NotMapped]
    public bool IsLocalMin { get; set; }

    #region Kibot
    [NotMapped]
    public string TimeStr { get; set; }

    [NotMapped]
    public string[] TimeStrSplit => (!string.IsNullOrEmpty(TimeStr)) ?
      TimeStr.Split(':') : new string[] { "0", "0" };

    [NotMapped]
    public int Hours => StringUtils.ToInt(TimeStrSplit[0]);

    [NotMapped]
    public int Minutes => StringUtils.ToInt(TimeStrSplit[1]);
    #endregion
  }
}
