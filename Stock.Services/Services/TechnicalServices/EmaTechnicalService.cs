using Stock.Services.Models.EF;
using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Services.Services.TechnicalServices
{
  public class EmaTechnicalService
  {
    /// <summary>
    /// EMA for t1 = Price of t1 
    /// EMA for > t1 = Alpha * Price + (1 - Alpha) * Previous EMA
    /// where Alpha = 2 / (N + 1)
    /// </summary>
    /// <returns></returns>
    public List<Technical> Calculate(int currIdx, int[] periods, List<StockPrice> items)
    {
      var models = new List<Technical>();
      if (periods == null || items == null) return models;
      if (items.ElementAtOrDefault(currIdx - periods.Max() + 1) == null) return models;

      foreach(var period in periods)
      {
        var alpha = 2m / (period + 1);
        var emas = new List<decimal>();
        var targetItems = items.GetRange(currIdx - period + 1, period);
        foreach(var item in targetItems.Select((value, idx) => (value, idx)))
        {
          if (item.idx == 0)
          {
            emas.Add(item.value.ClosePrice);
          }
          else
          {
            var ema = alpha * item.value.ClosePrice + (1 - alpha) * emas[item.idx - 1];
            emas.Add(ema);
          }
        }
        models.Add(new Technical()
        {
          StockPriceId = items[currIdx].Id,
          CalcType = Consts.TECHNICAL_EMA,
          CalcPeriod = period,
          CalcValue = NumberUtils.Round(emas.Last())
        });
      }
      return models;
    }
  }
}
