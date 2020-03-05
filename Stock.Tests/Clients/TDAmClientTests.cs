using NUnit.Framework;
using Stock.Services;
using Stock.Services.Clients;
using Stock.Services.Models.TDAm;
using System.Collections.Generic;

namespace Stock.Tests.Clients
{
  [TestFixture()]
  public class TDAmClientTests : BaseTest
  {
    [Test()]
    public void GetPriceHistory_Should_Return_Prices()
    {
      // ARRANGE
      var req = new List<TDAmPriceHistoryReq>();
      var symbols = new string[] { "MSFT", "AMZN", "AAPL", "MA" };
      foreach(var symbol in symbols)
      {
        req.Add(new TDAmPriceHistoryReq() {
          Symbol = symbol,
          apikey = Configs.TDAmApiKey,
          periodType = Consts.TDAM_PERIOD_MONTH,
          period = 1,
          frequencyType = Consts.TDAM_FREQUENCY_DAILY,
          frequency = 1
        });
      }

      // ACT
      var items = TDAmClient.GetPriceHistory(req);

      // ASSERT
      Assert.IsNotNull(items);
      Assert.IsTrue(items.Count > 0);
      Assert.AreEqual(symbols.Length, items.Count);
    }
  }
}
