export class StockPrice {
  Id: number;
  SymbolId: number;
  OpenPrice: number;
  HighPrice: number;
  LowPrice: number;
  ClosePrice: number;
  Volume: number;
  PriceDate: Date;

  //#region NotMapped

  SmoothedPrice: number;
  IsLocalMax: boolean;
  IsLocalMin: boolean;

  //#endregion
}
