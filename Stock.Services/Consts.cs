using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Stock.Services
{
  public class Consts
  {
    public const int CLASS_UP = 1;
    public const int CLASS_DOWN = 0;
    public const string SYMBOL_DOW_JONES = "^DJI";
    public const string SYMBOL_DOW_ETF = "DIA";
    public static readonly int[] PERFORMANCE_PERIODS = new int[] { 14, 28, 60, 365, 20000 };

    #region Charts
    public const int CHART_IMAGE_WIDTH = 224;
    public const int CHART_IMAGE_HEIGHT = 224;
    public const int CHART_IMAGE_WIDTH_V2 = 448;
    public const int CHART_IMAGE_HEIGHT_V2 = 448;
    public const int DEFAULT_CHART_PERIOD = 50;
    public const int CHART_V1 = 1;
    public const int CHART_V2 = 2;
    #endregion

    #region Stock Analysis
    public const decimal PRICE_VOLUME_THRESHOLD = 20000000;
    public const decimal TRADING_VOLUME_THRESHOLD = 1000000;
    public const decimal RSI_HIGH_THRESHOLD = 70;
    public const decimal RSI_LOW_THRESHOLD = 30;
    // TODO? public const string PERIOD_METRIC_EMA = "EMA";
    // TODO? public const string PERIOD_METRIC_MACD = "MACD";
    // TODO? public const string PERIOD_METRIC_OBV = "OBV";
    public const string PERIOD_METRIC_SMA = "SMA";
    public const string PERIOD_METRIC_ADTV = "ADTV";
    public const string PERIOD_METRIC_ADPV = "ADPV";
    public const string PERIOD_METRIC_RSI = "RSI";
    public const string PERIOD_METRIC_AvgGain = "AvgGain";
    public const string PERIOD_METRIC_AvgLoss = "AvgLoss";
    public const int MAX_CALC_PERIOD = 200;
    public const int SMA_PERIOD_10 = 10;
    public const int SMA_PERIOD_20 = 20;
    public const int SMA_PERIOD_50 = 50;
    public const int SMA_PERIOD_100 = 100;
    public const int SMA_PERIOD_200 = 200;
    public const int ADTV_PERIOD_20 = 20;
    public const int ADTV_PERIOD_30 = 30;
    public const int ADTV_PERIOD_50 = 50;
    public const int ADPV_PERIOD_20 = 20;
    public const int ADPV_PERIOD_30 = 30;
    public const int ADPV_PERIOD_50 = 50;
    public const int RSI_PERIOD_10 = 10;
    public const int RSI_PERIOD_14 = 14;
    public const int LOCAL_MAX_PERIOD_10 = 10;
    public const int LOCAL_MAX_PERIOD_20 = 20;
    public const int LOCAL_MAX_PERIOD_50 = 50;
    public const int LOCAL_MIN_PERIOD_10 = 10;
    public const int LOCAL_MIN_PERIOD_20 = 20;
    public const int LOCAL_MIN_PERIOD_50 = 50;
    public const int BOLLINGER_PERIOD_20 = 20;
    #endregion

    #region TD Ameritrade
    public const string TDAM_URL_PRICE_CURRENT = "https://api.tdameritrade.com/v1/marketdata/quotes";
    public const string TDAM_URL_PRICE_HISTORY = "https://api.tdameritrade.com/v1/marketdata/{0}/pricehistory";
    public const string TDAM_PERIOD_DAY = "day";
    public const string TDAM_PERIOD_MONTH = "month";
    public const string TDAM_PERIOD_YEAR = "year";
    public const string TDAM_PERIOD_YTD = "ytd";
    public const string TDAM_FREQUENCY_MINUTE = "minute";
    public const string TDAM_FREQUENCY_DAILY = "daily";
    public const string TDAM_FREQUENCY_WEEKLY = "weekly";
    public const string TDAM_FREQUENCY_MONTHLY = "monthly";
    #endregion
  }
}
