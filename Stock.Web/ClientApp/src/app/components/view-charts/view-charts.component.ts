import { Component, OnInit } from '@angular/core';
import { StockService } from '../../services/stock.service';
import { Consts } from '../../models/consts';
import { GoogleCharts } from 'google-charts';
import { StockPrice } from '../../models/stock-price';
import { ChartService } from '../../services/chart.service';
import { BackTestReq } from '../../models/back-test-req';

@Component({
  selector: 'app-view-charts',
  templateUrl: './view-charts.component.html'
})
export class ViewChartsComponent implements OnInit {
  periods: StockPrice[][];
  req: BackTestReq;

  constructor(
    private stockService: StockService,
    private chartService: ChartService) { }

  ngOnInit() {
    GoogleCharts.load(() => this.onLoadGoogleChartsLibrary());
  }

  onLoadGoogleChartsLibrary() {

    // Request
    var self = this;
    self.req = {
      Title: '',
      LookBack: 100,
      SmoothingAlpha: 0.5,
    };

    self.stockService.getPricesForCharts(this.req).subscribe(resp => {
      if (!resp || resp.length === 0) {
        return;
      }

      self.periods = resp;
      self.periods.forEach((period, i) => {

        // Transform Date
        var candleItems = [];
        period.forEach((item, k) => {
          candleItems.push([
            item.PriceDate.toString(),
            item.LowPrice * 0.99,
            item.OpenPrice * 0.99,
            item.ClosePrice * 0.99,
            item.HighPrice * 0.99,
            item.SmoothedPrice,
            (item.IsLocalMax || item.IsLocalMin) ? item.SmoothedPrice : null
          ]);
        });

        // Get Candle Chart
        var candleChart = self.chartService.getCandleChartObj('chart_div');

        // Add Series
        var smoothedSeries = self.chartService.getLineSeries(Consts.COLOR_YELLOW);
        var localMaxMins = self.chartService.getScatterSeries(Consts.COLOR_ORANGE);

        // Draw
        candleChart.draw(
          self.chartService.getGoogleArray(candleItems, true),
          self.chartService.getCandleChartOptions(
            500,
            900,
            [smoothedSeries, localMaxMins])
        );

      });
    });

  }

}
