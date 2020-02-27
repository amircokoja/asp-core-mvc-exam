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
    public class AjaxStavkeController : Controller
    {
        private MojContext _context;

        public AjaxStavkeController(MojContext context)
        {
            _context = context;
        }


        public IActionResult Index(int popravniIspitId)
        {
            var model = new AjaxStavkeIndexVM
            {
                PoravniIspitId = popravniIspitId,
                rows = _context.PopravniIspitUcenik.Where(p => p.PopravniIspitId == popravniIspitId).Select(p => new AjaxStavkeIndexVM.Row
                {
                    BrojUDnevniku = p.OdjeljenjeStavka.BrojUDnevniku,
                    imaPravoNaPopravi = p.imaPravoPristupiti,
                    Odjeljenje = p.OdjeljenjeStavka.Odjeljenje.Oznaka,
                    PopravniIspitUcenikId = p.Id,
                    Pristupio = p.Pristupio,
                    Rezultat = p.Rezultat,
                    Ucenik = p.OdjeljenjeStavka.Ucenik.ImePrezime
                }).ToList()
            };

            return PartialView(model);
        }

        public ActionResult Uredi(int popraviIspitId, int popravniIspitUcenikId)
        {
            PopravniIspitUcenik os = _context.PopravniIspitUcenik.Where(p => p.Id == popravniIspitUcenikId)
                .Include(p => p.OdjeljenjeStavka.Ucenik).Single();

            var model = new AjaxStavkeUrediVM
            {
                PopraviIspitId = popraviIspitId,
                PopravniIspitUcenikId = popravniIspitUcenikId,
                Bodovi = os.Rezultat,
                Ucenik = os.OdjeljenjeStavka.Ucenik.ImePrezime
            };

            return PartialView(model);
        }

        public ActionResult Snimi(AjaxStavkeUrediVM model)
        {
            PopravniIspitUcenik p = _context.PopravniIspitUcenik.Find(model.PopravniIspitUcenikId);
            p.Rezultat = model.Bodovi;
            _context.SaveChanges();

            return RedirectToAction("Index", new { popravniIspitId = model.PopraviIspitId });
        }

        public ActionResult SnimiBodovi(int popravniIspitUcenikId, int bodovi)
        {
            PopravniIspitUcenik p = _context.PopravniIspitUcenik.Find(popravniIspitUcenikId);
            p.Rezultat = bodovi;
            _context.SaveChanges();

            return RedirectToAction("Index", new { popravniIspitId = p.PopravniIspitId });
        }

        public ActionResult UcenikJePrisutan(int popravniIspitUcenikId, int PopravniIspitId)
        {
            PopravniIspitUcenik p = _context.PopravniIspitUcenik.Find(popravniIspitUcenikId);
            p.Pristupio = true;
            _context.SaveChanges();

            return RedirectToAction("Index", new { popravniIspitId = PopravniIspitId });
        }

        public ActionResult UcenikJeOdsutan(int popravniIspitUcenikId, int PopravniIspitId)
        {
            PopravniIspitUcenik p = _context.PopravniIspitUcenik.Find(popravniIspitUcenikId);
            p.Pristupio = false;
            _context.SaveChanges();

            return RedirectToAction("Index", new { popravniIspitId = PopravniIspitId });
        }
    }
}