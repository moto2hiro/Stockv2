using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Services.Models.EF
{
  public partial class StockPrice
  {
    public decimal PriceVolume => ClosePrice * Volume;

    #region YActual
    [NotMapped]
    public int? YActual { get; set; }

    [NotMapped]
    public int? YActual2 { get; set; }

    [NotMapped]
    public int? YActual3 { get; set; }

    [NotMapped]
    public int? YActual4 { get; set; }

    [NotMapped]
    public int? YActual5 { get; set; }

    [NotMapped]
    public int? YActual6 { get; set; }

    [NotMapped]
    public int? YActual7 { get; set; }

    [NotMapped]
    public int? YActual8 { get; set; }

    [NotMapped]
    public int? YActual9 { get; set; }

    [NotMapped]
    public int? YActual10 { get; set; }

    [NotMapped]
    public int? YActual11 { get; set; }

    [NotMapped]
    public int? YActual12 { get; set; }

    [NotMapped]
    public int? YActual13 { get; set; }

    [NotMapped]
    public int? YActual14 { get; set; }

    [NotMapped]
    public int? YActual15 { get; set; }

    [NotMapped]
    public int? YActual16 { get; set; }

    [NotMapped]
    public int? YActual17 { get; set; }

    [NotMapped]
    public int? YActual18 { get; set; }

    [NotMapped]
    public int? YActual19 { get; set; }

    [NotMapped]
    public int? YActual20 { get; set; }

    [NotMapped]
    public int? YActual21 { get; set; }

    [NotMapped]
    public int? YActual22 { get; set; }

    [NotMapped]
    public int? YActual23 { get; set; }

    [NotMapped]
    public int? YActual24 { get; set; }

    [NotMapped]
    public int? YActual25 { get; set; }

    [NotMapped]
    public int? YActual26 { get; set; }

    [NotMapped]
    public int? YActual27 { get; set; }

    [NotMapped]
    public int? YActual28 { get; set; }

    [NotMapped]
    public int? YActual29 { get; set; }

    [NotMapped]
    public int? YActual30 { get; set; }

    [NotMapped]
    public int? YActual31 { get; set; }

    [NotMapped]
    public int? YActual32 { get; set; }

    [NotMapped]
    public int? YActual33 { get; set; }

    [NotMapped]
    public int? YActual34 { get; set; }

    [NotMapped]
    public int? YActual35 { get; set; }

    [NotMapped]
    public int? YActual36 { get; set; }

    [NotMapped]
    public int? YActual37 { get; set; }

    [NotMapped]
    public int? YActual38 { get; set; }

    [NotMapped]
    public int? YActual39 { get; set; }

    [NotMapped]
    public int? YActual40 { get; set; }

    [NotMapped]
    public int? YActual41 { get; set; }

    [NotMapped]
    public int? YActual42 { get; set; }

    [NotMapped]
    public int? YActual43 { get; set; }

    [NotMapped]
    public int? YActual44 { get; set; }

    [NotMapped]
    public int? YActual45 { get; set; }

    [NotMapped]
    public int? YActual46 { get; set; }

    [NotMapped]
    public int? YActual47 { get; set; }

    [NotMapped]
    public int? YActual48 { get; set; }

    [NotMapped]
    public int? YActual49 { get; set; }

    [NotMapped]
    public int? YActual50 { get; set; }

    [NotMapped]
    public int? YActual51 { get; set; }

    [NotMapped]
    public int? YActual52 { get; set; }

    [NotMapped]
    public int? YActual53 { get; set; }

    [NotMapped]
    public int? YActual54 { get; set; }

    [NotMapped]
    public int? YActual55 { get; set; }

    [NotMapped]
    public int? YActual56 { get; set; }

    [NotMapped]
    public int? YActual57 { get; set; }

    [NotMapped]
    public int? YActual58 { get; set; }

    [NotMapped]
    public int? YActual59 { get; set; }

    [NotMapped]
    public int? YActual60 { get; set; }
    #endregion

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

    [NotMapped]
    public bool HasSMA10CrossAbove20 { get; set; }

    [NotMapped]
    public bool HasSMA10CrossBelow20 { get; set; }

    [NotMapped]
    public bool HasSMA10CrossAbove50 { get; set; }

    [NotMapped]
    public bool HasSMA10CrossBelow50 { get; set; }

    [NotMapped]
    public bool HasSMA10CrossAbove100 { get; set; }

    [NotMapped]
    public bool HasSMA10CrossBelow100 { get; set; }

    [NotMapped]
    public bool HasSMA10CrossAbove200 { get; set; }

    [NotMapped]
    public bool HasSMA10CrossBelow200 { get; set; }

    [NotMapped]
    public bool HasSMA20CrossAbove50 { get; set; }

    [NotMapped]
    public bool HasSMA20CrossBelow50 { get; set; }

    [NotMapped]
    public bool HasSMA20CrossAbove100 { get; set; }

    [NotMapped]
    public bool HasSMA20CrossBelow100 { get; set; }

    [NotMapped]
    public bool HasSMA20CrossAbove200 { get; set; }

    [NotMapped]
    public bool HasSMA20CrossBelow200 { get; set; }

    [NotMapped]
    public bool HasSMA50CrossAbove100 { get; set; }

    [NotMapped]
    public bool HasSMA50CrossBelow100 { get; set; }

    [NotMapped]
    public bool HasSMA50CrossAbove200 { get; set; }

    [NotMapped]
    public bool HasSMA50CrossBelow200 { get; set; }

    [NotMapped]
    public bool HasSMA100CrossAbove200 { get; set; }

    [NotMapped]
    public bool HasSMA100CrossBelow200 { get; set; }
    #endregion

    #region EMA
    [NotMapped]
    public decimal EMA_5 { get; set; }

    [NotMapped]
    public decimal EMA_9 { get; set; }

    [NotMapped]
    public decimal EMA_10 { get; set; }

    [NotMapped]
    public decimal EMA_12 { get; set; }

    [NotMapped]
    public decimal EMA_13 { get; set; }

    [NotMapped]
    public decimal EMA_26 { get; set; }

    [NotMapped]
    public decimal EMA_48 { get; set; }

    [NotMapped]
    public bool HasEMA5CrossAbove9 { get; set; }
    
    [NotMapped]
    public bool HasEMA5CrossBelow9 { get; set; }

    [NotMapped]
    public bool HasEMA5CrossAbove10 { get; set; }

    [NotMapped]
    public bool HasEMA5CrossBelow10 { get; set; }

    [NotMapped]
    public bool HasEMA5CrossAbove12 { get; set; }

    [NotMapped]
    public bool HasEMA5CrossBelow12 { get; set; }

    [NotMapped]
    public bool HasEMA5CrossAbove13 { get; set; }

    [NotMapped]
    public bool HasEMA5CrossBelow13 { get; set; }

    [NotMapped]
    public bool HasEMA5CrossAbove26 { get; set; }

    [NotMapped]
    public bool HasEMA5CrossBelow26 { get; set; }

    [NotMapped]
    public bool HasEMA5CrossAbove48 { get; set; }

    [NotMapped]
    public bool HasEMA5CrossBelow48 { get; set; }

    [NotMapped]
    public bool HasEMA9CrossAbove10 { get; set; }

    [NotMapped]
    public bool HasEMA9CrossBelow10 { get; set; }

    [NotMapped]
    public bool HasEMA9CrossAbove12 { get; set; }

    [NotMapped]
    public bool HasEMA9CrossBelow12 { get; set; }

    [NotMapped]
    public bool HasEMA9CrossAbove13 { get; set; }

    [NotMapped]
    public bool HasEMA9CrossBelow13 { get; set; }

    [NotMapped]
    public bool HasEMA9CrossAbove26 { get; set; }

    [NotMapped]
    public bool HasEMA9CrossBelow26 { get; set; }

    [NotMapped]
    public bool HasEMA9CrossAbove48 { get; set; }

    [NotMapped]
    public bool HasEMA9CrossBelow48 { get; set; }

    [NotMapped]
    public bool HasEMA10CrossAbove12 { get; set; }

    [NotMapped]
    public bool HasEMA10CrossBelow12 { get; set; }

    [NotMapped]
    public bool HasEMA10CrossAbove13 { get; set; }

    [NotMapped]
    public bool HasEMA10CrossBelow13 { get; set; }

    [NotMapped]
    public bool HasEMA10CrossAbove26 { get; set; }

    [NotMapped]
    public bool HasEMA10CrossBelow26 { get; set; }

    [NotMapped]
    public bool HasEMA10CrossAbove48 { get; set; }

    [NotMapped]
    public bool HasEMA10CrossBelow48 { get; set; }

    [NotMapped]
    public bool HasEMA12CrossAbove13 { get; set; }

    [NotMapped]
    public bool HasEMA12CrossBelow13 { get; set; }

    [NotMapped]
    public bool HasEMA12CrossAbove26 { get; set; }

    [NotMapped]
    public bool HasEMA12CrossBelow26 { get; set; }

    [NotMapped]
    public bool HasEMA12CrossAbove48 { get; set; }

    [NotMapped]
    public bool HasEMA12CrossBelow48 { get; set; }

    [NotMapped]
    public bool HasEMA13CrossAbove26 { get; set; }

    [NotMapped]
    public bool HasEMA13CrossBelow26 { get; set; }

    [NotMapped]
    public bool HasEMA13CrossAbove48 { get; set; }

    [NotMapped]
    public bool HasEMA13CrossBelow48 { get; set; }

    [NotMapped]
    public bool HasEMA26CrossAbove48 { get; set; }

    [NotMapped]
    public bool HasEMA26CrossBelow48 { get; set; }
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
    public decimal RSI_6 { get; set; }

    [NotMapped]
    public decimal RSI_10 { get; set; }

    [NotMapped]
    public decimal RSI_14 { get; set; }

    [NotMapped]
    public decimal? AvgGain_6 { get; set; }

    [NotMapped]
    public decimal? AvgLoss_6 { get; set; }

    [NotMapped]
    public decimal? AvgGain_10 { get; set; }

    [NotMapped]
    public decimal? AvgLoss_10 { get; set; }

    [NotMapped]
    public decimal? AvgGain_14 { get; set; }

    [NotMapped]
    public decimal? AvgLoss_14 { get; set; }

    [NotMapped]
    public bool HasRSI6CrossAbove70 { get; set; }

    [NotMapped]
    public bool HasRSI6CrossAbove75 { get; set; }

    [NotMapped]
    public bool HasRSI6CrossAbove80 { get; set; }

    [NotMapped]
    public bool HasRSI6CrossAbove85 { get; set; }

    [NotMapped]
    public bool HasRSI6CrossBelow30 { get; set; }

    [NotMapped]
    public bool HasRSI6CrossBelow25 { get; set; }

    [NotMapped]
    public bool HasRSI6CrossBelow20 { get; set; }

    [NotMapped]
    public bool HasRSI6CrossBelow15 { get; set; }

    [NotMapped]
    public bool HasRSI10CrossAbove70 { get; set; }

    [NotMapped]
    public bool HasRSI10CrossAbove75 { get; set; }

    [NotMapped]
    public bool HasRSI10CrossAbove80 { get; set; }

    [NotMapped]
    public bool HasRSI10CrossAbove85 { get; set; }

    [NotMapped]
    public bool HasRSI10CrossBelow30 { get; set; }

    [NotMapped]
    public bool HasRSI10CrossBelow25 { get; set; }

    [NotMapped]
    public bool HasRSI10CrossBelow20 { get; set; }

    [NotMapped]
    public bool HasRSI10CrossBelow15 { get; set; }

    [NotMapped]
    public bool HasRSI14CrossAbove70 { get; set; }

    [NotMapped]
    public bool HasRSI14CrossAbove75 { get; set; }

    [NotMapped]
    public bool HasRSI14CrossAbove80 { get; set; }

    [NotMapped]
    public bool HasRSI14CrossAbove85 { get; set; }

    [NotMapped]
    public bool HasRSI14CrossBelow30 { get; set; }

    [NotMapped]
    public bool HasRSI14CrossBelow25 { get; set; }

    [NotMapped]
    public bool HasRSI14CrossBelow20 { get; set; }

    [NotMapped]
    public bool HasRSI14CrossBelow15 { get; set; }
    #endregion

    #region Local Max, Min
    [NotMapped]
    public bool IsLocalMax_10 { get; set; }

    [NotMapped]
    public bool IsLocalMax_20 { get; set; }

    [NotMapped]
    public bool IsLocalMax_50 { get; set; }
    
    [NotMapped]
    public bool IsLocalMax_200 { get; set; }

    [NotMapped]
    public bool IsLocalMin_10 { get; set; }

    [NotMapped]
    public bool IsLocalMin_20 { get; set; }

    [NotMapped]
    public bool IsLocalMin_50 { get; set; }

    [NotMapped]
    public bool IsLocalMin_200 { get; set; }
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

    [NotMapped]
    public decimal BollingerUpperStvDev25_20 { get; set; }

    [NotMapped]
    public decimal BollingerLowerStvDev25_20 { get; set; }
    #endregion
  }
}
