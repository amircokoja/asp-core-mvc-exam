using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class StavkeIndexVM
    {
        public List<Row> rows { get; set; }
        public bool Zakljucan { get; set; }
        public bool TrenutniDatumVeci { get; set; }
        public class Row
        {
            public int IspitStavkeId { get; set; }
            public string Student { get; set; }
            public bool Pristupio { get; set; }
            public int? Ocjena { get; set; }
        }
    }
}
