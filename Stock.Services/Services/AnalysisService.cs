using Stock.Services.Models;
using Stock.Services.Models.EF;
using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Stock.Services.Services
{
  public interface IAnalysisService
  {
    void BackTest();
  }

  public class AnalysisService : BaseService, IAnalysisService
  {
    public void BackTest()
    {



    }
  }
}
