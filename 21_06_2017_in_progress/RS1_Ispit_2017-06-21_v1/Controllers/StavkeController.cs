using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RS1_Ispit_2017_06_21_v1.EF;
using RS1_Ispit_2017_06_21_v1.ViewModels;
using RS1_Ispit_2017_06_21_v1.Models;
using Microsoft.EntityFrameworkCore;

namespace RS1_Ispit_2017_06_21_v1.Controllers
{
    public class StavkeController : Controller
    {
        private MojContext db;

        public StavkeController(MojContext db)
        {
            this.db = db;
        }
        public ActionResult Index(int maturskiIspitId)
        {
            var model = new StavkeIndexVM
            {
                rows = db.MaturskiIspitStavka.Where(mis => mis.MaturskiIspitId == maturskiIspitId).Select(mis => new StavkeIndexVM.Row
                {
                    Bodovi = (float?)mis.Bodovi ?? 0,
                    OpciUspjeh = mis.UpisUOdjeljenje.OpciUspjeh,
                    Oslobodjen = mis.Oslobodjen,
                    StavkaId = mis.Id,
                    Ucenik = mis.UpisUOdjeljenje.Ucenik.ImePrezime
                }).ToList()
            };

            return PartialView(model);
        }


        public ActionResult Uredi(int stavkaId)
        {
            MaturskiIspitStavka mis = db.MaturskiIspitStavka.Where(m => m.Id == stavkaId).Include(m => m.UpisUOdjeljenje.Ucenik).Single();
            var model = new StavkeUrediVM
            {
                StavkaId = stavkaId,
                Bodovi = mis.Bodovi ?? 0,
                Ucenik = mis.UpisUOdjeljenje.Ucenik.ImePrezime
            };

            return PartialView(model);
        }

        public ActionResult Snimi(StavkeUrediVM model)
        {
            MaturskiIspitStavka mis = db.MaturskiIspitStavka.Find(model.StavkaId);
            mis.Bodovi = model.Bodovi;
            db.SaveChanges();

            return RedirectToAction("Index", new { maturskiIspitId = mis.MaturskiIspitId });
        }

        public ActionResult SnimiBodove(int bodovi, int stavkaId)
        {
            MaturskiIspitStavka mis = db.MaturskiIspitStavka.Find(stavkaId);
            mis.Bodovi = bodovi;
            db.SaveChanges();

            return RedirectToAction("Index", new { maturskiIspitId = mis.MaturskiIspitId });
        }

    }
}