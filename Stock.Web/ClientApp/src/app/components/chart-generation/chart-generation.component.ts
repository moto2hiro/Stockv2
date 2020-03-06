import { Component, OnInit } from '@angular/core';
import { StockService } from '../../services/stock.service';
import { GoogleCharts } from 'google-charts';
import { Consts } from '../../models/consts';
import { SaveChartImageReq } from '../../models/save-chart-image-req';
import { ActivatedRoute } from '@angular/router';
import { GetPeriodsOfStockPricesReq } from '../../models/get-periods-of-stock-prices-req';
import { StringUtils } from '../../utils/string-utils';

@Component({
  selector: 'app-chart-generation',
  templateUrl: './chart-generation.component.html'
})
export class ChartGenerationComponent implements OnInit {
  public allItems = [];
  public candleImages: string[] = [];
  public volumeImages: string[] = [];
  public models: SaveChartImageReq[] = [];
  public finalCount: number;
  public isProcessing: boolean = false;
  public isDoAllSymbols: boolean = false;
  public getDataReq: GetPeriodsOfStockPricesReq = new GetPeriodsOfStockPricesReq();

  readonly TEST_SYMBOL: string = 'MSFT';
  readonly TEST_TAKE: number = 10;
  readonly MAX_TAKE: number = 100;
  readonly MIN_SKIP: number = 0;

  constructor(
    private stockService: StockService,
    private stringUtils: StringUtils,
    private activeRoute: ActivatedRoute) { }

  ngOnInit() {
    this.getDataReq.Version = Consts.CHART_V1;
    this.getDataReq.NoOfPeriods = Consts.DEFAULT_CHART_PERIOD;
    this.getDataReq.Take = this.MAX_TAKE;
    this.getDataReq.Skip = this.stringUtils.toInt(this.activeRoute.snapshot.paramMap.get('skip'));
    this.getDataReq.Symbol = this.activeRoute.snapshot.paramMap.get('symbol');
    this.isDoAllSymbols = (this.getDataReq.Symbol) ? true : false;
    if (!this.getDataReq.Symbol) {
      this.getDataReq.Symbol = this.TEST_SYMBOL;
      this.getDataReq.Skip = 0;
      this.getDataReq.Take = this.TEST_TAKE;
    }
    this.getDataReq.Symbol = this.getDataReq.Symbol.toUpperCase();
    GoogleCharts.load(() => this.onLoadGoogleChartsLibrary());
  }

  onLoadGoogleChartsLibrary() {
    var self = this;
    self.stockService.getPeriodsOfStockPrices(this.getDataReq).subscribe(response => {
      if (response && response.length > 0) {
        self.allItems = response;
        self.finalCount = response.length;

        response.forEach((items, j) => {

          var candleItems = [];
          var volumeItems = [];
          items.forEach((item, k) => {
            candleItems.push([
              item.PriceDate.toString(),
              item.LowPrice,
              item.OpenPrice,
              item.ClosePrice,
              item.HighPrice,
              item.SMA_20,
              item.SMA_50,
              item.SMA_200,
              item.BollingerUpperStvDev2_20,
              item.BollingerLowerStvDev2_20
            ]);
            volumeItems.push([
              item.PriceDate.toString(),
              item.Volume,
              self.getVolumeColor(self.getDataReq.Symbol, item.IsCloseGreaterToday)
            ]);
          });
          volumeItems.splice(0, 0, ['Date', 'Volume', { role: 'style' }]);

          // Draw Candle Chart
          var candleChart = self.getCandleChartObj();
          GoogleCharts.api.visualization.events.addListener(
            candleChart,
            'ready',
            function () {
              self.candleImages.push(self.getImageBitArray(candleChart));
              if (self.candleImages.length === self.finalCount && self.volumeImages.length === self.finalCount) {
                self.doProcess();
              }
            }
          );

          // Draw Volume Chart
          var volumeChart = self.getColumnChartObj();
          GoogleCharts.api.visualization.events.addListener(
            volumeChart,
            'ready',
            function () {
              self.volumeImages.push(self.getImageBitArray(volumeChart));
              if (self.candleImages.length === self.finalCount && self.volumeImages.length === self.finalCount) {
                self.doProcess();
              }
            }
          );
          volumeChart.draw(self.getGoogleArray(volumeItems, false), self.getVolumeChartOptions());
          candleChart.draw(self.getGoogleArray(candleItems, true), self.getCandleChartOptions(self.getDataReq.Symbol));
        });
      } else {
        self.doProcess();
      }
    });
  }

  doProcess() {
    if (!this.isProcessing && this.isDoAllSymbols) {
      this.isProcessing = true;
      this.models = [];
      for (var i = 0; i < this.finalCount; i++) {
        var lastIndex = Consts.DEFAULT_CHART_PERIOD - 1;
        this.models.push({
          Symbol: this.allItems[i][lastIndex].Symbol,
          PriceDate: this.allItems[i][lastIndex].PriceDate,
          YActual: this.allItems[i][lastIndex].YActual,
          CandleImage: this.candleImages[i],
          VolumeImage: this.volumeImages[i],
          Version: Consts.CHART_V1
        });
      }

      // Refresh on finish w/next symbol
      // to prevent running out of memory
      this.stockService.saveChartImages(this.models).subscribe(response => {

        // Periods left for this Symbol
        if (this.allItems.length > 0) {

          var nextSkip = this.getDataReq.Skip + this.MAX_TAKE;
          var nextSymbol = this.getDataReq.Symbol;
          var path = `/chart-generation/${nextSkip}/${nextSymbol}`;
          window.location.replace(path);

          // Next Symbol
        } else {

          this.stockService.getSymbols().subscribe(symbols => {

            if (symbols) {
              var index = symbols.findIndex(s => s.Symbol.toLowerCase() === this.getDataReq.Symbol.toLowerCase());
              if (index !== -1 && index !== symbols.length - 1) {
                var nextSymbol = symbols[index + 1].Symbol;
                var path = `/chart-generation/0/${nextSymbol}`;
                window.location.replace(path);
              }
            }

          });

        }

      });
    }
  }

