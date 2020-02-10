using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.Web.ViewModels
{
    public class AjaxTestPodsjetniciVM
    {
        public List<Dogadjaji> Podsjetnici { get; set; }

        public class Dogadjaji
        {
            public string OpisDogadjaja { get; set; }
            public DateTime DatumDogadjaja { get; set; }
            public List<ObavezeDogadjaja> ListaObaveza { get; set; }

            public class ObavezeDogadjaja
            {
                public int StanjeObavezeId { get; set; }
                public string NazivObaveze { get; set; }

            }
        }

      
    }
}
