using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_02_15.ViewModels
{
    public class LoginVM
    {
        [StringLength(100, ErrorMessage = "Korisnicko ime mora sadrzavati minimalno 3 karaktera", MinimumLength = 3)]
        public string KorisnickoIme { get; set; }
        [StringLength(100, ErrorMessage = "Sifra mora sadrzavati minimalno 4 karaktera", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }
        public bool ZapamtiPassword { get; set; }
    }
}
