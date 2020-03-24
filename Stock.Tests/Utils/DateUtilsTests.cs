using NUnit.Framework;
using Stock.Services.Models;
using Stock.Services.Models.CsvHelper;
using Stock.Services.Models.EF;
using Stock.Services.Services;
using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace Stock.Tests.Utils
{
  [TestFixture()]
  public class DateUtilsTests : BaseTest
  {
    [Test()]
    public void ToUtc_Should_Return_Utc()
    {
      // ARRANGE
      var now = new DateTime(
        DateTime.UtcNow.Year,
        DateTime.UtcNow.Month,
        DateTime.UtcNow.Day,
        DateTime.UtcNow.Hour,
        0,
        0);
      var testCases = new Dictionary<string, int>
      {
        { "UTC", 0 },
        { "Central European Standard Time", 1 },
      };

      // ACT
      var results = new List<DateTime>();
      foreach (var testCase in testCases)
      {
        results.Add(DateUtils.ToUtc(now, testCase.Key));
      }

      // ASSERT
      Assert.IsNotNull(results);
      Assert.IsTrue(results.Count > 0);
      var index = 0;
      foreach (var testCase in testCases)
      {
        Assert.AreEqual(now, results[index].AddHours(testCase.Value));
        index += 1;
      }
    }
  }
}
