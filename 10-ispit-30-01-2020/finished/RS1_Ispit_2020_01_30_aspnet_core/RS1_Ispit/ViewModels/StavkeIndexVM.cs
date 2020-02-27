using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class StavkeIndexVM
    {
        public int TakmicenjeId { get; set; }
        public List<Row> rows { get; set; }
        public bool Zakljucan { get; set; }
        public class Row
        {
            public int TakmicenjeUcesnikId { get; set; }
            public string Odjeljenje { get; set; }
            public int BrojUDnevniku { get; set; }
            public bool Pristupio { get; set; }
            public int? Rezultati { get; set; }
        }
    }
}
