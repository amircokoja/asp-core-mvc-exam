using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class OdrzanaNastavaIndexVM
    {
        public List<Row> rows { get; set; }
        public class Row
        {
            public int NastavnikId { get; set; }
            public string Nastavnik { get; set; }
            public int SkolaId { get; set; }
            public string Skola { get; set; }
        }
    }
}
