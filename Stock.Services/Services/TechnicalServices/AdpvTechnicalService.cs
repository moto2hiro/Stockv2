using Stock.Services.Models.EF;
using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Services.Services.TechnicalServices
{
  public class AdpvTechnicalService
  {
    ///// <summary>
    ///// Adtv = Total / Periods
    ///// </summary>
    //public List<Technical> Calculate(int currIdx, int[] periods, List<StockPrice> items)
    //{
    //  var models = new List<Technical>();
    //  if (periods == null || items == null) return models;
    //  if (items.ElementAtOrDefault(currIdx - periods.Max() + 1) == null) return models;

    //  foreach (var period in periods)
    //  {
    //    var targetItems = items.GetRange(currIdx - period + 1, period);
    //    models.Add(new Technical()
    //    {
    //      StockPriceId = items[currIdx].Id,
    //      CalcType = Consts.TECHNICAL_ADPV,
    //      CalcPeriod = period,
    //      CalcValue = NumberUtils.Round(targetItems.Average(t => t.PriceVolume))
    //    });
    //  }
    //  return models;
    //}
  }
}
