using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Stock.Services
{
  public class Consts
  {
    public const int DEFAULT_SKIP = 500;
    public const int CLASS_UP = 1;
    public const int CLASS_DOWN = 0;
    public const string SYMBOL_DOW_JONES = "^DJI";
    public const string SYMBOL_DOW_ETF = "DIA";
    public static readonly int[] AVG_PERIODS = new int[] { 14, 28, 60, 365, 20000 };
    public const string AVG_TYPE_UP = "Up";
    public const string AVG_TYPE_DOWN = "Down";
    public const string AVG_TYPE_TOTAL = "Total";
    public static readonly string[] AVG_TYPES =
      new string[] { AVG_TYPE_UP, AVG_TYPE_DOWN, AVG_TYPE_TOTAL };

    #region Charts
    public const int CHART_IMAGE_WIDTH = 224;
    public const int CHART_IMAGE_HEIGHT = 224;
    public const int CHART_IMAGE_WIDTH_V2 = 448;
    public const int CHART_IMAGE_HEIGHT_V2 = 448;
    public const int DEFAULT_CHART_PERIOD = 50;
    public const int CHART_V0 = 0; // t - 1 80% success idk threshold.
    public const int CHART_V1 = 1; // t 50%
    public const int CHART_V2 = 2; // dow jones
    public const int CHART_V3 = 3; // med threshold rsi
    public const int CHART_V4 = 4; // t - 1 + 2 prediction 50%
    public const int CHART_V5 = 5; // EMA 13 and 48 cross
    #endregion

    #region Stock Analysis
    public const decimal PRICE_VOLUME_THRESHOLD = 20000000;
    public const decimal TRADING_VOLUME_THRESHOLD = 1000000;
    public const decimal RSI_HIGHEST_THRESHOLD = 85;
    public const decimal RSI_HIGH_THRESHOLD = 80;
    public const decimal RSI_MED_HIGH_THRESHOLD = 65;
    public const decimal RSI_LOW_THRESHOLD = 20;
    public const decimal RSI_MED_LOW_THRESHOLD = 35;
    public const decimal RSI_LOWEST_THRESHOLD = 15;
    public const decimal RSI_85 = 85;
    public const decimal RSI_80 = 80;
    public const decimal RSI_75 = 75;
    public const decimal RSI_70 = 70;
    public const decimal RSI_30 = 30;
    public const decimal RSI_25 = 25;
    public const decimal RSI_20 = 20;
    public const decimal RSI_15 = 15;
    public const int MAX_CALC_PERIOD = 200;
    public const int MAX_FUTURE_PERIOD = 60;
    public const int SMA_PERIOD_10 = 10;
    public const int SMA_PERIOD_20 = 20;
    public const int SMA_PERIOD_50 = 50;
    public const int SMA_PERIOD_100 = 100;
    public const int SMA_PERIOD_200 = 200;
    public const int EMA_PERIOD_5 = 5;
    public const int EMA_PERIOD_9 = 9;
    public const int EMA_PERIOD_10 = 10;
    public const int EMA_PERIOD_12 = 12;
    public const int EMA_PERIOD_13 = 13;
    public const int EMA_PERIOD_26 = 26;
    public const int EMA_PERIOD_48 = 48;
    public const int ADTV_PERIOD_20 = 20;
    public const int ADTV_PERIOD_30 = 30;
    public const int ADTV_PERIOD_50 = 50;
    public const int ADPV_PERIOD_20 = 20;
    public const int ADPV_PERIOD_30 = 30;
    public const int ADPV_PERIOD_50 = 50;
    public const int RSI_PERIOD_6 = 6;
    public const int RSI_PERIOD_10 = 10;
    public const int RSI_PERIOD_14 = 14;
    public const int LOCAL_MAX_PERIOD_10 = 10;
    public const int LOCAL_MAX_PERIOD_20 = 20;
    public const int LOCAL_MAX_PERIOD_50 = 50;
    public const int LOCAL_MAX_PERIOD_200 = 200;
    public const int LOCAL_MIN_PERIOD_10 = 10;
    public const int LOCAL_MIN_PERIOD_20 = 20;
    public const int LOCAL_MIN_PERIOD_50 = 50;
    public const int LOCAL_MIN_PERIOD_200 = 200;
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

    #region WorldPrice
    public const string SYMBOL_GSPC = "^GSPC";
    public const string SYMBOL_SPY = "SPY";
    public const string SYMBOL_DJI = "^DJI";
    public const string SYMBOL_DIA = "DIA";
    public const string SYMBOL_N225 = "^N225";
    public const string SYMBOL_AXJO = "^AXJO";
    public const string SYMBOL_HSI = "^HSI";
    public const string SYMBOL_SSEC = "^SSEC";
    public const string SYMBOL_BSESN = "^BSESN";
    public const string SYMBOL_NIFTY = "^NIFTY";
    public const string SYMBOL_KS11 = "^KS11";
    public const string SYMBOL_TWII = "^TWII";
    #endregion
  }
}
