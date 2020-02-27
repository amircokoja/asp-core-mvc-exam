using RS1_Ispit_asp.net_core.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class OdrzanaNastavaOdaberiVM
    {
        public int NastavnikId { get; set; }
        public List<Row> rows { get; set; }
        public class Row
        {
            public int MaturskiIspitId { get; set; }
            public string Skola { get; set; }
            public string Predmet { get; set; }
            public List<string> NisuPristupiliLista { get; set; }
            [DataType(DataType.Date)]
            public string NisuPristupili { get; set; }
            public DateTime Datum { get; set; }

        }
    }
}
