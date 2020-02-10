using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class IspitOdaberiVM
    {
        public List<Row> rows { get; set; }
        public int AngazovanId { get; set; }
        public class Row
        {
            public int IspitId { get; set; }
            public int BrojStudenataKojiNisuPolozili { get; set; }
            public int BrojPrijavljenihStudenata { get; set; }
            public bool Zakljucano { get; set; }
            [DataType(DataType.Date)]
            public DateTime Datum { get; set; }
        }
    }
}
