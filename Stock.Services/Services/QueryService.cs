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
    PastAvgResp GetTopPastAvgs(int take);
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
      foreach (var period in Consts.AVG_PERIODS)
      {
        foreach (var avgType in Consts.AVG_TYPES)
        {
          var date = DateTime.Now.AddDays(-1 * period);
          var query = DB.ChartImage.AsQueryable();
          query = query.Where(c => c.YActual != null && c.YPredicted != null);
          query = query.Where(c => c.PriceDate > date);
          query = query.Where(c => c.Version == version);
          if (!string.IsNullOrEmpty(symbol))
          {
            query = query.Where(c => c.Symbol == symbol);
          }
          var noCorrect = 0;
          var noIncorrect = 0;
          if (avgType == Consts.AVG_TYPE_UP)
          {
            query = query.Where(c => c.YActual.Value == Consts.CLASS_UP);
          }
          else if (avgType == Consts.AVG_TYPE_DOWN)
          {
            query = query.Where(c => c.YActual.Value == Consts.CLASS_DOWN);
          }
          noCorrect = query.Where(c => c.YActual.Value == c.YPredicted.Value).Count();
          noIncorrect = query.Where(c => c.YActual.Value != c.YPredicted.Value).Count();
          pastAvgs.Add(new PastAvg()
          {
            AvgType = avgType,
            Period = period,
            NoCorrect = noCorrect,
            NoIncorrect = noIncorrect
          });
        }
      }
      ret.PastAvgs = pastAvgs;
      return ret;
    }

    public PastAvgResp GetTopPastAvgs(int take)
    {
      return null;
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
