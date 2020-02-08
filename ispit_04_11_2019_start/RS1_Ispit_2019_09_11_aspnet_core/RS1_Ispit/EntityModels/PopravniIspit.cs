using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class PopravniIspit
    {
        public int PopravniIspitId { get; set; }
        public DateTime Datum { get; set; }
   

        public int SkolaId { get; set; }
        public Skola Skola { get; set; }
        public int SkolskGodinaId { get; set; }
        public SkolskaGodina SkolskaGodina { get; set; }

        public int PredmetId { get; set; }
        public Predmet Predmet { get; set; }

    }
}
