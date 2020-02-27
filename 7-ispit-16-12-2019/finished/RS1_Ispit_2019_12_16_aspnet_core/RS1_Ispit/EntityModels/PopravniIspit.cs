using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class PopravniIspit
    {
        public int Id { get; set; }

        public DateTime Datum { get; set; }
        [ForeignKey(nameof(PredmetID))]
        public virtual Predmet Predmet { get; set; }
        public int PredmetID { get; set; }

        [ForeignKey(nameof(ClanKomisije1Id))]
        public virtual Nastavnik ClanKomisije1 { get; set; }
        public int? ClanKomisije1Id { get; set; }

        [ForeignKey(nameof(ClanKomisije2Id))]
        public virtual Nastavnik ClanKomisije2 { get; set; }
        public int? ClanKomisije2Id { get; set; }
        [ForeignKey(nameof(ClanKomisije3Id))]
        public virtual Nastavnik ClanKomisije3 { get; set; }
        public int? ClanKomisije3Id { get; set; }

        [ForeignKey(nameof(SkolaID))]
        public virtual Skola Skola { get; set; }
        public int SkolaID { get; set; }

        [ForeignKey(nameof(SkolskaGodinaID))]
        public virtual SkolskaGodina SkolskaGodina { get; set; }
        public int SkolskaGodinaID { get; set; }
        public int? Razred { get; set; }
    }
}
