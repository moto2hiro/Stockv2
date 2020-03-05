export class SaveChartImageReq {
  Symbol: string;
  PriceDate: Date;
  YActual?: number;
  CandleImage: string;
  VolumeImage: string;
  Version: number;
}
