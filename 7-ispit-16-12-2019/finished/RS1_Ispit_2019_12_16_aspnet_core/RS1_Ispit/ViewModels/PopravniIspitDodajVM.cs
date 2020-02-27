using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class PopravniIspitDodajVM
    {
        public int PopravniIspitId { get; set; }
        public string Predmet { get; set; }
        public int SkolaID { get; set; }
        public string Skola { get; set; }
        public int SkolskaGodinaID { get; set; }
        public string SkolskaGodina { get; set; }
        public int Razred { get; set; }
        public DateTime Datum { get; set; }
        public int ClanKomisije1Id { get; set; }
        public int ClanKomisije2Id { get; set; }
        public int ClanKomisije3Id { get; set; }
        public List<SelectListItem> ListaNastavnika { get; set; }
        public int PredmetId { get; set; }
        public List<SelectListItem> ListaPredmeta { get; set; }
        public string ClanKomisije1 { get; set; }
        public string ClanKomisije2 { get; set; }
        public string ClanKomisije3 { get; set; }
    }
}
