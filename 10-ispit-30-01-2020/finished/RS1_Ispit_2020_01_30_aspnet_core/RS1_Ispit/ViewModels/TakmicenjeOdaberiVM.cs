using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class TakmicenjeOdaberiVM
    {
        public string Skola { get; set; }
        public int? Razred { get; set; }
        public int SkolaId { get; set; }
        public List<Row> rows { get; set; }
        public class Row
        {
            public int TakmicenjeId { get; set; }
            public string Predmet { get; set; }
            public int Razred { get; set; }
            public DateTime Datum { get; set; }
            public int BrojUcesnikaKojiNisuPristupili { get; set; }
            public int NajboljiUcesnikOdjeljenjeStavka { get; set; }
            public string NajboljiUcesnik { get; set; }
        }
    }
}
