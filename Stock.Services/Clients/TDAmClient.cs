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
  /// <summary>
  /// https://developer.tdameritrade.com/apis
  /// TO DO: Refactor
  /// </summary>
  public class TDAmClient
  {
    public static List<TDAmPriceCurrentResp> GetPriceCurrent(List<TDAmPriceCurrentReq> models)
    {
      var ret = new List<TDAmPriceCurrentResp>();
      if (models != null)
      {
        var results = new List<string>();
        foreach (var model in models)
        {
          var url = Consts.TDAM_URL_PRICE_CURRENT;
          var client = new CustomHttpClient(url);
          client.AppendParams(model, null);

          // There is an unknown limit on number of requests
          System.Threading.Thread.Sleep(1000);
          var result = client.GetAsync().Result;
          LogUtils.Debug(result);
          ret.Add(StringUtils.Deserialize<TDAmPriceCurrentResp>(
            result,
            new Dictionary<string, string>() { 
              { model.symbol, nameof(TDAmPriceCurrentResp.symbol) }
            }));
        }
      }
      return ret;
    }

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