  getImageBitArray(chart) {
    var uri = chart.getImageURI();
    return (uri) ? uri.replace(Consts.GOOGLE_CHART_IMAGE_PREFIX, '') : '';
  }

  getCandleChartObj() {
    var container = document.getElementById('chart_div').appendChild(document.createElement('div'));
    return new GoogleCharts.api.visualization.CandlestickChart(container);
  }

  getColumnChartObj() {
    var container = document.getElementById('chart_div').appendChild(document.createElement('div'));
    return new GoogleCharts.api.visualization.ColumnChart(container);
  }

  getGoogleArray(arrayItems, isFirstRowData) {
    return GoogleCharts.api.visualization.arrayToDataTable(arrayItems, isFirstRowData);
  }

  getVolumeColor(symbol: string, isCloseGreaterToday: boolean) {
    return (this.isDowJones(symbol) && isCloseGreaterToday) ? Consts.COLOR_GREEN_DARK_INVERSE :
      (this.isDowJones(symbol) && !isCloseGreaterToday) ? Consts.COLOR_RED_DARK_INVERSE :
        (!this.isDowJones(symbol) && isCloseGreaterToday) ? Consts.COLOR_GREEN_DARK :
          Consts.COLOR_RED_DARK;
  }

  getCandleChartOptions(symbol: string) {
    return {
      legend: 'none',
      backgroundColor: Consts.COLOR_BLACK,
      height: Consts.CHART_CANDLE_HEIGHT,
      width: Consts.CHART_CANDLE_WIDTH,
      candlestick: {
        fallingColor: {
          fill: (this.isDowJones(symbol)) ? Consts.COLOR_RED_INVERSE : Consts.COLOR_RED,
          stroke: (this.isDowJones(symbol)) ? Consts.COLOR_RED_INVERSE : Consts.COLOR_RED
        },
        risingColor: {
          fill: (this.isDowJones(symbol)) ? Consts.COLOR_GREEN_INVERSE : Consts.COLOR_GREEN,
          stroke: (this.isDowJones(symbol)) ? Consts.COLOR_GREEN_INVERSE : Consts.COLOR_GREEN
        }
      },
      chartArea: {
        backgroundColor: Consts.COLOR_BLACK,
        height: Consts.CHART_CANDLE_HEIGHT,
        width: Consts.CHART_CANDLE_WIDTH
      },
      hAxis: {
        baselineColor: Consts.COLOR_BLACK,
        gridlines: { count: 0 },
        minorGridlines: { count: 0 }
      },
      vAxis: {
        baselineColor: Consts.COLOR_BLACK,
        gridlines: { count: 0 },
        minorGridlines: { count: 0 }
      },
      seriesType: 'candlesticks',
      series: {
        1: {
          type: 'line',
          lineWidth: 1,
          color: (this.isDowJones(symbol)) ? Consts.COLOR_YELLOW_INVERSE : Consts.COLOR_YELLOW
        },
        2: {
          type: 'line',
          lineWidth: 1,
          color: (this.isDowJones(symbol)) ? Consts.COLOR_ORANGE_INVERSE : Consts.COLOR_ORANGE
        },
        3: {
          type: 'line',
          lineWidth: 1,
          color: (this.isDowJones(symbol)) ? Consts.COLOR_PURPLE_INVERSE : Consts.COLOR_PURPLE
        },
        4: {
          type: 'line',
          lineWidth: 1,
          color: (this.isDowJones(symbol)) ? Consts.COLOR_BROWN_INVERSE : Consts.COLOR_BROWN
        },
        5: {
          type: 'line',
          lineWidth: 1,
          color: (this.isDowJones(symbol)) ? Consts.COLOR_BROWN_INVERSE : Consts.COLOR_BROWN
        }
      }
    };
  }

  getVolumeChartOptions() {
    return {
      legend: 'none',
      backgroundColor: Consts.COLOR_BLACK,
      height: Consts.CHART_VOLUME_HEIGHT,
      width: Consts.CHART_VOLUME_WIDTH,
      chartArea: {
        backgroundColor: Consts.COLOR_BLACK,
        height: Consts.CHART_VOLUME_HEIGHT,
        width: Consts.CHART_VOLUME_WIDTH
      },
      hAxis: {
        baselineColor: Consts.COLOR_BLACK,
        gridlines: { count: 0 },
        minorGridlines: { count: 0 }
      },
      vAxis: {
        baselineColor: Consts.COLOR_BLACK,
        gridlines: { count: 0 },
        minorGridlines: { count: 0 }
      }
    };
  }

  isDowJones(symbol: string) {
    return (symbol === Consts.SYMBOL_DOW_JONES) || (symbol === Consts.SYMBOL_DOW_ETF);
  }
}
