using Ispit_2017_02_15.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_02_15.ViewModels
{
    public class OdjeljenjeIndexVM
    {
        public Nastavnik nastavnik { get; set; }
        public List<Row> rows { get; set; }
        public class Row
        {
            public int OdrzaniCasoviId { get; set; }
            public DateTime Datum { get; set; }
            public string AkademskaGodina { get; set; }
            public string Predmet { get; set; }
            public int BrojPrisutnih { get; set; }
            public int UkupnoStudenata { get; set; }
            public double ProsjecnaOcjena { get; set; }
        }
        
    }
}
