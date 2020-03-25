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
  public class RsiTechnicalServiceTests : BaseTest
  {
    [Test()]
    public void Calculate_Should_Return_RSI()
    {
      // ARRANGE
      var periods = new int[] { 3 };
      var items = new List<StockPrice>() 
      { 
        new StockPrice() { ClosePrice = 12 }, new StockPrice() { ClosePrice = 14 },
        new StockPrice() { ClosePrice = 14 }, new StockPrice() { ClosePrice = 15 },
        new StockPrice() { ClosePrice = 13 }, new StockPrice() { ClosePrice = 12 },
        new StockPrice() { ClosePrice = 12 }, new StockPrice() { ClosePrice = 16 },
        new StockPrice() { ClosePrice = 18 }, new StockPrice() { ClosePrice = 20 },
        new StockPrice() { ClosePrice = 16 }, new StockPrice() { ClosePrice = 15 }
      };

      // ACT
      var result = new RsiTechnicalService().Calculate(periods, items);

      // ASSERT
      Assert.IsNotNull(result);
      Assert.AreEqual(Consts.TECHNICAL_RSI, result[0].CalcType);
      Assert.AreEqual(0, result[0].CalcValue);
      Assert.AreEqual(50, result[1].CalcValue);
      Assert.AreEqual(36.36, result[2].CalcValue);
      Assert.AreEqual(36.36, result[3].CalcValue);
      Assert.AreEqual(81.58, result[4].CalcValue);
      Assert.AreEqual(87.98, result[5].CalcValue);
    }
  }
}
