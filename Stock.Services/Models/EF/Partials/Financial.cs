using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Services.Models.EF
{
  public partial class Financial
  {
    [NotMapped]
    public string Symbol { get; set; }

    [NotMapped]
    public string CompanyName { get; set; }
    
    [NotMapped]
    public string FiscalPeriod { get; set; }
  }
}
