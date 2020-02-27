﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class PopravniIspitIndexVM
    {
        public int SkolaId { get; set; }
        public int SkolskaGodinaId { get; set; }
        public int Razred { get; set; }
        public List<SelectListItem> ListaSkola { get; set; }
        public List<SelectListItem> ListaSkolskihGodina { get; set; }
    }
}
