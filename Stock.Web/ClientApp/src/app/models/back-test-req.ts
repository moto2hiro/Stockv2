export class BackTestReq {
  Title: string;
  LookBack: number;
  //LookBackSegs?: number;
  SmoothingAlpha: number;
  PolynomialOrder: number;
  SupportResistanceProximityPct: number;
}
