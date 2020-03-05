using Stock.Services.Custom;
using Stock.Services.Models.TDAm;
using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Stock.Services.Clients
{
  // https://developer.tdameritrade.com/apis
  public class TDAmClient
  {
    public static List<TDAmPriceHistoryResp> GetPriceHistory(List<TDAmPriceHistoryReq> models)
    {
      var ret = new List<TDAmPriceHistoryResp>();
      if (models != null)
      {
        var results = new List<string>();
        foreach (var model in models)
        {
          var url = string.Format(Consts.TDAM_URL_PRICE_HISTORY, model.Symbol);
          var client = new CustomHttpClient(url);
          client.AppendParams(model, new string[] { nameof(TDAmPriceHistoryReq.Symbol) });

          // There is an unknown limit on number of requests
          System.Threading.Thread.Sleep(1000);
          var result = client.GetAsync().Result;
          LogUtils.Debug(result);
          results.Add(result);
        }
        
        if (results != null)
        {
          foreach(var result in results)
          {
            ret.Add(StringUtils.Deserialize<TDAmPriceHistoryResp>(result));
          }
        }
      }
      return ret;
    }
  }
}
