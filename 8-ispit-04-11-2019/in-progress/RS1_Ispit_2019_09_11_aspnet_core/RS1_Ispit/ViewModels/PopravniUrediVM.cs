using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class PopravniUrediVM
    {
        public int PopravniIspitId { get; set; }
        public string Naziv { get; set; }
        public int Razred { get; set; }
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        public string Skola { get; set; }
        public string SkolskaGodina { get; set; }
    }
}
