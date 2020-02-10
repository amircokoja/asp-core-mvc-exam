using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class PopravniIspitIndexVM
    {

        public List<Row> rows { get; set; }
        public class Row
        {
            public int OdjeljenjeId { get; set; }
            public string SkolskaGodina { get; set; }
            public string Skola { get; set; }
            public string Odjeljenje { get; set; }
        }
    }
}
