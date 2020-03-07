export class Consts {

  // #region Stock.Web Consts

  // #region Colors
  public static readonly COLOR_BLACK = '#000000';
  public static readonly COLOR_RED = '#ff0000';
  public static readonly COLOR_RED_INVERSE = '#00FFFF';
  public static readonly COLOR_RED_DARK = '#8b0000';
  public static readonly COLOR_RED_DARK_INVERSE = '#74FFFF';
  public static readonly COLOR_GREEN = '#00ff00';
  public static readonly COLOR_GREEN_INVERSE = '#FF00FF';
  public static readonly COLOR_GREEN_DARK = '#013220';
  public static readonly COLOR_GREEN_DARK_INVERSE = '#FECDDF';
  public static readonly COLOR_YELLOW = '#ffff00';
  public static readonly COLOR_YELLOW_INVERSE = '#0000FF';
  public static readonly COLOR_ORANGE = '#ffa500';
  public static readonly COLOR_ORANGE_INVERSE = '#005AFF';
  public static readonly COLOR_PURPLE = '#6a0dad';
  public static readonly COLOR_PURPLE_INVERSE = '#95F252';
  public static readonly COLOR_BROWN = '#964b00';
  public static readonly COLOR_BROWN_INVERSE = '#69B4FF';
  public static readonly COLOR_TURQUOISE = '#40e0d0';
  public static readonly COLOR_TURQUOISE_INVERSE = '#BF1F2F';
  // #endregion

  // #region Charts
  public static readonly CHART_CANDLE_HEIGHT = 182;
  public static readonly CHART_CANDLE_WIDTH = 224;
  public static readonly CHART_VOLUME_HEIGHT = 42;
  public static readonly CHART_VOLUME_WIDTH = 224;
  public static readonly GOOGLE_CHART_IMAGE_PREFIX = 'data:image/png;base64,';
  // #endregion

  // #endregion

  // #region Stock.Services Consts

  public static readonly CLASS_UP = 1;
  public static readonly CLASS_DOWN = 0;
  public static readonly SYMBOL_DOW_JONES = "^DJI";
  public static readonly SYMBOL_DOW_ETF = "DIA";
  public static readonly PERFORMANCE_PERIODS = [5, 10, 20, 50, 200];
  public static readonly AVG_TYPE_UP = "Up";
  public static readonly AVG_TYPE_DOWN = "Down";
  public static readonly AVG_TYPE_TOTAL = "Total";
  public static readonly AVG_TYPES = [ Consts.AVG_TYPE_UP, Consts.AVG_TYPE_DOWN, Consts.AVG_TYPE_TOTAL];
    
  // #region Charts
  public static readonly CHART_IMAGE_WIDTH = 224;
  public static readonly CHART_IMAGE_HEIGHT = 224;
  public static readonly CHART_IMAGE_WIDTH_V2 = 448;
  public static readonly CHART_IMAGE_HEIGHT_V2 = 448;
  public static readonly DEFAULT_CHART_PERIOD = 50;
  public static readonly CHART_V0 = 0;
  public static readonly CHART_V1 = 1;
  public static readonly CHART_V2 = 2;
  public static readonly CHART_V3 = 3;
  public static readonly CHART_V4 = 4;
  // #endregion

  // #region Stock Analysis
  public static readonly PRICE_VOLUME_THRESHOLD = 20000000;
  public static readonly TRADING_VOLUME_THRESHOLD = 1000000;
  public static readonly RSI_HIGH_THRESHOLD = 70;
  public static readonly RSI_MED_HIGH_THRESHOLD = 65;
  public static readonly RSI_LOW_THRESHOLD = 30;
  public static readonly RSI_MED_LOW_THRESHOLD = 35;
  // TODO? public static readonly PERIOD_METRIC_EMA = "EMA";
  // TODO? public static readonly PERIOD_METRIC_MACD = "MACD";
  // TODO? public static readonly PERIOD_METRIC_OBV = "OBV";
  public static readonly PERIOD_METRIC_SMA = "SMA";
  public static readonly PERIOD_METRIC_ADTV = "ADTV";
  public static readonly PERIOD_METRIC_ADPV = "ADPV";
  public static readonly PERIOD_METRIC_RSI = "RSI";
  public static readonly PERIOD_METRIC_AvgGain = "AvgGain";
  public static readonly PERIOD_METRIC_AvgLoss = "AvgLoss";
  public static readonly MAX_CALC_PERIOD = 200;
  public static readonly SMA_PERIOD_10 = 10;
  public static readonly SMA_PERIOD_20 = 20;
  public static readonly SMA_PERIOD_50 = 50;
  public static readonly SMA_PERIOD_100 = 100;
  public static readonly SMA_PERIOD_200 = 200;
  public static readonly EMA_PERIOD_5 = 5;
  public static readonly EMA_PERIOD_9 = 9;
  public static readonly EMA_PERIOD_10 = 10;
  public static readonly EMA_PERIOD_12 = 12;
  public static readonly EMA_PERIOD_26 = 26;
  public static readonly ADTV_PERIOD_20 = 20;
  public static readonly ADTV_PERIOD_30 = 30;
  public static readonly ADTV_PERIOD_50 = 50;
  public static readonly ADPV_PERIOD_20 = 20;
  public static readonly ADPV_PERIOD_30 = 30;
  public static readonly ADPV_PERIOD_50 = 50;
  public static readonly RSI_PERIOD_10 = 10;
  public static readonly RSI_PERIOD_14 = 14;
  public static readonly LOCAL_MAX_PERIOD_10 = 10;
  public static readonly LOCAL_MAX_PERIOD_20 = 20;
  public static readonly LOCAL_MAX_PERIOD_50 = 50;
  public static readonly LOCAL_MIN_PERIOD_10 = 10;
  public static readonly LOCAL_MIN_PERIOD_20 = 20;
  public static readonly LOCAL_MIN_PERIOD_50 = 50;
  public static readonly BOLLINGER_PERIOD_20 = 20;
  // #endregion

  // #endregion
}
