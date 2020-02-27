using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class PopravniUcenik
    {
        public int Id { get; set; }
        [ForeignKey(nameof(PopravniId))]
        public virtual PopravniIspit Popravni { get; set; }
        public int PopravniId { get; set; }
        [ForeignKey(nameof(OdjeljenjeStavkaId))]
        public virtual OdjeljenjeStavka OdjeljenjeStavka { get; set; }
        public int OdjeljenjeStavkaId { get; set; }
        public bool Pristupio { get; set; }
        public bool ImaPravoNaPopravni { get; set; }
        public int? Rezultat { get; set; }
    }
}
