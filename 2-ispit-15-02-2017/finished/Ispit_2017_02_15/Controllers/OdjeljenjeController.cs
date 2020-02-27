using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ispit_2017_02_15.EF;
using Microsoft.AspNetCore.Mvc;
using Ispit_2017_02_15.ViewModels;
using Microsoft.AspNetCore.Http;
using Ispit_2017_02_15.Models;
using Microsoft.EntityFrameworkCore;

namespace Ispit_2017_02_15.Controllers
{
    public class OdjeljenjeController : Controller
    {
        private MojContext _context;

        public OdjeljenjeController(MojContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            int KorisnickiNalogId = (int)HttpContext.Session.GetInt32("key");
            Nastavnik nastavnik2 = _context.Nastavnik.Where(n => n.KorisnickiNalogId == KorisnickiNalogId).Single();

            var model = new OdjeljenjeIndexVM
            {
                nastavnik = nastavnik2,
                rows = _context.OdrzaniCasovi
                .Where(o => o.Angazovan.NastavnikId == nastavnik2.Id)
                .Select(o => new OdjeljenjeIndexVM.Row
                {
                    AkademskaGodina = o.Angazovan.AkademskaGodina.Opis,
                    Datum = o.Datum,
                    OdrzaniCasoviId = o.Id,
                    Predmet = o.Angazovan.Predmet.Naziv,
                    BrojPrisutnih = _context.OdrzaniCasDetalji.Where(ocd => ocd.OdrzaniCasId == o.Id).Count(ocd => ocd.Prisutan == true),
                    UkupnoStudenata = _context.OdrzaniCasDetalji.Where(ocd => ocd.OdrzaniCasId == o.Id).Count(),
                    ProsjecnaOcjena = Math.Round(_context.SlusaPredmet.Where(sp => sp.AngazovanId == o.AngazovanId).Average(sp => sp.Ocjena) ?? 0, 2)
                }).ToList()
            };

            return View(model);
        }

        public ActionResult Dodaj(int nastavnikId)
        {
            Nastavnik n = _context.Nastavnik.Find(nastavnikId);
            var model = new OdjeljenjeDodajVM
            {
                NastavnikId = nastavnikId,
                Nastavnik = n.Ime + " " + n.Prezime,
                ListaPredmeta = _context.Angazovan
                .Where(a => a.NastavnikId == nastavnikId)
                .Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.AkademskaGodina.Opis + " " + a.Predmet.Naziv
                }).ToList(),
                AkademskaGodinaPredmet = "",
                OdrzaniCasId = 0
            };

            return View(model);
        }

        public ActionResult Snimi(OdjeljenjeDodajVM model)
        {
            OdrzaniCas odrzaniCas;
            
            if(model.OdrzaniCasId == 0)
            {
                odrzaniCas = new OdrzaniCas
                {
                    Datum = model.Datum,
                    AngazovanId = model.AngazovanId
                };
                _context.OdrzaniCasovi.Add(odrzaniCas);
                _context.SaveChanges();
                
                List<SlusaPredmet> listaSlusaPredmet = _context.SlusaPredmet.Where(s => s.AngazovanId == model.AngazovanId).ToList();

                foreach (var x in listaSlusaPredmet)
                {
                    OdrzaniCasDetalji newOCD = new OdrzaniCasDetalji
                    {
                        BodoviNaCasu = 0,
                        OdrzaniCasId = odrzaniCas.Id,
                        Prisutan = false,
                        SlusaPredmetId = x.Id
                    };
                    _context.OdrzaniCasDetalji.Add(newOCD);
                    _context.SaveChanges();
                }
            }
            else
            {
                odrzaniCas = _context.OdrzaniCasovi.Find(model.OdrzaniCasId);
                odrzaniCas.Datum = model.Datum;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Uredi(int odrzaniCasoviId)
        {
            var OdrzaniCasovi = _context.OdrzaniCasovi.Where(o => o.Id == odrzaniCasoviId)
                .Include(o => o.Angazovan)
                .Include(o => o.Angazovan.Nastavnik)
                .Include(o => o.Angazovan.AkademskaGodina)
                .Include(o => o.Angazovan.Predmet)
                .Single();

            var model = new OdjeljenjeDodajVM
            {
                AngazovanId = OdrzaniCasovi.AngazovanId,
                Datum = OdrzaniCasovi.Datum,
                NastavnikId = OdrzaniCasovi.Angazovan.NastavnikId,
                Nastavnik = OdrzaniCasovi.Angazovan.Nastavnik.Ime + " " + OdrzaniCasovi.Angazovan.Nastavnik.Prezime,
                AkademskaGodinaPredmet = OdrzaniCasovi.Angazovan.AkademskaGodina.Opis + " " + OdrzaniCasovi.Angazovan.Predmet.Naziv,
                OdrzaniCasId = odrzaniCasoviId
            };

            return View("Dodaj", model);
        }
    }
}