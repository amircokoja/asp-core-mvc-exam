using Microsoft.AspNetCore.Mvc.Rendering;
using RS1_Ispit_asp.net_core.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class OdrzanaNastavaUrediVM
    {
        public int MaturskiIspitId { get; set; }
        public DateTime Datum { get; set; }
        public string Predmet { get; set; }
        public string Napomena { get; set; }
    }
}
