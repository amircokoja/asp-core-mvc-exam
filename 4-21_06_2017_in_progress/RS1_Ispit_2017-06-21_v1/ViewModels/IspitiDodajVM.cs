using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_2017_06_21_v1.ViewModels
{
    public class IspitiDodajVM
    {
        public string Ispitivac { get; set; }
        public int IspitivacId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        [Required(ErrorMessage = "Izaberite odjeljenje")]
        public int? OdjeljenjeId { get; set; }
        public List<SelectListItem> ListaOdjeljenja { get; set; }
    }
}
