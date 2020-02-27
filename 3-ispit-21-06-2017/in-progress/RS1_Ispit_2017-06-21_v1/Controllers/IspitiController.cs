using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_Ispit_2017_06_21_v1.EF;
using RS1_Ispit_2017_06_21_v1.Models;
using RS1_Ispit_2017_06_21_v1.ViewModels;


namespace RS1_Ispit_2017_06_21_v1.Controllers
{
    public class IspitiController : Controller
    {
        private MojContext db;

        public IspitiController(MojContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var model = new IspitiIndexVM
            {
                rows = db.MaturskiIspit.Select(s => new IspitiIndexVM.Row
                {
                    Datum = s.Datum,
                    Ispitivac = s.Nastavnik.ImePrezime,
                    ProsjecniBodovi = db.MaturskiIspitStavka.Where(mis => mis.MaturskiIspitId == s.Id).Average(mis => mis.Bodovi) ?? 0,
                    MaturskiIspitId = s.Id,
                    Odjeljenje = s.Odjeljenje.Naziv
                }).ToList()
            };
            return View(model);
        }

        public ActionResult Dodaj()
        {
            Nastavnik n = db.Nastavnik.Find(1);
            var model = new IspitiDodajVM
            {
                ListaOdjeljenja = db.Odjeljenje.Select(o => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = o.Id.ToString(),
                    Text = o.Naziv
                }).ToList(),
                Ispitivac = n.ImePrezime,
                IspitivacId = n.Id
            };
            return View(model);
        }

        public ActionResult Snimi(IspitiDodajVM model)
        {
            MaturskiIspit newMI = new MaturskiIspit
            {
                Datum = model.Datum,
                NastavnikId = model.IspitivacId,
                OdjeljenjeId = (int)model.OdjeljenjeId
            };

            db.MaturskiIspit.Add(newMI);
            db.SaveChanges();

            List<UpisUOdjeljenje> listaUpisaUOdjeljenje = db.UpisUOdjeljenje.Where(u => u.OdjeljenjeId == newMI.OdjeljenjeId && u.OpciUspjeh > 1 ).ToList();

            foreach (var x in listaUpisaUOdjeljenje)
            {
                MaturskiIspitStavka mis = new MaturskiIspitStavka
                {
                    Bodovi = null,
                    MaturskiIspitId = newMI.Id,
                    UpisUOdjeljenjeId = x.Id
                };
                if(x.OpciUspjeh == 5)
                {
                    mis.Oslobodjen = true;
                }
                db.MaturskiIspitStavka.Add(mis);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Detalji(int maturskiIspitId)
        {
            MaturskiIspit m = db.MaturskiIspit.Where(mi => mi.Id == maturskiIspitId)
                .Include(mi => mi.Nastavnik)
                .Include(mi => mi.Odjeljenje)
                .Single();

            var model = new IspitiDetaljiVM
            {
                Datum = m.Datum,
                Ispitivac = m.Nastavnik.ImePrezime,
                Odjeljenje = m.Odjeljenje.Naziv,
                MaturskiIspitId = m.Id
            };

            return View(model);
        }

    }
}
