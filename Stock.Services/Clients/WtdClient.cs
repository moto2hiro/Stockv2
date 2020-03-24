using Newtonsoft.Json.Linq;
using Stock.Services.Custom;
using Stock.Services.Models.Wtd;
using Stock.Services.Utils;
using System.Collections.Generic;

namespace Stock.Services.Clients
{
  /// <summary>
  /// https://www.worldtradingdata.com/documentation   
  /// TO DO: Refactor
  /// </summary>
  public class WtdClient
  {
    public static List<WtdPriceIntraResp> GetPriceIntra(WtdPriceIntraReq model)
    {
      var ret = new List<WtdPriceIntraResp>();
      if (model == null)
      {
        return ret;
      }

      // Create Client
      var client = new CustomHttpClient(Consts.WTD_URL_PRICE_INTRA);
      client.AppendParams(model, null);

      // Get Result
      var result = client.GetAsync().Result;
      LogUtils.Debug(result);

      // Parse Result
      var jObj = JObject.Parse(result);
      if (jObj == null || jObj["intraday"] == null)
      {
        return ret;
      }

      foreach (JProperty price in jObj["intraday"])
      {
        ret.Add(new WtdPriceIntraResp()
        {
          symbol = model.symbol,
          date = price.Name,
          timezone_name = jObj[nameof(WtdPriceIntraResp.timezone_name)].Value<string>(),
          open = price.Value[nameof(WtdPriceIntraResp.open)].Value<decimal>(),
          close = price.Value[nameof(WtdPriceIntraResp.close)].Value<decimal>(),
        });
      }

      return ret;
    }

    public static List<WtdPriceDailyResp> GetPriceDaily(WtdPriceDailyReq model)
    {
      var ret = new List<WtdPriceDailyResp>();
      if (model == null)
      {
        return ret;
      }

      // Create Client
      var client = new CustomHttpClient(Consts.WTD_URL_PRICE_DAILY);
      client.AppendParams(model, null);

      // Get Result
      var result = client.GetAsync().Result;
      LogUtils.Debug(result);

      // Parse Result
      var jObj = JObject.Parse(result);
      if (jObj == null || jObj["history"] == null)
      {
        return ret;
      }

      foreach (JProperty price in jObj["history"])
      {
        ret.Add(new WtdPriceDailyResp()
        {
          symbol = model.symbol,
          date = price.Name,
          open = price.Value[nameof(WtdPriceDailyResp.open)].Value<decimal>(),
          close = price.Value[nameof(WtdPriceDailyResp.close)].Value<decimal>(),
        });
      }

      return ret;
    }

  }
}
