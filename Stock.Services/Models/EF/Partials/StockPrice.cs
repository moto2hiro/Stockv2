using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Services.Models.EF
{
  public partial class StockPrice
  {
    public decimal PriceVolume => ClosePrice * Volume;

    [NotMapped]
    public int? YActual { get; set; }

    [NotMapped]
    public bool IsCloseGreaterToday { get; set; }

    #region SMA
    [NotMapped]
    public decimal SMA_10 { get; set; }

    [NotMapped]
    public decimal SMA_20 { get; set; }

    [NotMapped]
    public decimal SMA_50 { get; set; }

    [NotMapped]
    public decimal SMA_100 { get; set; }

    [NotMapped]
    public decimal SMA_200 { get; set; }
    #endregion

    #region ADTV, ADPV
    [NotMapped]
    public decimal ADTV_20 { get; set; }

    [NotMapped]
    public decimal ADTV_30 { get; set; }

    [NotMapped]
    public decimal ADTV_50 { get; set; }

    [NotMapped]
    public decimal ADPV_20 { get; set; }

    [NotMapped]
    public decimal ADPV_30 { get; set; }

    [NotMapped]
    public decimal ADPV_50 { get; set; }
    #endregion

    #region RSI
    [NotMapped]
    public decimal RSI_10 { get; set; }

    [NotMapped]
    public decimal RSI_14 { get; set; }

    [NotMapped]
    public decimal? AvgGain_10 { get; set; }

    [NotMapped]
    public decimal? AvgLoss_10 { get; set; }

    [NotMapped]
    public decimal? AvgGain_14 { get; set; }

    [NotMapped]
    public decimal? AvgLoss_14 { get; set; }
    #endregion

    #region Local Max, Min
    [NotMapped]
    public bool IsLocalMax_10 { get; set; }

    [NotMapped]
    public bool IsLocalMax_20 { get; set; }

    [NotMapped]
    public bool IsLocalMax_50 { get; set; }

    [NotMapped]
    public bool IsLocalMin_10 { get; set; }

    [NotMapped]
    public bool IsLocalMin_20 { get; set; }

    [NotMapped]
    public bool IsLocalMin_50 { get; set; }
    #endregion

    #region Bollinger Bands
    [NotMapped]
    public decimal BollingerUpperStvDev1_20 { get; set; }

    [NotMapped]
    public decimal BollingerLowerStvDev1_20 { get; set; }

    [NotMapped]
    public decimal BollingerUpperStvDev2_20 { get; set; }

    [NotMapped]
    public decimal BollingerLowerStvDev2_20 { get; set; }
    #endregion
  }
}
