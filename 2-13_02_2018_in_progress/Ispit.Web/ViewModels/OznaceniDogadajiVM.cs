using Ispit.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.Web.ViewModels
{
    public class OznaceniDogadajiVM
    {
        public int korisnikId { get; set; }
        public List<Neoznaceni> NeoznaceniDogadjaji { get; set; }
        public List<Oznaceni> OznaceniDogadjaji { get; set; }
        public class Neoznaceni
        {
            public int DogadjajId { get; set; }
            public DateTime DatumDogadjaja { get; set; }
            public string Nastavnik { get; set; }
            public string OpisDogadjaja { get; set; }
            public int BrojObaveza { get; set; }
        }   
        public class Oznaceni
        {
            public int DogadjajId { get; set; }
            public DateTime DatumDogadjaja { get; set; }
            public string Nastavnik { get; set; }
            public string OpisDogadjaja { get; set; }
            public float RealizovanoObaveza { get; set; }
        }
    }
}
