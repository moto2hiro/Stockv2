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
    public string AvgType { get; set; }
    public int Period { get; set; }
    public int NoCorrect { get; set; }
    public int NoIncorrect { get; set; }
    public bool HasSum => (Sum > 0);
    public int Sum => NoCorrect + NoIncorrect; 
    public decimal? PctCorrect => (HasSum) ? (decimal?)NumberUtils.Round((NoCorrect / (decimal)Sum) * 100, 2) : null;
  }
}
