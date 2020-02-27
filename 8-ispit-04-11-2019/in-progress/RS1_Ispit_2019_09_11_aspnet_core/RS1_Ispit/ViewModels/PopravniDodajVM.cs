using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class PopravniDodajVM
    {
        public int PopravniIspitId { get; set; }
        public int PredmetId { get; set; }
        public string Naziv { get; set; }
        public int Razred { get; set; }
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        public int SkolaId { get; set; }
        public List<SelectListItem> ListaSkola { get; set; }
        public int SkolskaGodinaId { get; set; }
        public List<SelectListItem> ListaSkolskihGodina { get; set; }
    }
}
