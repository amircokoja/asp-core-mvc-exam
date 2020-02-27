using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class StavkeController : Controller
    {
        private MojContext _context;

        public StavkeController(MojContext context)
        {
            _context = context;
        }

        public IActionResult Index(int takmicenjeId)
        {
            StavkeIndexVM model = new StavkeIndexVM
            {
                Zakljucan = _context.Takmicenje.Find(takmicenjeId).Zakljucano,
                TakmicenjeId = takmicenjeId,
                rows = _context.TakmicenjeUcesnik.Where(t => t.TakmicenjeId == takmicenjeId).Select(t => new StavkeIndexVM.Row
                {
                    BrojUDnevniku = t.OdjeljenjeStavka.BrojUDnevniku,
                    Odjeljenje = t.OdjeljenjeStavka.Odjeljenje.Oznaka,
                    Pristupio = t.Pristupio,
                    Rezultati = t.Rezultat,
                    TakmicenjeUcesnikId = t.Id
                }).ToList()
            };

            return PartialView(model);
        }

        public ActionResult UcesnikNijePristupio(int takmicenjeUcesnikId)
        {
            var t = _context.TakmicenjeUcesnik.Find(takmicenjeUcesnikId);
            t.Pristupio = false;
            _context.SaveChanges();

            return RedirectToAction("Index", new { takmicenjeId = t.TakmicenjeId });
        }

        public ActionResult UcesnikJePristupio(int takmicenjeUcesnikId)
        {
            var t = _context.TakmicenjeUcesnik.Find(takmicenjeUcesnikId);
            t.Pristupio = true;
            _context.SaveChanges();

            return RedirectToAction("Index", new { takmicenjeId = t.TakmicenjeId });
        }

        public ActionResult DodajUcesnika(int takmicenjeId)
        {
            StavkeDodajUcesnikaVM model = new StavkeDodajUcesnikaVM
            {
                ListaUcesnika = _context.Ucenik.Select(dp => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = dp.Id.ToString(),
                    Text = dp.ImePrezime
                }).ToList(),
                TakmicenjeId = takmicenjeId
            };

            return PartialView(model);
        }


        public ActionResult UrediUcesnika(int takmicenjeUcesnikId)
        {
            TakmicenjeUcesnik t = _context.TakmicenjeUcesnik.Find(takmicenjeUcesnikId);

            StavkeDodajUcesnikaVM model = new StavkeDodajUcesnikaVM
            {
                ListaUcesnika = _context.Ucenik.Select(dp => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = dp.Id.ToString(),
                    Text = dp.ImePrezime
                }).ToList(),
                TakmicenjeId = t.TakmicenjeId,
                Rezultat = t.Rezultat,
                OdjeljenjeStavkaId = t.OdjeljenjeStavkaId,
                TakmicenjeUcesnikId = takmicenjeUcesnikId
            };

            return PartialView("DodajUcesnika", model);
        }

        public ActionResult SnimiUcesnika(StavkeDodajUcesnikaVM model)
        {
            TakmicenjeUcesnik t;
            if(model.TakmicenjeUcesnikId == 0)
            {
                t = new TakmicenjeUcesnik();
                _context.TakmicenjeUcesnik.Add(t);
                t.OdjeljenjeStavkaId = model.OdjeljenjeStavkaId;
                t.Pristupio = false;
                t.TakmicenjeId = model.TakmicenjeId;
                //dodaj
            } else
            {
                t = _context.TakmicenjeUcesnik.Find(model.TakmicenjeUcesnikId);
                //uredi
            }

            t.Rezultat = model.Rezultat;
            _context.SaveChanges();


            return RedirectToAction("Index", "Stavke", new { takmicenjeId = model.TakmicenjeId });
        }

            public ActionResult SnimiFocusOut(int takmicenjeUcesnikId, int rezultat)
        {
            TakmicenjeUcesnik t = _context.TakmicenjeUcesnik.Find(takmicenjeUcesnikId);
            t.Rezultat = rezultat;
            _context.SaveChanges();

            return RedirectToAction("Index", "Stavke", new { takmicenjeId = t.TakmicenjeId });
        }

    }

}