using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.Web.ViewModels
{
    public class OznaceniDogadajiDetaljiVM
    {
        public int OznaceniDogadjajId { get; set; }
        public DateTime DatumDogadjaja { get; set; }
        public DateTime DatumDodavanja { get; set; }
        public string Opis { get; set; }
        public string Nastavnik { get; set; }
    }
}
