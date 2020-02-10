using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.ViewModels;
using System.Linq;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class IspitController : Controller
    {
        private MojContext _context;

        public IspitController(MojContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            IspitIndexVM model = new IspitIndexVM
            {
                rows = _context.Angazovan.Select(a => new IspitIndexVM.Row
                {
                    AkademskaGodina = a.AkademskaGodina.Opis,
                    AngazovanId = a.Id,
                    Nastavnik = a.Nastavnik.Ime + " " + a.Nastavnik.Prezime,
                    NazivPredemta = a.Predmet.Naziv,
                    BrojOdrzanihCasova = _context.OdrzaniCas.Where(o => o.AngazovaniId == a.Id).Count(),
                    BrojStudenataNaPredmetu = _context.SlusaPredmet.Where(s => s.AngazovanId == a.Id).Count()
                }).ToList()
            };

            return View(model);
        }

        public ActionResult Odaberi(int angazovanId)
        {
            IspitOdaberiVM model = new IspitOdaberiVM
            {
                AngazovanId = angazovanId,
                rows = _context.Ispit.Where(i => i.AngazovanId == angazovanId).Select(i => new IspitOdaberiVM.Row
                {
                    IspitId = i.Id,
                    Datum = i.Datum,
                    Zakljucano = i.Zakljuceno,
                    BrojPrijavljenihStudenata = _context.IspitStavke.Where(a => a.IspitId == i.Id).Count(),
                    BrojStudenataKojiNisuPolozili = _context.SlusaPredmet.Where(b => b.AngazovanId == i.AngazovanId && b.Ocjena < 6).Count()
                }).ToList()
            };

            return View(model);
        }

        public ActionResult Dodaj(int angazovanId)
        {
            Angazovan a = _context.Angazovan.Where(ai => ai.Id == angazovanId)
                .Include(ai => ai.Predmet)
                .Include(ai => ai.Nastavnik)
                .Include(ai => ai.AkademskaGodina)
                .Single();

            IspitDodajVM model = new IspitDodajVM
            {
                AngazovanId = a.Id,
                Nastavnik = a.Nastavnik.Ime + " " + a.Nastavnik.Prezime,
                AkademskaGodina = a.AkademskaGodina.Opis,
                Predmet = a.Predmet.Naziv
            };

            ViewBag.Model = 0;

            return View(model);
        }

        public ActionResult Zakljucaj(int ispitId)
        {
            Ispit i = _context.Ispit.Find(ispitId);
            i.Zakljuceno = true;
            _context.SaveChanges();
            return RedirectToAction("Odaberi", new { angazovanId = i.AngazovanId });
        }

        public ActionResult Detalji(int ispitId)
        {
            Ispit i = _context.Ispit.Where(ai => ai.Id == ispitId)
                .Include(ai => ai.Angazovan.AkademskaGodina)
                .Include(ai => ai.Angazovan.Nastavnik)
                .Include(ai => ai.Angazovan.Predmet)
                .Single();

            IspitDodajVM model = new IspitDodajVM
            {
                IspitId = i.Id,
                AkademskaGodina = i.Angazovan.AkademskaGodina.Opis,
                AngazovanId = i.AngazovanId,
                Datum = i.Datum,
                Napomena = i.Napomena,
                Nastavnik = i.Angazovan.Nastavnik.Ime + " " + i.Angazovan.Nastavnik.Prezime,
                Predmet = i.Angazovan.Predmet.Naziv
            };

            ViewBag.Model = 1;

            return View("Dodaj", model);
        }

        public ActionResult DodajStudenta(int ispitId)
        {
            int angazovanId = _context.Ispit.Find(ispitId).AngazovanId;
            IspitDodajStudentaVM model = new IspitDodajStudentaVM
            {
                IspitId = ispitId,
                ListaStudenata = _context.SlusaPredmet.Where(a => a.AngazovanId == angazovanId).Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.UpisGodine.Student.Ime + " " + a.UpisGodine.Student.Prezime
                }).Distinct().ToList()
            };

            return View(model);
        }

        public ActionResult SnimiStudenta(IspitDodajStudentaVM model)
        {
            IspitStavke i = new IspitStavke
            {
                IspitId = model.IspitId,
                Pristupio = false,
                SlusaPredmetId = model.SlusaPredmetId
            };

            _context.IspitStavke.Add(i);
            _context.SaveChanges();


            return RedirectToAction("Detalji", new { ispitId = model.IspitId });
        }

        public ActionResult Snimi(IspitDodajVM model)
        {
            Ispit i = new Ispit
            {
                AngazovanId = model.AngazovanId,
                Datum = model.Datum,
                Napomena = model.Napomena,
                Zakljuceno = false
            };

            _context.Ispit.Add(i);
            _context.SaveChanges();

            return RedirectToAction("Odaberi", new { angazovanId = model.AngazovanId });
        }

    }
}