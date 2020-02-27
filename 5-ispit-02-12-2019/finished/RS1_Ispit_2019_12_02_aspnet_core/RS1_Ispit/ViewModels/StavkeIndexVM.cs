using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class StavkeIndexVM
    {
        public List<Row> rows { get; set; }
        public class Row
        {
            public string Ucenik { get; set; }
            public int IspitStavkeId { get; set; }
            public string Odjeljenje { get; set; }
            public int BrojUdnevniku { get; set; }
            public bool Pristupio { get; set; }
            public bool PravoNaIspit { get; set; }
            public int? Rezultat { get; set; }
        }
    }
}
