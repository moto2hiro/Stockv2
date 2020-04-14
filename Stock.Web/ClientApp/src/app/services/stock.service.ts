import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { StockPrice } from '../models/stock-price';
import { SaveChartImageReq } from '../models/save-chart-image-req';
import { SymbolMaster } from '../models/symbol-master';
import { ChartImage } from '../models/chart-image';
import { GetPeriodsOfStockPricesReq } from '../models/get-periods-of-stock-prices-req';
import { BackTestReq } from '../models/back-test-req';

@Injectable({
  providedIn: 'root'
})
export class StockService {
  static readonly serviceUrl: string = '/api/stock';

  constructor(private http: HttpClient) { }

  getPricesForCharts(req: BackTestReq) {
    return this.http.post<StockPrice[][]>(`${StockService.serviceUrl}/getPricesForCharts`, req);
  }

  getSymbols() {
    return this.http.get<SymbolMaster[]>(`${StockService.serviceUrl}/symbols`);
  }

  getPeriodsOfStockPrices(request: GetPeriodsOfStockPricesReq) {
    return this.http.post<StockPrice[][]>(`${StockService.serviceUrl}/periods`, request);
  }

  saveChartImages(models: SaveChartImageReq[]) {
    return this.http.post<number>(`${StockService.serviceUrl}/saveChartImages`, models);
  }

  getRandomCharts(version: number) {
    return this.http.get<ChartImage[]>(`${StockService.serviceUrl}/random/${version}`);
  }
}
