using NUnit.Framework;
using Stock.Services;
using Stock.Services.Clients;
using Stock.Services.Models.Wtd;
using System;
using System.Collections.Generic;

namespace Stock.Tests.Clients
{
  [TestFixture()]
  public class WtdClientTests : BaseTest
  {
    [Test()]
    public void GetPriceIntra_Should_Return_Prices()
    {
      // ARRANGE
      var req = new WtdPriceIntraReq()
      {
        symbol = Consts.SYMBOL_FTSE,
        api_token = Configs.WtdApiKey,
        interval = 60,
        range = 30
      };

      // ACT
      var items = WtdClient.GetPriceIntra(req);

      // ASSERT
      Assert.IsNotNull(items);
      Assert.IsTrue(items.Count > 0);
    }

    [Test()]
    public void GetPriceDaily_Should_Return_Prices()
    {
      // ARRANGE
      var req = new WtdPriceDailyReq()
      {
        symbol = Consts.SYMBOL_SPY,
        api_token = Configs.WtdApiKey,
        date_from = DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd")
      };

      // ACT
      var items = WtdClient.GetPriceDaily(req);

      // ASSERT
      Assert.IsNotNull(items);
      Assert.IsTrue(items.Count > 0);
    }
  }
}
