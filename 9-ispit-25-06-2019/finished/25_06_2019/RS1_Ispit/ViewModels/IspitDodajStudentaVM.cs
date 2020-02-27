using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class IspitDodajStudentaVM
    {
        public int IspitId { get; set; }
        public int SlusaPredmetId { get; set; }
        public List<SelectListItem> ListaStudenata { get; set; }
    }
}
