using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class PopravniPrikazPopravnihVM
    {
        public int PredmetId { get; set; }
        public List<Row> rows { get; set; }
        public class Row
        {
            public int PopravniIspitId { get; set; }
            public string Skola { get; set; }
            public string SkolskaGodina { get; set; }
            public DateTime Datum { get; set; }
            public int BrojUcenikaNaPopravnom { get; set; }
            public int BrojUcenikaKojiSuPolozili { get; set; }
        }
    }
}
