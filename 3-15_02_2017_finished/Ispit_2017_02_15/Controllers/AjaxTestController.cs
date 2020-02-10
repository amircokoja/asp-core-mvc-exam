using Ispit_2017_02_15.EF;
using Microsoft.AspNetCore.Mvc;
using Ispit_2017_02_15.ViewModels;
using System.Linq;
using Ispit_2017_02_15.Models;
using Microsoft.EntityFrameworkCore;

namespace Ispit_2017_02_15.Controllers
{
    public class AjaxTestController : Controller
    {
        private MojContext _context;

        public AjaxTestController(MojContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AjaxTestAction()
        {
            return View("_AjaxTestView");
        }

        public ActionResult IspisStudenata(int odrzaniCasId)
        {
            OdrzaniCas odrzaniCas = _context.OdrzaniCasovi.Find(odrzaniCasId);

            var model = new AjaxTestIspisStudenataVM
            {
                listaStudenata = _context.OdrzaniCasDetalji.Where(s => s.OdrzaniCasId == odrzaniCas.Id).Select(s => new AjaxTestIspisStudenataVM.StudentiLista
                {
                    Student = s.SlusaPredmet.UpisGodine.Student.Ime + " " + s.SlusaPredmet.UpisGodine.Student.Prezime,
                    Bodovi = _context.OdrzaniCasDetalji.Where(ocd => ocd.SlusaPredmet.UpisGodine.StudentId == s.SlusaPredmet.UpisGodine.StudentId).Sum(ocd => ocd.BodoviNaCasu),
                    Prisutan = s.Prisutan,
                    OdrzaniCasDetaljiId = s.Id
                }).ToList()
                
            };
            return PartialView(model);
        }

        public ActionResult UcenikJePrisutan(int odrzaniCasDetaljiId)
        {
            OdrzaniCasDetalji o = _context.OdrzaniCasDetalji.Find(odrzaniCasDetaljiId);
            o.Prisutan = true;
            _context.SaveChanges();

            return RedirectToAction("Uredi", "Odjeljenje" , new { odrzaniCasoviId = o.OdrzaniCasId });
        }

        public ActionResult UcenikJeOdsutan(int odrzaniCasDetaljiId)
        {
            OdrzaniCasDetalji o = _context.OdrzaniCasDetalji.Find(odrzaniCasDetaljiId);
            o.Prisutan = false;
            _context.SaveChanges();

            return RedirectToAction("Uredi", "Odjeljenje", new { odrzaniCasoviId = o.OdrzaniCasId });
        }

        public ActionResult Uredi(int odrzaniCasDetaljiId)
        {
            OdrzaniCasDetalji o = _context.OdrzaniCasDetalji.Where(ocs => ocs.Id == odrzaniCasDetaljiId)
                .Include(ocs => ocs.SlusaPredmet.UpisGodine.Student)
                .Single();

            AjaxTestUrediVM model = new AjaxTestUrediVM
            {
                OdrzaniCasDetaljiId = odrzaniCasDetaljiId,
                Student = o.SlusaPredmet.UpisGodine.Student.Ime + " " + o.SlusaPredmet.UpisGodine.Student.Prezime,
                Bodovi = o.BodoviNaCasu
            };

            return PartialView(model);
        }

        public ActionResult Snimi(AjaxTestUrediVM model)
        {
            OdrzaniCasDetalji o = _context.OdrzaniCasDetalji.Find(model.OdrzaniCasDetaljiId);
            o.BodoviNaCasu = model.Bodovi;
            _context.SaveChanges();

            return RedirectToAction("IspisStudenata", new { odrzaniCasId = o.OdrzaniCasId });
        }
    }
}