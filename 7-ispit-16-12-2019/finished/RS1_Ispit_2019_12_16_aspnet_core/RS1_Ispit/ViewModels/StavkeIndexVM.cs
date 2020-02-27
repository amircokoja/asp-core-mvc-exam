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
            public int PopravniUcesnikId { get; set; }
            public string Ucenik { get; set; }
            public string Odjeljenje { get; set; }
            public int BrojUDnevniku { get; set; }
            public bool Pristupio { get; set; }
            public bool ImaPravoNaPopravni { get; set; }
            public int? Rezultat { get; set; }
        }
    }
}
