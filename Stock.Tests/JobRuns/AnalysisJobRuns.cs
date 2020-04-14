using NUnit.Framework;
using Stock.Services;
using Stock.Services.Models;
using Stock.Services.Services;
using Stock.Services.Utils;
using System;
using System.IO;

namespace Stock.Tests.JobRuns
{
  /// <summary>
  /// To manually run jobs
  /// </summary>
  [TestFixture()]
  public class AnalysisJobRuns : BaseTest
  {
    [Test()]
    public void Run_BackTest()
    {
      new AnalysisService().GetPricesForCharts(new BackTestReq()
      {
        LookBack = 100,
        LookBackSegs = 5,
        SmoothingAlpha = 0.6m,
      });
    }

  }
}
