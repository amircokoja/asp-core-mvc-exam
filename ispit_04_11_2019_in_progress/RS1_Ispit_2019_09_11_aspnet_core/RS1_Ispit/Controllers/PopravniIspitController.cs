using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.ViewModels;


namespace RS1_Ispit_asp.net_core.Controllers
{
    public class PopravniIspitController : Controller
    {
        private MojContext _context;

        public PopravniIspitController(MojContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            List<Predmet> listaPredmeta = _context.Predmet.ToList();

            return View(listaPredmeta);
        }

        public ActionResult PrikazPopravnih(int predmetId)
        {
            PopravniPrikazPopravnihVM model = new PopravniPrikazPopravnihVM
            {
                PredmetId = predmetId,
                rows = _context.PopravniIspit.Where(p => p.PredmetId == predmetId).Select(p => new PopravniPrikazPopravnihVM.Row
                {
                    Datum = p.Datum,
                    PopravniIspitId = p.Id,
                    SkolskaGodina = p.SkolskaGodina.Naziv,
                    BrojUcenikaNaPopravnom = _context.PopravniIspitUcenik.Count(pi => pi.PopravniIspitId == p.Id),
                    BrojUcenikaKojiSuPolozili = _context.PopravniIspitUcenik.Where(pi => pi.PopravniIspitId == p.Id && pi.Rezultat > 50).Count(),
                    Skola = p.Skola.Naziv
                }).ToList()
            };

            return View(model);
        }

        public ActionResult Dodaj(int predmetId)
        {
            Predmet p = _context.Predmet.Find(predmetId);

            PopravniDodajVM model = new PopravniDodajVM
            {
                ListaSkola = _context.Skola.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Naziv
                }).ToList(),
                ListaSkolskihGodina = _context.SkolskaGodina.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Naziv
                }).ToList(),
                PredmetId = predmetId,
                Naziv = p.Naziv,
                Razred = p.Razred
            };

            return View(model);
        }

        public ActionResult Snimi(PopravniDodajVM model)
        {
            PopravniIspit noviPI = new PopravniIspit
                {
                    Datum = model.Datum,
                    PredmetId = model.PredmetId,
                    SkolaId = model.SkolaId,
                    SkolskGodinaId = model.SkolskaGodinaId
                };

            _context.PopravniIspit.Add(noviPI);
            _context.SaveChanges();


            List<OdjeljenjeStavka> listaOS = _context.DodjeljenPredmet.Where(dp => dp.PredmetId == model.PredmetId && dp.ZakljucnoKrajGodine == 1).Select(dp => dp.OdjeljenjeStavka).ToList();

            foreach (var x in listaOS)
            {
                PopravniIspitUcenik noviPIU = new PopravniIspitUcenik
                {
                    OdjeljenjeStavkaId = x.Id,
                    PopravniIspitId = noviPI.Id,
                    Pristupio = false,
                    Rezultat = null,
                    imaPravoPristupiti = true
                };

                int brojNegativnih = _context.DodjeljenPredmet.Where(dp => dp.OdjeljenjeStavkaId == x.Id && dp.ZakljucnoKrajGodine == 1).Count();

                if(brojNegativnih > 2)
                {
                    noviPIU.imaPravoPristupiti = false;
                    noviPIU.Rezultat = 0;
                }
                _context.PopravniIspitUcenik.Add(noviPIU);
                _context.SaveChanges();
            }


            return RedirectToAction("PrikazPopravnih", new { predmetId = model.PredmetId });
        }

        public ActionResult Uredi(int popravniIspitId)
        {
            PopravniIspit p = _context.PopravniIspit.Where(pi => pi.Id == popravniIspitId)
                .Include(pi => pi.Skola)
                .Include(pi => pi.Predmet)
                .Single();

            SkolskaGodina sg = _context.SkolskaGodina.Find(p.SkolskGodinaId);

            PopravniUrediVM model = new PopravniUrediVM
            {
                PopravniIspitId = p.Id,
                Datum = p.Datum,
                Naziv = p.Predmet.Naziv,
                Razred = p.Predmet.Razred,
                Skola = p.Skola.Naziv,
                SkolskaGodina = sg.Naziv
            };

            return View(model);
        }
    }
}