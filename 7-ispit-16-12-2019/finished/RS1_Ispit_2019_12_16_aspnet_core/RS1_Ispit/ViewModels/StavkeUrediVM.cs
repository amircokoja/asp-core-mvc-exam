using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class StavkeUrediVM
    {
        public int PopravniIspitId { get; set; }
        public int PopravniUcesnikId { get; set; }
        public string Ucenik { get; set; }
        public int? Rezultat { get; set; }
        public List<SelectListItem> ListaUcenika { get; set; }
        public int OdjeljenjeStavkaId { get; set; }
    }
}
