using System;
using System.Collections.Generic;

namespace Stock.Services.Models.EF
{
    public partial class WorldPriceIndex
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
        public bool IsIntraRequired { get; set; }
    }
}
