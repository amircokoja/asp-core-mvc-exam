using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class PopravniIspitUrediVM
    {
        public int IspitID { get; set; }
        public string Predmet { get; set; }
        public string Datum { get; set; }
        public string Skola { get; set; }
        public string SkolskaGodina { get; set; }
        public string Odjeljenje { get; set; }
    }
}
