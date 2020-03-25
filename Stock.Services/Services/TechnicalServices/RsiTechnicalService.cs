using Stock.Services.Models.EF;
using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Services.Services.TechnicalServices
{
  public class RsiTechnicalService
  {
    /// <summary>
    /// RSI = 100 - (100 / (1 + RS))
    //  RS = AvgGain / AvgLoss
    //  RS = 0 if AvgLoss = 0
    //  First AvgGain = Sum / 14
    //  First AvgLoss = Sum / 14
    //  Next AvgGain = (Prev AvgGain * 13 + CurrentGain) / 14
    //  Next AvgLoss = (Prev AvgLoss * 13 + CurrentLoss) / 14
    /// </summary>
    /// <returns></returns>
    public List<Technical> Calculate(int[] periods, List<StockPrice> items)
    {
      var models = new List<Technical>();
      if (periods == null || items == null) return models;

      foreach (var period in periods)
      {
        var sumGain = 0m;
        var sumLoss = 0m;
        var avgGains = new Dictionary<int, decimal>();
        var avgLosses = new Dictionary<int, decimal>();
        foreach (var item in items.Select((value, idx) => (value, idx)).ToList())
        {
          if (item.idx == 0)
          {
            continue;
          }

          var diff = items[item.idx].ClosePrice - items[item.idx - 1].ClosePrice;
          var currGain = (diff > 0) ? diff : 0;
          var currLoss = (diff < 0) ? Math.Abs(diff) : 0;
          var avgGain = 0m;
          var avgLoss = 0m;
          var isValid = false;
          sumGain += currGain;
          sumLoss += currLoss;
          if (item.idx == period)
          {
            avgGain = sumGain / period;
            avgLoss = sumLoss / period;
            avgGains.Add(item.idx, avgGain);
            avgLosses.Add(item.idx, avgLoss);
            isValid = true;
          }
          else if (item.idx > period)
          {
            avgGain = (avgGains[item.idx - 1] * (period - 1) + currGain) / period;
            avgLoss = (avgLosses[item.idx - 1] * (period - 1) + currLoss) / period;
            avgGains.Add(item.idx, avgGain);
            avgLosses.Add(item.idx, avgLoss);
            isValid = true;
          }

          if (isValid)
          {
            var rs = (avgLoss > 0) ? avgGain / avgLoss : 0;
            var rsi = NumberUtils.Round(100 - (100 / (1 + rs)));
            models.Add(new Technical()
            {
              StockPriceId = item.value.Id,
              CalcType = Consts.TECHNICAL_RSI,
              CalcPeriod = period,
              CalcValue = rsi
            });
          }
        }
      }

      return models;
    }
  }
}
