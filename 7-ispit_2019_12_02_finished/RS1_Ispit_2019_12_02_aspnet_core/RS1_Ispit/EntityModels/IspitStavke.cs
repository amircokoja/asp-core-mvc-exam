using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class IspitStavke
    {
        public int Id { get; set; }
        [ForeignKey(nameof(OdjeljenjeStavkaId))]
        public virtual OdjeljenjeStavka OdjeljenjeStavka { get; set; }
        public int OdjeljenjeStavkaId { get; set; }

        [ForeignKey(nameof(IspitId))]
        public virtual Ispit Ispit { get; set; }
        public int IspitId { get; set; }
        public int? Rezultat { get; set; }
        public bool Pristupio { get; set; }
        public bool PravoNaPopravni { get; set; }
    }
}
