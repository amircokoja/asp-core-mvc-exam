using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ispit_2017_09_11_DotnetCore.EF;
using Ispit_2017_09_11_DotnetCore.EntityModels;
using Ispit_2017_09_11_DotnetCore.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Ispit_2017_09_11_DotnetCore.Controllers
{
    public class AjaxController : Controller
    {
        private MojContext _context;
        public AjaxController(MojContext context)
        {
            _context = context;
        }

        public IActionResult Index(int odjeljenjeId)
        {
            AjaxIndexVM model = new AjaxIndexVM
            {
                OdjeljenjeId = odjeljenjeId,


                rows = _context.OdjeljenjeStavka.Where(os => os.OdjeljenjeId == odjeljenjeId).Include(os => os.Ucenik).OrderBy(os => os.Ucenik.ImePrezime).Select(o => new AjaxIndexVM.Row
                {
                    BrojUDneviku = o.BrojUDnevniku,
                    OdjeljenjeStavkaId = o.Id,
                    Ucenik = o.Ucenik.ImePrezime,
                    BrojZakljucenihOcjena = _context.DodjeljenPredmet.Where(dp => dp.OdjeljenjeStavkaId == o.OdjeljenjeId).Count()
                }).ToList()
            };

            return PartialView(model);
        }

        public ActionResult Dodaj(int odjeljenjeId)
        {
            var model = new AjaxDodajVM
            {
                ListaUcenika = _context.Ucenik.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = u.ImePrezime,
                    Value = u.Id.ToString()
                }).ToList(),
                OdjeljenjeId = odjeljenjeId,
                OdjeljenjeStavkeId = 0
            };

            return PartialView(model);
        }


        public ActionResult Snimi(AjaxDodajVM model)
        {
            OdjeljenjeStavka os;
            
            if (model.OdjeljenjeStavkeId == 0)
            {
                os = new OdjeljenjeStavka();
                _context.OdjeljenjeStavka.Add(os);
            } else
            {
                os = _context.OdjeljenjeStavka.Find(model.OdjeljenjeStavkeId);
            }

            os.BrojUDnevniku = model.BrojUDnevniku;
            os.OdjeljenjeId = model.OdjeljenjeId;
            os.UcenikId = model.UcenikId;

            _context.SaveChanges();

            return RedirectToAction("Index", new { odjeljenjeId = model.OdjeljenjeId });
        }

        public ActionResult Obrisi(int odjeljenjeStavkaId)
        {
            OdjeljenjeStavka os = _context.OdjeljenjeStavka.Find(odjeljenjeStavkaId);

            if (os != null)
            {
                _context.OdjeljenjeStavka.Remove(os);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", new { odjeljenjeId = os.OdjeljenjeId });
        }

        public ActionResult Uredi(int odjeljenjeStavkaId)
        {
            OdjeljenjeStavka os = _context.OdjeljenjeStavka.Find(odjeljenjeStavkaId);

            AjaxDodajVM model = new AjaxDodajVM
            {
                OdjeljenjeId = os.OdjeljenjeId,
                UcenikId = os.UcenikId,
                BrojUDnevniku = os.BrojUDnevniku,
                ListaUcenika = _context.Ucenik.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = u.ImePrezime,
                    Value = u.Id.ToString()
                }).ToList(),
                OdjeljenjeStavkeId = os.Id
            };
            return PartialView("Dodaj", model);
        }

    }
}