using Stock.Services.Models;
using Stock.Services.Models.EF;
using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Stock.Services.Services
{
  public interface IAnalysisService
  {
    void BackTest(string title);
  }

  public class AnalysisService : BaseService, IAnalysisService
  {
    // TO DO: Do injection.
    private StockService _stockService = new StockService();

    private const decimal DEFAULT_CAPITAL = 10000;

    public void BackTest(string title)
    {
      var result = new BackTestResult();
      var benchmarkResult = new BackTestResult();
      var symbols = _stockService.GetSymbols();
      if (symbols == null)
      {
        return;
      }

      foreach (var symbol in symbols)
      {
        if (symbol.Symbol != Consts.SYMBOL_SPY)
        {
          continue;
        }

        var items = DB.StockPrice.Where(s => s.SymbolId == symbol.Id).OrderBy(s => s.PriceDate).ToList();

        //// Create Data
        //foreach (var item in items.Select((model, idx) => (model, idx)))
        //{
        //  var currIdx = item.idx;
        //  var prevIdx = currIdx - 1;
        //  var nextIdx = currIdx + 1;
        //  // Need at least 201 days for calculation
        //  if (currIdx <= Consts.MAX_CALC_PERIOD)
        //  {
        //    continue;
        //  }



        //}


        // Create Transactions
        result.Transactions = new List<Transaction>();
        benchmarkResult.Transactions = new List<Transaction>();
        foreach (var item in items.Select((model, idx) => (model, idx)))
        {
          if (item.model.PriceDate.DayOfWeek == DayOfWeek.Tuesday)
          {
            result.Transactions.Add(new Transaction()
            {
              Symbol = Consts.SYMBOL_SPY,
              StartDate = item.model.PriceDate,
              EndDate = item.model.PriceDate,
              Action = Consts.ACTION_BUY,
              StartPrice = item.model.OpenPrice,
              EndPrice = item.model.ClosePrice,
              Volume = NumberUtils.Floor(DEFAULT_CAPITAL / item.model.OpenPrice)
            });
          }
          result.TtlNoDays += 1;

          benchmarkResult.Transactions.Add(new Transaction()
          {
            Symbol = Consts.SYMBOL_SPY,
            StartDate = item.model.PriceDate,
            EndDate = item.model.PriceDate,
            Action = Consts.ACTION_BUY,
            StartPrice = item.model.OpenPrice,
            EndPrice = item.model.ClosePrice,
            Volume = NumberUtils.Floor(DEFAULT_CAPITAL / item.model.OpenPrice)
          });
          benchmarkResult.TtlNoDays += 1;
        }
      }

      // Log Back Test Results
      LogUtils.Debug($"Title=Benchmark");
      LogUtils.Debug($"Total Change in Capital={benchmarkResult.TtlChangeInCapital}");
      LogUtils.Debug($"Average % Change in Capital={benchmarkResult.AvgPctChangeInCapital}%");
      LogUtils.Debug($"Max % Change in Capital={benchmarkResult.MaxPctChangeInCapital}%");
      LogUtils.Debug($"Min % Change in Capital={benchmarkResult.MinPctChangeInCapital}%");
      LogUtils.Debug($"Avg $ Change in Capital=${benchmarkResult.AvgChangeInCapital}");
      LogUtils.Debug($"Max $ Change in Capital=${benchmarkResult.MaxChangeInCapital}");
      LogUtils.Debug($"Min $ Change in Capital=${benchmarkResult.MinChangeInCapital}");
      LogUtils.Debug($"Total Number of Days={benchmarkResult.TtlNoDays}");
      LogUtils.Debug($"Total Number of Transactions={benchmarkResult.TtlNoTrans} days");
      LogUtils.Debug($"Percent Transactions Available={benchmarkResult.PctTransAvailable}%");
      LogUtils.Debug($"Total Number of Profit Transactions={benchmarkResult.TtlNoProfitTrans} days");
      LogUtils.Debug($"Percent of Profit Transactions={benchmarkResult.PctProfitTrans}%");
      LogUtils.Debug($"Average Hold Length={benchmarkResult.AvgHoldLength} days");
      LogUtils.Debug($"Max Hold Length={benchmarkResult.MaxHoldLength} days");
      LogUtils.Debug($"Min Hold Length={benchmarkResult.MinHoldLength} days");
      LogUtils.Debug("");
      LogUtils.Debug("");

      // Log Back Test Results
      LogUtils.Debug($"Title={title}");
      LogUtils.Debug($"Total Change in Capital={result.TtlChangeInCapital}");
      LogUtils.Debug($"Average % Change in Capital={result.AvgPctChangeInCapital}%");
      LogUtils.Debug($"Max % Change in Capital={result.MaxPctChangeInCapital}%");
      LogUtils.Debug($"Min % Change in Capital={result.MinPctChangeInCapital}%");
      LogUtils.Debug($"Avg $ Change in Capital=${result.AvgChangeInCapital}");
      LogUtils.Debug($"Max $ Change in Capital=${result.MaxChangeInCapital}");
      LogUtils.Debug($"Min $ Change in Capital=${result.MinChangeInCapital}");
      LogUtils.Debug($"Total Number of Days={result.TtlNoDays}");
      LogUtils.Debug($"Total Number of Transactions={result.TtlNoTrans} days");
      LogUtils.Debug($"Percent Transactions Available={result.PctTransAvailable}%");
      LogUtils.Debug($"Total Number of Profit Transactions={result.TtlNoProfitTrans} days");
      LogUtils.Debug($"Percent of Profit Transactions={result.PctProfitTrans}%");
      LogUtils.Debug($"Average Hold Length={result.AvgHoldLength} days");
      LogUtils.Debug($"Max Hold Length={result.MaxHoldLength} days");
      LogUtils.Debug($"Min Hold Length={result.MinHoldLength} days");
      LogUtils.Debug("");
      LogUtils.Debug("");
    }
  }
}
