using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_2017_06_21_v1.Models
{
    public class MaturskiIspit
    {
        public int Id { get; set; }
        public Odjeljenje Odjeljenje { get; set; }
        public int OdjeljenjeId { get; set; }
        public Nastavnik Nastavnik { get; set; }
        public int NastavnikId { get; set; }
        public DateTime Datum { get; set; }
        
    }
}
