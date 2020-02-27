using Microsoft.AspNetCore.Mvc;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.ViewModels;
using RS1_Ispit_asp.net_core.EntityModels;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class AjaxStavkeController : Controller
    {
        private MojContext _context;

        public AjaxStavkeController(MojContext context)
        {
            _context = context;
        }


        public IActionResult Index(int maturskiIspitId)
        {
            AjaxStavkeIndexVM model = new AjaxStavkeIndexVM
            {
                rows = _context.MaturskiIspitStavke.Where(mis => mis.MaturskiIspitId == maturskiIspitId).Select(mis => new AjaxStavkeIndexVM.Row
                {
                    MaturskiIspitStavkeId = mis.Id,
                    Pristupio = mis.Pristupio,
                    Rezultat = mis.Rezultat,
                    Ucenik = mis.OdjeljenjeStavka.Ucenik.ImePrezime,
                    ProsjekOcjena = _context.DodjeljenPredmet.Where(dp => dp.OdjeljenjeStavkaId == mis.OdjeljenjeStavkaId).Average(dp => dp.ZakljucnoKrajGodine)
                }).ToList()
            };

            return PartialView(model);
        }
        public IActionResult Uredi(int maturskiIspitStavkaId)
        {
            MaturskiIspitStavke m = _context.MaturskiIspitStavke.Where(mi => mi.Id == maturskiIspitStavkaId)
                .Include(mi => mi.OdjeljenjeStavka.Ucenik).Single();

            AjaxStavkeUrediVM model = new AjaxStavkeUrediVM
            {
                Bodovi = m.Rezultat,
                MaturskiIspitStavkeId = m.Id,
                Ucenik = m.OdjeljenjeStavka.Ucenik.ImePrezime
            };

            return PartialView(model);
        }

        public ActionResult Snimi(AjaxStavkeUrediVM model)
        {
            MaturskiIspitStavke m = _context.MaturskiIspitStavke.Find(model.MaturskiIspitStavkeId);
            m.Rezultat = model.Bodovi;
            _context.SaveChanges();

            return RedirectToAction("Index", new { maturskiIspitId = m.MaturskiIspitId });
        }

        public ActionResult UcenikJeOdsutan(int maturskiIspitStavkeId)
        {
            MaturskiIspitStavke m = _context.MaturskiIspitStavke.Find(maturskiIspitStavkeId);
            m.Pristupio = false;
            _context.SaveChanges();

            return RedirectToAction("Index", new { maturskiIspitId = m.MaturskiIspitId });
        }

        public ActionResult UcenikJePrisutan(int maturskiIspitStavkeId)
        {
            MaturskiIspitStavke m = _context.MaturskiIspitStavke.Find(maturskiIspitStavkeId);
            m.Pristupio = true;
            _context.SaveChanges();

            return RedirectToAction("Index", new { maturskiIspitId = m.MaturskiIspitId });
        }

        public ActionResult AjaxSnimi(int maturskiIspitStavkeId, int bodovi)
        {
            MaturskiIspitStavke m = _context.MaturskiIspitStavke.Find(maturskiIspitStavkeId);
            m.Rezultat = bodovi;
            _context.SaveChanges();

            return RedirectToAction("Index", new { maturskiIspitId = m.MaturskiIspitId });
        }
    }

}