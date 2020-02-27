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


        public IActionResult Index(int popravniIspitId)
        {
            StavkeIndexVM model = new StavkeIndexVM
            {
                rows = _context.PopravniUcenik.Where(a => a.PopravniId == popravniIspitId).Select(a => new StavkeIndexVM.Row
                {
                    BrojUDnevniku = a.OdjeljenjeStavka.BrojUDnevniku,
                    ImaPravoNaPopravni = a.ImaPravoNaPopravni,
                    Odjeljenje = a.OdjeljenjeStavka.Odjeljenje.Oznaka,
                    PopravniUcesnikId = a.Id,
                    Pristupio = a.Pristupio,
                    Rezultat = a.Rezultat,
                    Ucenik = a.OdjeljenjeStavka.Ucenik.ImePrezime
                }).ToList()
            };

            return PartialView(model);
        }

        public ActionResult UcenikJeOdsutan(int popravniUcesnikId)
        {
            PopravniUcenik p = _context.PopravniUcenik.Find(popravniUcesnikId);
            p.Pristupio = false;
            _context.SaveChanges();

            return RedirectToAction("Index", new { popravniIspitId = p.PopravniId });
        }

        public ActionResult UcenikJePrisutan(int popravniUcesnikId)
        {
            PopravniUcenik p = _context.PopravniUcenik.Find(popravniUcesnikId);
            p.Pristupio = true;
            _context.SaveChanges();

            return RedirectToAction("Index", new { popravniIspitId = p.PopravniId });
        }

        public ActionResult Uredi(int popravniUcesnikId)
        {
            PopravniUcenik p = _context.PopravniUcenik.Where(a => a.Id == popravniUcesnikId).Include(a => a.OdjeljenjeStavka.Ucenik).Single();

            StavkeUrediVM model = new StavkeUrediVM
            {
                PopravniUcesnikId = popravniUcesnikId,
                Rezultat = p.Rezultat,
                Ucenik = p.OdjeljenjeStavka.Ucenik.ImePrezime
            };
            return PartialView("Uredi", model);
        }



        public ActionResult Snimi(StavkeUrediVM model)
        {
            PopravniUcenik p = _context.PopravniUcenik.Find(model.PopravniUcesnikId);

            PopravniUcenik novi;

            if (model.PopravniIspitId == 0 )
            {
                novi = _context.PopravniUcenik.Find(model.PopravniUcesnikId);
                novi.Rezultat = model.Rezultat;
                _context.SaveChanges();
                //uredi
            }
            else
            {
                novi = new PopravniUcenik();
                novi.ImaPravoNaPopravni = true;
                novi.OdjeljenjeStavkaId = model.OdjeljenjeStavkaId;
                novi.PopravniId = model.PopravniIspitId;
                novi.Pristupio = false;
                _context.PopravniUcenik.Add(novi);
                //dodaj
                _context.SaveChanges();

                p = _context.PopravniUcenik.Find(novi.Id);

            }

            return RedirectToAction("Index", new { popravniIspitId = p.PopravniId });
        }

        public ActionResult DodajUcenikaStavke(StavkeUrediVM model)
        {
            return PartialView("Dodaj", model);
        }

        public ActionResult SnimiFocusOut(int popravniUcenikId, int bodovi)
        {
            PopravniUcenik p = _context.PopravniUcenik.Find(popravniUcenikId);
            p.Rezultat = bodovi;

            _context.SaveChanges();
            return RedirectToAction("Index", new { popravniIspitId = p.PopravniId });
        }

    }
}