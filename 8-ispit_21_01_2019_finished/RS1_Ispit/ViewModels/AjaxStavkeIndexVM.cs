using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class AjaxStavkeIndexVM
    {
        public List<Row> rows { get; set; }
        public class Row
        {
            public int MaturskiIspitStavkeId { get; set; }
            public string Ucenik { get; set; }
            public bool Pristupio { get; set; }
            public int? Rezultat { get; set; }
            public double ProsjekOcjena { get; set; }
        }
    }
}
