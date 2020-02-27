using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_09_11_DotnetCore.ViewModels
{
    public class AjaxIndexVM
    {
        public int OdjeljenjeId { get; set; }
        public List<Row> rows { get; set; }
        public class Row
        {
            public int OdjeljenjeStavkaId { get; set; }
            public int BrojUDneviku { get; set; }
            public string Ucenik { get; set; }
            public int BrojZakljucenihOcjena { get; set; }
        }

    }
}
