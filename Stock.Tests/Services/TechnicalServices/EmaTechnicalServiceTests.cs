using NUnit.Framework;
using Stock.Services;
using Stock.Services.Models.EF;
using Stock.Services.Services.TechnicalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Tests.Services.TechnicalServices
{
  [TestFixture()]
  public class EmaTechnicalServiceTests : BaseTest
  {
    [Test()]
    public void Calculate_Should_Return_If_Not_Enough_Items()
    {
      // ARRANGE
      var idx = 10;
      var periods = new int[] { 2, 4, 6 };
      var items = new List<StockPrice>();

      // ACT
      var result = new EmaTechnicalService().Calculate(idx, periods, items);

      // ASSERT
      Assert.IsNotNull(result);
      Assert.AreEqual(0, result.Count);
    }

    [Test()]
    public void Calculate_Should_Return_Ema()
    {
      // ARRANGE
      var currIdx = 3;
      var periods = new int[] { 2, 4 };
      var items = new List<StockPrice>() 
      { 
        new StockPrice() { ClosePrice = 2 },
        new StockPrice() { ClosePrice = 4 },
        new StockPrice() { ClosePrice = 6 },
        new StockPrice() { ClosePrice = 8 }
      };

      // ACT
      var result = new EmaTechnicalService().Calculate(currIdx, periods, items);

      // ASSERT
      Assert.IsNotNull(result);
      Assert.AreEqual(periods.Count(), result.Count);
      Assert.AreEqual(Consts.TECHNICAL_EMA, result[0].CalcType);
      Assert.AreEqual(7.33, result[0].CalcValue);
      Assert.AreEqual(5.65, result[1].CalcValue);
    }
  }
}
