using System;
using System.Collections.Generic;

namespace Stock.Services.Models.EF
{
    public partial class SymbolMaster
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
    }
}
