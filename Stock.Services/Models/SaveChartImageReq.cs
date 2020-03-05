using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Services.Models
{
  public class SaveChartImageReq
  {
    public string Symbol { get; set; }
    public DateTime PriceDate { get; set; }
    public int? YActual { get; set; }
    public string CandleImage { get; set; }
    public string VolumeImage { get; set; }
    public int Version { get; set; }

    public byte[] CandleByteArray => StringUtils.ToByteArray(CandleImage);
    public byte[] VolumeByteArray => StringUtils.ToByteArray(VolumeImage);
  }
}
