export class StockPrice {
  Id: number;
  Symbol: string;
  OpenPrice: number;
  HighPrice: number;
  LowPrice: number;
  ClosePrice: number;
  Volume: number;
  PriceDate: Date;
  PriceVolume: number;
  YActual?: boolean;
  IsCloseGreaterToday: boolean;
  SMA_10: number;
  SMA_20: number;
  SMA_50: number;
  SMA_100: number;
  SMA_200: number;
  EMA_5: number;
  EMA_9: number;
  EMA_10: number;
  EMA_12: number;
  EMA_26: number;
  ADTV_20: number;
  ADTV_30: number;
  ADTV_50: number;
  ADPV_20: number;
  ADPV_30: number;
  ADPV_50: number;
  RSI_10: number;
  RSI_14: number;
  AvgGain_10: number;
  AvgLoss_10: number;
  AvgGain_14: number;
  AvgLoss_14: number;
  IsLocalMax_10: boolean
  IsLocalMax_20: boolean
  IsLocalMax_50: boolean
  IsLocalMin_10: boolean
  IsLocalMin_20: boolean
  IsLocalMin_50: boolean
  BollingerUpperStvDev1_20: number;
  BollingerLowerStvDev1_20: number;
  BollingerUpperStvDev2_20: number;
  BollingerLowerStvDev2_20: number;
}
