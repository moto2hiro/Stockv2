using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stock.Services.Models;
using Stock.Services.Models.EF;
using Stock.Services.Services;

namespace Stock.Web.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class StockController : ControllerBase
  {
    private readonly IStockService _StockService;
    private readonly IAnalysisService _AnalysisService;

    public StockController(IStockService stockService, IAnalysisService analysisService)
    {
      _StockService = stockService;
      _AnalysisService = analysisService;
    }

    [HttpPost("getPricesForCharts")]
    public List<List<StockPrice>> GetPricesForCharts(BackTestReq req)
    {
      return _AnalysisService.GetPricesForCharts(req);
    }

    //[HttpGet("symbols")]
    //public List<SymbolMaster> GetSymbols()
    //{
    //  return _StockService.GetSymbols();
    //}

    //[HttpPost("saveChartImages")]
    //public int SaveChartImages(List<SaveChartImageReq> models)
    //{
    //  return _StockService.SaveChartImages(models);
    //}

    //[HttpPost("periods")]
    //public List<List<StockPrice>> GetPeriodsOfStockPrices(GetPeriodsOfStockPricesReq request)
    //{
    //  return _StockService.GetPeriodsOfStockPrices(request);
    //}

    //[HttpGet("random/{version}")]
    //public List<ChartImage> GetRandomCharts(int version)
    //{
    //  return _QueryService.GetRandomCharts(version);
    //}
  }
}
