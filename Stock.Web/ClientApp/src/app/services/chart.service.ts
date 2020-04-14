import { Injectable } from '@angular/core';
import { Consts } from '../models/consts';
import { GoogleCharts } from 'google-charts';

@Injectable({
  providedIn: 'root'
})
export class ChartService {
  static readonly serviceUrl: string = '/api/stock';

  constructor() { }

  getCandleChartObj(divId) {
    var container = document.getElementById(divId).appendChild(document.createElement('div'));
    return new GoogleCharts.api.visualization.CandlestickChart(container);
  }

  getGoogleArray(arrayItems, isFirstRowData) {
    return GoogleCharts.api.visualization.arrayToDataTable(arrayItems, isFirstRowData);
  }

  getCandleChartOptions(h, w, extraSeries) {
    var ret = {
      legend: 'none',
      backgroundColor: Consts.COLOR_BLACK,
      height: h,
      width: w,
      candlestick: {
        fallingColor: {
          fill: Consts.COLOR_RED,
          stroke: Consts.COLOR_RED
        },
        risingColor: {
          fill: Consts.COLOR_GREEN,
          stroke: Consts.COLOR_GREEN
        }
      },
      chartArea: {
        backgroundColor: Consts.COLOR_BLACK,
        height: h,
        width: w
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
      series: {}
    };

    if (extraSeries) {
      extraSeries.forEach((item, i) => {
        ret.series[i + 1] = item;
      });
    }

    return ret;
  }

  getLineSeries(color) {
    return {
      type: 'line',
      lineWidth: 1,
      color: color
    };
  }

  getScatterSeries(color) {
    return {
      type: 'scatter',
      pointSize: 5,
      color: color,
      dataOpacity: 0.5
    };
  }
}
