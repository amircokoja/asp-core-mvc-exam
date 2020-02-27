using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class Takmicenje
    {
        public int Id { get; set; }
        [ForeignKey(nameof(PredmetId))]
        public virtual Predmet Predmet { get; set; }
        public int PredmetId { get; set; }
        public DateTime Datum { get; set; }

        [ForeignKey(nameof(SkolaID))]
        public virtual Skola Skola { get; set; }
        public int SkolaID { get; set; }
        public bool Zakljucano { get; set; }
    }
}
