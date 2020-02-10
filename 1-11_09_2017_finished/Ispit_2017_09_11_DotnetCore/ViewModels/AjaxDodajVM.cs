using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_09_11_DotnetCore.ViewModels
{
    public class AjaxDodajVM
    {
        public int OdjeljenjeStavkeId { get; set; }
        public int OdjeljenjeId { get; set; }
        public int UcenikId { get; set; }
        public List<SelectListItem> ListaUcenika { get; set; }
        public int BrojUDnevniku { get; set; }
    }
}
