using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ispit.Web.ViewModels;
using Ispit.Data.EntityModels;
using Ispit.Data;
using Microsoft.EntityFrameworkCore;
using static Ispit.Web.ViewModels.AjaxTestPodsjetniciVM.Dogadjaji;

namespace Ispit_2017_09_11_DotnetCore.Controllers
{
    public class AjaxTestController : Controller
    {
        private MyContext _context;
        public AjaxTestController(MyContext db)
        {
            _context = db;
        }
        public IActionResult Index(int oznaceniDogadjajId)
        {
            return View();
        }

        public IActionResult Obaveze(int oznaceniDogadjajId)
        {
            List<StanjeObaveze> listaStanjaObaveza = _context.StanjeObaveze.Where(so => so.OznacenDogadjajID == oznaceniDogadjajId).ToList();

            AjaxTestObavezeVM model = new AjaxTestObavezeVM
            {
                rows = _context.StanjeObaveze.Where(so => so.OznacenDogadjajID == oznaceniDogadjajId).Select(so => new AjaxTestObavezeVM.Row
                {
                    BrojDana = so.NotifikacijaDanaPrije,
                    Naziv = so.Obaveza.Naziv,
                    Ponavljaj = so.NotifikacijeRekurizivno,
                    Procenat = so.IzvrsenoProcentualno,
                    StanjeObavezeId = so.Id
                }).ToList()
            };

            return View(model);
        }

        public IActionResult AjaxTestAction()
        {
            return PartialView("_AjaxTestView");
        }

        public ActionResult Uredi(int stanjeObavezeId)
        {
            StanjeObaveze so = _context.StanjeObaveze.Where(s => s.Id == stanjeObavezeId)
                .Include(s => s.Obaveza).Single();

            var model = new AjaxTestUrediVM
            {
                StanjeObavezeId = so.Id,
                NazivObaveze = so.Obaveza.Naziv,
                Procenat = so.IzvrsenoProcentualno
            };

            return PartialView(model);
        }

        public ActionResult Snimi(AjaxTestUrediVM model)
        {
            StanjeObaveze so = _context.StanjeObaveze.Find(model.StanjeObavezeId);

            so.IzvrsenoProcentualno = model.Procenat;
            _context.SaveChanges();

            int oznaceniDogId = so.OznacenDogadjajID;
            return RedirectToAction("Obaveze", new { oznaceniDogadjajId = oznaceniDogId });
        }

        public ActionResult Podsjetnici(int korisnikId)
        {
            var model = new AjaxTestPodsjetniciVM
            {
                Podsjetnici = _context.OznacenDogadjaj.Where(od => od.StudentID == korisnikId).Select(od => new AjaxTestPodsjetniciVM.Dogadjaji
                {
                    DatumDogadjaja = od.Dogadjaj.DatumOdrzavanja,
                    OpisDogadjaja = od.Dogadjaj.Opis,
                    ListaObaveza = _context.StanjeObaveze.Where(o => o.OznacenDogadjajID == od.ID).Select(o => new ObavezeDogadjaja
                    {
                        NazivObaveze = o.Obaveza.Naziv,
                        StanjeObavezeId = o.Id
                    }).ToList()
                }).ToList()
            };

            return PartialView(model);
        }

    }
}