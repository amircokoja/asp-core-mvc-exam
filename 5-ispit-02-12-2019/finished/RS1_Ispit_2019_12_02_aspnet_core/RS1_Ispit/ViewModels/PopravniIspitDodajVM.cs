using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class PopravniIspitDodajVM
    {
        public List<SelectListItem> ListaPredmeta { get; set; }
        public int OdjeljenjeId { get; set; }
        public string Skola { get; set; }
        public string SkolskaGodina { get; set; }
        public string Odjeljenje { get; set; }
        public int PredmetId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
    }
}
