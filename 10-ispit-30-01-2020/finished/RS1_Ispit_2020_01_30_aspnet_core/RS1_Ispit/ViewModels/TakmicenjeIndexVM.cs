using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class TakmicenjeIndexVM
    {
        [Required(ErrorMessage = "Izaberite skolu")]
        public int? SkolaId { get; set; }
        public List<SelectListItem> ListaSkola { get; set; }
        public int? Razred { get; set; }
    }
}
