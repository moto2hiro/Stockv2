using Stock.Services.Models;
using Stock.Services.Models.EF;
using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stock.Services.Services
{
  public interface IQueryService
  {
    PastAvgResp GetPastAvgs(int version);
    PastAvgResp GetPastAvgs(int version, string symbol);
    List<ChartImage> GetPredictions(DateTime dateFrom);
  }

  public class QueryService : BaseService, IQueryService
  {
    public PastAvgResp GetPastAvgs(int version)
    {
      return GetPastAvgs(version, "");
    }

    public PastAvgResp GetPastAvgs(int version, string symbol)
    {
      var ret = new PastAvgResp() { };
      var pastAvgs = new List<PastAvg>();
      foreach (var period in Consts.PERFORMANCE_PERIODS)
      {
        var date = DateTime.Now.AddDays(-1 * period);
        var query = DB.ChartImage.AsQueryable();
        query = query.Where(c => c.YActual != null && c.YPredicted != null);
        query = query.Where(c => c.PriceDate > date);
        query = query.Where(c => c.Version == version);
        if (!string.IsNullOrEmpty(symbol)) query = query.Where(c => c.Symbol == symbol);
        var noCorrect = query.Where(c => c.YActual.Value == c.YPredicted.Value).Count();
        var noIncorrect = query.Where(c => c.YActual.Value != c.YPredicted.Value).Count();
        var sum = (noCorrect > 0 && noIncorrect > 0) ? noCorrect + noIncorrect : 1;
        var avg = NumberUtils.Round((noCorrect / (decimal)sum) * 100, 2);
        pastAvgs.Add(new PastAvg() { Period = period, Avg = avg });
      }
      ret.PastAvgs = pastAvgs;
      return ret;
    }

    public List<ChartImage> GetPredictions(DateTime dateFrom)
    {
      return DB.ChartImage
        .Where(c => c.PriceDate >= dateFrom && c.YPredicted != null)
        .Select(c => new ChartImage()
        {
          Symbol = c.Symbol,
          PriceDate = c.PriceDate,
          YPredicted = c.YPredicted,
          YPredictedProbability = c.YPredictedProbability
        }).ToList();
    }
  }
}
