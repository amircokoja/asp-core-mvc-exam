using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_09_11_DotnetCore.ViewModels
{
    public class OdjeljenjeDodajVM
    {
        public string SkolskaGodina { get; set; }
        public int Razred { get; set; }
        public string Oznaka { get; set; }
        public int RazrednikId { get; set; }
        public List<SelectListItem> ListaRazrednika { get; set; }
        public List<SelectListItem> ListaNizihOdjeljenja { get; set; }
        public int NizeOdjeljenjeId { get; set; }
    }
}
