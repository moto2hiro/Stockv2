using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Services.Models
{
  public class BackTestReq
  {
    public string Title { get; set; }
    
    public int LookBack { get; set; }
    public int LookBackSegs { get; set; }
    
    public decimal SmoothingAlpha { get; set; }
    public int PolynomialOrder { get; set; }
    public decimal SupportResistanceProximityPct { get; set; }
  }
}
