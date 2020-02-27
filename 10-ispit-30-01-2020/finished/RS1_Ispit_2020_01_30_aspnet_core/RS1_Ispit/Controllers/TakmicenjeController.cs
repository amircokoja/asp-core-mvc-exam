using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class TakmicenjeController : Controller
    {
        private MojContext _context;

        public TakmicenjeController(MojContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            TakmicenjeIndexVM model = new TakmicenjeIndexVM
            {
                ListaSkola = _context.Skola.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Naziv
                }).ToList()
            };

            return View(model);
        }

        public ActionResult Odaberi(TakmicenjeIndexVM podaci)
        {

            TakmicenjeOdaberiVM model = new TakmicenjeOdaberiVM
            {
                Razred = podaci.Razred,
                SkolaId = (int)podaci.SkolaId,
                Skola = _context.Skola.Find(podaci.SkolaId).Naziv
            };


            if (podaci.Razred == null)
            {
                model.rows = _context.Takmicenje.Where(t => t.SkolaID == podaci.SkolaId).Select(t => new TakmicenjeOdaberiVM.Row
                {
                    TakmicenjeId = t.Id,
                    Razred = t.Predmet.Razred,
                    Predmet = t.Predmet.Naziv,
                    Datum = t.Datum,
                    BrojUcesnikaKojiNisuPristupili = _context.TakmicenjeUcesnik.Where(tu => tu.TakmicenjeId == t.Id && tu.Pristupio == false).Count(),
                    NajboljiUcesnik = "??"
                }).ToList();
            }
            else
            {
                model.rows = _context.Takmicenje.Where(t => t.Predmet.Razred == podaci.Razred && t.SkolaID == podaci.SkolaId).Select(t => new TakmicenjeOdaberiVM.Row
                {
                    TakmicenjeId = t.Id,
                    Razred = t.Predmet.Razred,
                    Predmet = t.Predmet.Naziv,
                    Datum = t.Datum,
                    BrojUcesnikaKojiNisuPristupili = _context.TakmicenjeUcesnik.Where(tu => tu.TakmicenjeId == t.Id && tu.Pristupio == false).Count(),
                    NajboljiUcesnikOdjeljenjeStavka = _context.TakmicenjeUcesnik.Where(tu => tu.TakmicenjeId == t.Id).OrderByDescending(tu => tu.Rezultat).First().OdjeljenjeStavkaId
                }).ToList();
            }


            for (int i = 0; i < model.rows.Count; i++)
            {
                if (model.rows[i].NajboljiUcesnikOdjeljenjeStavka != 0)
                {
                    var OS = _context.OdjeljenjeStavka.Where(a => a.Id == model.rows[i].NajboljiUcesnikOdjeljenjeStavka)
                        .Include(a => a.Odjeljenje.Skola)
                        .Include(a => a.Odjeljenje)
                        .Include(a => a.Ucenik)
                        .Single();

                    model.rows[i].NajboljiUcesnik = OS.Odjeljenje.Skola.Naziv + " | " + OS.Odjeljenje.Oznaka + " | " + OS.Ucenik.ImePrezime;
                }
            }


            return View(model);
        }

        public ActionResult Dodaj(int skolaId, int razred)
       {
            TakmicenjeDodajVM model = new TakmicenjeDodajVM
            {
                Razred = razred,
                ListaPredmeta = _context.Predmet.Select(p => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Naziv
                }).ToList(),
                SkolaId = skolaId,
                Skola = _context.Skola.Find(skolaId).Naziv
            };

            return View(model);
        }
        public ActionResult Snimi(TakmicenjeDodajVM model)
        {
            Takmicenje t = new Takmicenje
            {
                Datum = model.Datum,
                PredmetId = model.PredmetId,
                SkolaID = model.SkolaId,
                Zakljucano = false
            };

            _context.Takmicenje.Add(t);
            _context.SaveChanges();

            List<OdjeljenjeStavka> listaOS = _context.DodjeljenPredmet.Where(dp => dp.PredmetId == model.PredmetId && dp.ZakljucnoKrajGodine == 5).Select(dp => dp.OdjeljenjeStavka).ToList();

            foreach (var x in listaOS)
            {
                double prosjek = _context.DodjeljenPredmet.Where(dp => dp.OdjeljenjeStavkaId == x.Id).Average(dp => dp.ZakljucnoKrajGodine);

                if (prosjek >= 4)
                {
                    TakmicenjeUcesnik tu = new TakmicenjeUcesnik
                    {
                        OdjeljenjeStavkaId = x.Id,
                        Pristupio = false,
                        Rezultat = 0,
                        TakmicenjeId = t.Id
                    };

                    _context.TakmicenjeUcesnik.Add(tu);
                    _context.SaveChanges();
                }
            }



            TakmicenjeIndexVM newTI = new TakmicenjeIndexVM
            {
                SkolaId = model.SkolaId,
                Razred = model.Razred
            };
            return RedirectToAction("Index");
        }

        public ActionResult NovoTakmicenje(int skolaId, int razred)
        {
            return RedirectToAction("Dodaj", new { skolaId = skolaId, razred = razred });
        }

        public ActionResult Rezultati(int takmicenjeId)
        {
            Takmicenje t = _context.Takmicenje.Where(a => a.Id == takmicenjeId)
                .Include(a => a.Skola)
                .Include(a => a.Predmet)
                .SingleOrDefault();

            TakmicenjeRezultatiVM model = new TakmicenjeRezultatiVM
            {
                
                Datum = t.Datum,
                Predmet = t.Predmet.Naziv,
                Razred = t.Predmet.Razred,
                Skola = t.Skola.Naziv,
                TakmicenjeId = t.Id,
                Zakljucano = t.Zakljucano,
                SkolaId = t.SkolaID
            };

            return View(model);
        }


    }

}