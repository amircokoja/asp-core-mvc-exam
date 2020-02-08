using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ispit_2017_02_15.EF;
using Ispit_2017_02_15.Models;
using Ispit_2017_02_15.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ispit_2017_02_15.Controllers
{
    public class LoginController : Controller
    {
        private MojContext _context;

        public LoginController(MojContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(new LoginVM
            {
                ZapamtiPassword = true
            });
        }

        public IActionResult Login(LoginVM input)
        {
            KorisnickiNalog korisnik = _context.KorisnickiNalog
                .SingleOrDefault(x => x.KorisnickoIme == input.KorisnickoIme && x.Lozinka == input.Lozinka);

            if (korisnik == null)
            {
                TempData["error_poruka"] = "pogrešan username ili password";
                return View("Index", input);
            }

            //            HttpContext.SetLogiraniKorisnik(korisnik);


            HttpContext.Session.SetInt32("key", korisnik.Id);


            return RedirectToAction("Index", "Odjeljenje");
        }

        public IActionResult Logout()
        {

            return RedirectToAction("Index");
        }
    }
}