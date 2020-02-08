using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ispit_2017_09_11_DotnetCore.EF;
using Ispit_2017_09_11_DotnetCore.EntityModels;
using Ispit_2017_09_11_DotnetCore.ViewModels;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.Math;

namespace Ispit_2017_09_11_DotnetCore.Controllers
{
    public class OdjeljenjeController : Controller
    {
        private MojContext _context;

        public OdjeljenjeController(MojContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new OdjeljenjeIndexVM
            {
                rows = _context.Odjeljenje.Select(o => new OdjeljenjeIndexVM.Row
                {
                    NajboljiUcenik = "??",
                    Oznaka = o.Oznaka,
                    PrebaceniUViseOdjeljenja = o.IsPrebacenuViseOdjeljenje,
                    ProsjekOcjena = Math.Round( _context.DodjeljenPredmet.Where(dp => dp.OdjeljenjeStavka.OdjeljenjeId == o.Id).Average(dp => (int?) dp.ZakljucnoKrajGodine) ?? 0, 2),
                    Razred = o.Razred,
                    SkolskaGodina = o.SkolskaGodina,
                    Razrednik = o.Nastavnik.ImePrezime,
                    OdjeljenjeId = o.Id
                }).ToList()
            };


            return View(model);
        }

        public IActionResult Dodaj()
        {
            OdjeljenjeDodajVM model = new OdjeljenjeDodajVM
            {
                ListaNizihOdjeljenja = _context.Odjeljenje.Select(o => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = o.Id.ToString(),
                    Text = o.SkolskaGodina + o.Oznaka
                }).ToList(),
                ListaRazrednika = _context.Nastavnik.Select(n => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = n.NastavnikID.ToString(),
                    Text = n.ImePrezime
                }).ToList()
            };

            return View(model);
        }


        public IActionResult Snimi(OdjeljenjeDodajVM model)
        {
            Odjeljenje o2 = new Odjeljenje
            {
                IsPrebacenuViseOdjeljenje = false,
                NastavnikID = model.RazrednikId,
                Oznaka = model.Oznaka,
                Razred = model.Razred,
                SkolskaGodina = model.SkolskaGodina
            };

            _context.Odjeljenje.Add(o2);
            _context.SaveChanges();

            Odjeljenje o1 = _context.Odjeljenje.Find(model.NizeOdjeljenjeId);

            if(o1 != null)
            {
                o1.IsPrebacenuViseOdjeljenje = true;
                List<OdjeljenjeStavka> listaOS = _context.OdjeljenjeStavka.Where(os => os.OdjeljenjeId == o1.Id).ToList();

                foreach (var s1 in listaOS)
                {
                    int brojNegativnih = _context.DodjeljenPredmet.Where(dp => dp.OdjeljenjeStavkaId == s1.Id).Count(dp => dp.ZakljucnoKrajGodine == 1);
                    
                    if (brojNegativnih == 0)
                    {
                        OdjeljenjeStavka s2 = new OdjeljenjeStavka
                        {
                            BrojUDnevniku = 0,
                            OdjeljenjeId = o2.Id,
                            UcenikId = s1.UcenikId
                        };
                        _context.OdjeljenjeStavka.Add(s2);
                        _context.SaveChanges();


                        List<Predmet> listaPredmeta = _context.Predmet.Where(p => p.Razred == o2.Razred).ToList();

                        foreach (var predmeti in listaPredmeta)
                        {
                            DodjeljenPredmet newDP = new DodjeljenPredmet
                            {
                                OdjeljenjeStavkaId = s2.Id,
                                PredmetId = predmeti.Id,
                                ZakljucnoKrajGodine = 0,
                                ZakljucnoPolugodiste = 0
                            };
                            _context.DodjeljenPredmet.Add(newDP);
                            _context.SaveChanges();

                        }
                    }
                }
            }


            return RedirectToAction("Index");
        }

        public ActionResult Obrisi(int odjeljenjeId)
        {
            Odjeljenje o = _context.Odjeljenje.Find(odjeljenjeId);
            _context.Odjeljenje.Remove(o);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Detalji(int odjeljenjeId)
        {
            var o = _context.Odjeljenje.Where(o1 => o1.Id == odjeljenjeId).Include(o1 => o1.Nastavnik).Single();

            OdjeljenjeDetaljiVM model = new OdjeljenjeDetaljiVM
            {
                Oznaka = o.Oznaka,
                Razred = o.Razred,
                SkolskaGodina = o.SkolskaGodina,
                BrojPredmeta = _context.Predmet.Count(p => p.Razred == o.Razred),
                OdjeljenjeId = odjeljenjeId
            };

            if (o.Nastavnik != null)
            {
                model.Razrednik = o.Nastavnik.ImePrezime;
            }

            return View(model);
        }

        public ActionResult Rekonstrukcija(int OdjeljenjeId)
        {
            List<OdjeljenjeStavka> listaOS = _context.OdjeljenjeStavka.Where(os => os.OdjeljenjeId == OdjeljenjeId).Include(os => os.Ucenik).OrderBy(os => os.Ucenik.ImePrezime).ToList();

            for(int i = 0; i < listaOS.Count; i++)
            {
                listaOS[i].BrojUDnevniku = i + 1;
            }
            _context.SaveChanges();


            return RedirectToAction("Detalji", new { odjeljenjeId = OdjeljenjeId });
        }
    }
}

