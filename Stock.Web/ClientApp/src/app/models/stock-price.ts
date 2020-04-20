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
  PolynomialPrice: number;
  IsPolynomialMax: boolean;
  IsPolynomialMin: boolean;
  IsLocalMax: boolean;
  IsLocalMin: boolean;
  SupportPriceTop1: number;
  SupportPriceTop2: number;
  ResistancePriceTop1: number;
  ResistancePriceTop2: number;

  //#endregion
}
