import { Component, OnInit } from '@angular/core';
import { StockService } from '../../services/stock.service';
import { Consts } from '../../models/consts';
import { ChartImage } from '../../models/chart-image';
import { ActivatedRoute } from '@angular/router';
import { StringUtils } from '../../utils/string-utils'

@Component({
  selector: 'app-view-charts',
  templateUrl: './view-charts.component.html'
})
export class ViewChartsComponent implements OnInit {
  public charts: ChartImage[] = [];
  public version: number = 0;

  constructor(
    private stockService: StockService,
    private activeRoute: ActivatedRoute,
    private stringUtils: StringUtils) { }

  ngOnInit() {
    this.version =  this.stringUtils.toInt(this.activeRoute.snapshot.paramMap.get('version'));
    this.stockService.getRandomCharts(this.version).subscribe(resp => {
      this.charts = resp;
      this.charts.forEach(chart => {
        chart.XImage = `${Consts.GOOGLE_CHART_IMAGE_PREFIX}${chart.XImage}`;
      });
    });
  }
}
