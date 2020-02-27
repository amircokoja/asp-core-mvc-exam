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


        public IActionResult Index(int ispitId)
        {
            Ispit i = _context.Ispit.Find(ispitId);

            StavkeIndexVM model = new StavkeIndexVM
            {
                rows = _context.IspitStavke.Where(a => a.IspitId == ispitId).Select(a => new StavkeIndexVM.Row
                {
                    Ucenik = a.OdjeljenjeStavka.Ucenik.ImePrezime,
                    BrojUdnevniku = a.OdjeljenjeStavka.BrojUDnevniku,
                    IspitStavkeId = a.Id,
                    Odjeljenje = a.OdjeljenjeStavka.Odjeljenje.Oznaka,
                    PravoNaIspit = a.PravoNaPopravni,
                    Pristupio = a.Pristupio,
                    Rezultat = a.Rezultat
                }).ToList()
            };

            return PartialView(model);
        }

        public ActionResult Uredi(int ispitStavkaId)
        {
            IspitStavke i = _context.IspitStavke.Where(a => a.Id == ispitStavkaId).Include(a => a.OdjeljenjeStavka.Ucenik).Single();
            StavkeUrediVM model = new StavkeUrediVM
            {
                IspitStavkeId = ispitStavkaId,
                Ucenik = i.OdjeljenjeStavka.Ucenik.ImePrezime,
                Rezultat = i.Rezultat
            };
            return PartialView(model);
        }

        public ActionResult Snimi(StavkeUrediVM model)
        {
            IspitStavke i = _context.IspitStavke.Find(model.IspitStavkeId);
            i.Rezultat = model.Rezultat;
            _context.SaveChanges();

            return RedirectToAction("Index", new { ispitId = i.IspitId });
        }

        public ActionResult UcenikJePrisutan(int ispitStavkaId)
        {
            IspitStavke i = _context.IspitStavke.Find(ispitStavkaId);
            i.Pristupio = false;
            _context.SaveChanges();

            return RedirectToAction("Index", new { ispitId = i.IspitId });
        }

        public ActionResult UcenikJeOdsutan(int ispitStavkaId)
        {
            IspitStavke i = _context.IspitStavke.Find(ispitStavkaId);
            i.Pristupio = true;
            _context.SaveChanges();

            return RedirectToAction("Index", new { ispitId = i.IspitId });
        }

        public ActionResult SnimiInput(int ispitstavkeId, int rezultat)
        {
            IspitStavke i = _context.IspitStavke.Find(ispitstavkeId);
            i.Rezultat = rezultat;
            _context.SaveChanges();

            return RedirectToAction("Index", new { ispitId = i.IspitId });
        }

    }
}