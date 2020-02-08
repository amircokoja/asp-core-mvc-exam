using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.Web.ViewModels
{
    public class AjaxTestObavezeVM
    {
        public List<Row> rows { get; set; }
        public class Row
        {
            public int StanjeObavezeId { get; set; }
            public string Naziv { get; set; }
            public float Procenat { get; set; }
            public int BrojDana { get; set; }
            public bool Ponavljaj { get; set; }
        }
    
    }
}
