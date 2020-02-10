using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class MaturskiIspitStavke
    {
        public int Id { get; set; }

        [ForeignKey(nameof(MaturskiIspitId))]
        public virtual MaturskiIspit MaturskiIspit { get; set; }
        public int MaturskiIspitId { get; set; }

        [ForeignKey(nameof(OdjeljenjeStavkaId))]
        public virtual OdjeljenjeStavka OdjeljenjeStavka { get; set; }
        public int OdjeljenjeStavkaId { get; set; }
        public bool Pristupio { get; set; }
        public int? Rezultat { get; set; }
    }
}
