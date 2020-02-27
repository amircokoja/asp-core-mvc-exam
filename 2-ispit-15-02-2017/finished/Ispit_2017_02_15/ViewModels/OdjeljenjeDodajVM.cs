using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_02_15.ViewModels
{
    public class OdjeljenjeDodajVM
    {
        public int OdrzaniCasId { get; set; }
        public int NastavnikId { get; set; }
        public string Nastavnik { get; set; }
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        public List<SelectListItem> ListaPredmeta { get; set; }
        public int AngazovanId { get; set; }
        public string AkademskaGodinaPredmet { get; set; }
    }
}
