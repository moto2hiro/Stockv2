using Stock.Services.Models;
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
    public const int YACTUAL_MAX_FUTURE = 150;
    public const string TECHNICAL_SMA = "SMA";
    public const string TECHNICAL_EMA = "EMA";
    public const string TECHNICAL_RSI = "RSI";
    public static readonly int[] SMA_PERIODS = new int[]
    {
      4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 
      21, 22, 23, 24, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 
      58, 59, 60, 61, 62, 100, 200
    };
    public static readonly int[] EMA_PERIODS = new int[]
    {
      4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
      21, 22, 23, 24, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57,
      58, 59, 60, 61, 62, 100, 200
    };
    public static readonly int[] RSI_PERIODS = new int[]
    {
      4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20
    };
    public static readonly int[] ADTV_PERIODS = new int[] { 50 };
    public static readonly int[] ADPV_PERIODS = new int[] { 50 };

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

    #region Intrinio
    public const string INTRINIO_PERIOD_SUFFIX_YTD = "YTD";
    public const string INTRINIO_PERIOD_SUFFIX_TTM = "TTM";
    public const string INTRINIO_PERIOD_SUFFIX_FY = "FY";
    public const string INTRINIO_PERIOD_PREFIX = "Q";
    #endregion

    #region WorldPrice
    public const string SYMBOL_GSPC = "^GSPC";
    public const string SYMBOL_SPY = "SPY";
    public const string SYMBOL_DJI = "^DJI";
    public const string SYMBOL_DIA = "DIA";
    public const string SYMBOL_FTSE = "^FTSE";
    public const string SYMBOL_STOXX = "^STOXX";
    public const string SYMBOL_GDAXI = "^GDAXI";
    public const string SYMBOL_SSMI = "^SSMI";
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
