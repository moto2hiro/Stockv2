using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Services.Models
{
  public class PastAvgResp
  {
    public List<PastAvg> PastAvgs { get; set; }
  }

  public class PastAvg
  {
    public int Period { get; set; }
    public decimal Avg { get; set; }
  }
}
