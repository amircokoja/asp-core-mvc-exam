using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_02_15.ViewModels
{
    public class AjaxTestIspisStudenataVM
    {
        public List<StudentiLista> listaStudenata { get; set; }
        public class StudentiLista
        {
            public string Student { get; set; }
            public int Bodovi { get; set; }
            public bool Prisutan { get; set; }
            public int OdrzaniCasDetaljiId { get; set; }

        }
    }
}
