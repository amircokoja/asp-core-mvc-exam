using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.ViewModels;
using System;
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
            int veci = DateTime.Compare(i.Datum, DateTime.Now);

            StavkeIndexVM model = new StavkeIndexVM
            {
                Zakljucan = i.Zakljuceno,
                TrenutniDatumVeci = veci < 0,
                rows = _context.IspitStavke.Where(a => a.IspitId == ispitId).Select(a => new StavkeIndexVM.Row
                {
                    IspitStavkeId = a.Id,
                    Ocjena = a.Ocjena,
                    Pristupio = a.Pristupio,
                    Student = a.SlusaPredmet.UpisGodine.Student.Ime + " " + a.SlusaPredmet.UpisGodine.Student.Prezime
                }).ToList()
            };

            return PartialView(model);
        }

        public ActionResult Pristupio(int ispitStavkeId)
        {
            IspitStavke i = _context.IspitStavke.Find(ispitStavkeId);
            i.Pristupio = !i.Pristupio;
            _context.SaveChanges();

            return RedirectToAction("Index", new { ispitId = i.IspitId });
        }

        public ActionResult UrediStavke(int ispitStavkeId)
        {
            IspitStavke i = _context.IspitStavke.Where(a => a.Id == ispitStavkeId)
                .Include(a => a.SlusaPredmet.UpisGodine.Student).Single();

            StavkeUrediVM model = new StavkeUrediVM
            {
                ispitStavkeId = ispitStavkeId,
                Ocjena = i.Ocjena,
                Student = i.SlusaPredmet.UpisGodine.Student.Ime + " " + i.SlusaPredmet.UpisGodine.Student.Prezime
            };

            return PartialView(model);
        }

        public ActionResult SnimiOcjenu(StavkeUrediVM model)
        {
            IspitStavke i = _context.IspitStavke.Find(model.ispitStavkeId);
            i.Ocjena = model.Ocjena;
            _context.SaveChanges();

            return RedirectToAction("Index", new { ispitId = i.IspitId });
        }


        public ActionResult SnimiOcjenuInput(int ispitStavkaId, int ocjena)
        {
            IspitStavke i = _context.IspitStavke.Find(ispitStavkaId);
            i.Ocjena = ocjena;
            _context.SaveChanges();

            return RedirectToAction("Index", new { ispitId = i.IspitId });
        }
    }
}