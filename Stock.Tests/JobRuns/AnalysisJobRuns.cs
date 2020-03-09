using NUnit.Framework;
using Stock.Services;
using Stock.Services.Services;
using Stock.Services.Utils;
using System;
using System.IO;

namespace Stock.Tests.JobRuns
{
  [TestFixture()]
  public class AnalysisJobRuns : BaseTest
  {
    [Test()]
    public void Run_Transform()
    {
      new AnalysisService().Transform();
    }
  }
}
