using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.ViewModels;
using System.Collections.Generic;
using System.Linq;

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
            PopravniIspitIndexVM model = new PopravniIspitIndexVM
            {
                rows = _context.Odjeljenje.Select(o => new PopravniIspitIndexVM.Row
                {
                    OdjeljenjeId = o.Id,
                    Odjeljenje = o.Oznaka,
                    Skola = o.Skola.Naziv,
                    SkolskaGodina = o.SkolskaGodina.Naziv
                }).ToList()
            };

            return View(model);
        }

        public ActionResult Odaberi(int odjeljenjeId)
        {

            PopravniIspitOdaberiVM model = new PopravniIspitOdaberiVM
            {
                OdjeljenjeId = odjeljenjeId,
                rows = _context.Ispit.Where(i => i.OdjeljenjeId == odjeljenjeId).Select(i => new PopravniIspitOdaberiVM.Row
                {
                    IspitId = i.Id,
                    Datum = i.Datum,
                    Predmet = i.Predmet.Naziv,
                    BrojUcenikaNaPopravnom = _context.IspitStavke.Where(a => a.IspitId == i.Id).Count(),
                    BrojUcenikaKojiSuPolozili = _context.IspitStavke.Where(a => a.IspitId == i.Id && a.Rezultat > 50).Count()
                }).ToList()
            };

            return View(model);
        }

        public ActionResult Dodaj(int odjeljenjeId)
        {
            Odjeljenje o = _context.Odjeljenje.Where(a => a.Id == odjeljenjeId)
                .Include(a => a.Skola)
                .Include(a => a.SkolskaGodina)
                .Single();

            PopravniIspitDodajVM model = new PopravniIspitDodajVM
            {
                OdjeljenjeId = odjeljenjeId,
                Odjeljenje = o.Oznaka,
                Skola = o.Skola.Naziv,
                SkolskaGodina = o.SkolskaGodina.Naziv,
                ListaPredmeta = _context.PredajePredmet.Where(pp => pp.OdjeljenjeID == odjeljenjeId).Select(pp => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = pp.Predmet.Id.ToString(),
                    Text = pp.Predmet.Naziv
                }).Distinct().ToList()
            };

            return View(model);
        }

        public ActionResult Snimi(PopravniIspitDodajVM model)
        {
            Ispit i = new Ispit
            {
                Datum = model.Datum,
                OdjeljenjeId = model.OdjeljenjeId,
                PredmetId = model.PredmetId
            };
            _context.Ispit.Add(i);
            _context.SaveChanges();

            List<OdjeljenjeStavka> listaOS = _context.DodjeljenPredmet
                .Where(o => o.OdjeljenjeStavka.OdjeljenjeId == model.OdjeljenjeId && o.PredmetId == model.PredmetId
                && o.ZakljucnoKrajGodine == 1)
                .Select(o => o.OdjeljenjeStavka).ToList();

            foreach (var x in listaOS)
            {
                IspitStavke ispitS = new IspitStavke
                {
                    IspitId = i.Id,
                    OdjeljenjeStavkaId = x.Id,
                    Pristupio = false,
                    PravoNaPopravni = true
                };

                int brojNegativnih = _context.DodjeljenPredmet.Where(dp => dp.OdjeljenjeStavkaId == x.Id).Count(dp => dp.ZakljucnoKrajGodine == 1);

                if(brojNegativnih >= 3)
                {
                    ispitS.PravoNaPopravni = false;
                    ispitS.Rezultat = 0;
                }

                _context.IspitStavke.Add(ispitS);
                _context.SaveChanges();
            }


            return RedirectToAction("Odaberi", new { odjeljenjeId = model.OdjeljenjeId });
        }

        public ActionResult Uredi(int ispitId)
        {
            Ispit i = _context.Ispit.Where(a => a.Id == ispitId)
                .Include(a => a.Odjeljenje)
                .Include(a => a.Odjeljenje.SkolskaGodina)
                .Include(a => a.Odjeljenje.Skola)
                .Include(a => a.Predmet)
                .Single();

            PopravniIspitUrediVM model = new PopravniIspitUrediVM
            {
                Datum = i.Datum.ToString("dd.MM.yyyy"),
                IspitID = ispitId,
                Odjeljenje = i.Odjeljenje.Oznaka,
                Predmet = i.Predmet.Naziv,
                Skola = i.Odjeljenje.Skola.Naziv,
                SkolskaGodina = i.Odjeljenje.SkolskaGodina.Naziv
            };

            return View(model);
        }
    }
}