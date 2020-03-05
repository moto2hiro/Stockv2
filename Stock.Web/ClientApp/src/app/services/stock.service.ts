import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { StockPrice } from '../models/stock-price';
import { SaveChartImageReq } from '../models/save-chart-image-req';
import { SymbolMaster } from '../models/symbol-master';
import { GetPeriodsOfStockPricesReq } from '../models/get-periods-of-stock-prices-req';

@Injectable({
  providedIn: 'root'
})
export class StockService {
  static readonly serviceUrl: string = '/api/stock';

  constructor(private http: HttpClient) { }

  getSymbols() {
    return this.http.get<SymbolMaster[]>(`${StockService.serviceUrl}/symbols`);
  }

  getPeriodsOfStockPrices(request: GetPeriodsOfStockPricesReq) {
    return this.http.post<StockPrice[][]>(`${StockService.serviceUrl}/periods`, request);
  }

  saveChartImages(models: SaveChartImageReq[]) {
    return this.http.post<number>(`${StockService.serviceUrl}/saveChartImages`, models);
  }
}