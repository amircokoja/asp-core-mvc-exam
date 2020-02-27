using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class StavkeDodajUcesnikaVM
    {
        public int TakmicenjeId { get; set; }
        public int OdjeljenjeStavkaId { get; set; }
        public List<SelectListItem> ListaUcesnika { get; set; }
        public int? Rezultat { get; set; }
        public int TakmicenjeUcesnikId { get; set; }
    }
}
