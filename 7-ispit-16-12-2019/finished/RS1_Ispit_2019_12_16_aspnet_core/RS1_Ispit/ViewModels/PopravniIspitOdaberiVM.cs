using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class PopravniIspitOdaberiVM
    {
        public int Razred { get; set; }
        public string Skola { get; set; }
        public string SkolskaGodina { get; set; }
        public int SkolaId { get; set; }
        public int SkolskaGodinaId { get; set; }
        public List<Row> rows { get; set; }
        public class Row
        {
            public int PopravniIspitId { get; set; }
            public DateTime Datum { get; set; }
            public string Predmet { get; set; }
            public int BrojUcenikaNaPopravnom { get; set; }
            public int BrojUcenikaKojiSuPolozili { get; set; }
        }
    }
}
