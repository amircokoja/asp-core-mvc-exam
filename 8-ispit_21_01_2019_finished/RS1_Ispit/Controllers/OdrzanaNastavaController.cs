using Microsoft.AspNetCore.Mvc;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.ViewModels;
using RS1_Ispit_asp.net_core.EntityModels;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class OdrzanaNastavaController : Controller
    {
        private MojContext _context;

        public OdrzanaNastavaController(MojContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            OdrzanaNastavaIndexVM model = new OdrzanaNastavaIndexVM
            {
                rows = _context.PredajePredmet.Select(n => new OdrzanaNastavaIndexVM.Row
                {
                    NastavnikId = n.NastavnikID,
                    Nastavnik = n.Nastavnik.Ime + " " + n.Nastavnik.Prezime,
                    SkolaId = n.Odjeljenje.SkolaID,
                    Skola = n.Odjeljenje.Skola.Naziv
                }).Distinct().ToList()
            };

            return View(model);
        }
        
        public ActionResult Odaberi(int skolaId, int nastavnikId)
        {
            OdrzanaNastavaOdaberiVM model = new OdrzanaNastavaOdaberiVM
            {
                NastavnikId = nastavnikId,
                rows = _context.MaturskiIspit.Where(m => m.NastavnikId == nastavnikId && m.SkolaId == skolaId).Select(m => new OdrzanaNastavaOdaberiVM.Row
                {
                    Datum = m.Datum,
                    MaturskiIspitId = m.Id,
                    Predmet = m.Predmet.Naziv,
                    Skola = m.Skola.Naziv,
                    NisuPristupiliLista = _context.MaturskiIspitStavke.Where(mis => mis.MaturskiIspitId == m.Id && mis.Pristupio == false).Select(mis => mis.OdjeljenjeStavka.Ucenik.ImePrezime).ToList()
                }).ToList()
            };

            string ucenici = "";
            foreach (var x in model.rows)
            {
                foreach (var y in x.NisuPristupiliLista)
                {
                    ucenici = ucenici + ", " + y;
                }
                x.NisuPristupili = ucenici;
                ucenici = "";
            }
            return View(model);
        }

        public ActionResult Dodaj(int nastavnikId)
        {
            Nastavnik n = _context.Nastavnik.Find(nastavnikId);
            OdrzanaNastavaDodajVM model = new OdrzanaNastavaDodajVM
            {
                ListaSkola = _context.Skola.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Naziv
                }).ToList(),
                ListaPredmeta = _context.Predmet.Select(p => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Naziv
                }).ToList(),
                NastavnikId = nastavnikId,
                Nastavnik = n.Ime + " " + n.Prezime,
                SkolskaGodinaId = _context.SkolskaGodina.Where(sk => sk.Aktuelna).FirstOrDefault().Id,
                SkolskaGodina = _context.SkolskaGodina.Where(sk => sk.Aktuelna).FirstOrDefault().Naziv
            };

            return View(model);
        }

        public ActionResult Snimi(OdrzanaNastavaDodajVM model)
        {
            MaturskiIspit m = new MaturskiIspit
            {
                Datum = model.Datum,
                NastavnikId = model.NastavnikId,
                PredmetId = model.PredmetId,
                SkolaId = model.SkolaId,
                SkolskaGodinaId = model.SkolskaGodinaId
            };

            _context.MaturskiIspit.Add(m);
            _context.SaveChanges();

            List<OdjeljenjeStavka> listaOS = _context.OdjeljenjeStavka.Where(os => os.Odjeljenje.SkolaID == m.SkolaId && os.Odjeljenje.Razred == 4).ToList();

            foreach (var x in listaOS)
            {
                int brojNegativnih = _context.DodjeljenPredmet.Where(dp => dp.OdjeljenjeStavkaId == x.Id).Count(dp => dp.ZakljucnoKrajGodine == 1);
                
                if(brojNegativnih == 0)
                {
                    List<MaturskiIspit> mi2 = _context.MaturskiIspit.Where(mi => mi.NastavnikId == m.NastavnikId && mi.PredmetId == m.PredmetId && mi.SkolaId == m.SkolaId).ToList();

                    foreach (var y in mi2)
                    {
                        List<MaturskiIspitStavke> MIS = _context.MaturskiIspitStavke.Where(conMIS => conMIS.MaturskiIspitId == y.Id).ToList();
                        int imaPolozen = MIS.Where(h => h.OdjeljenjeStavkaId == x.Id && h.Rezultat > 55).Count();

                        if(imaPolozen == 0)
                        {
                            MaturskiIspitStavke noviMIS = new MaturskiIspitStavke
                            {
                                MaturskiIspitId = m.Id,
                                OdjeljenjeStavkaId = x.Id,
                                Pristupio = false,
                                Rezultat = null
                            };
                            _context.MaturskiIspitStavke.Add(noviMIS);
                            _context.SaveChanges();
                        }
                    }
                }
            }

            return RedirectToAction("Odaberi", new { skolaId = model.SkolaId, nastavnikId = model.NastavnikId });
        }

        public ActionResult Uredi(int MaturskiIspitId)
        {
            MaturskiIspit m = _context.MaturskiIspit.Where(mi => mi.Id == MaturskiIspitId)
                .Include(mi => mi.Predmet)
                .Single();

            OdrzanaNastavaUrediVM model = new OdrzanaNastavaUrediVM
            {
                MaturskiIspitId = MaturskiIspitId,
                Datum = m.Datum,
                Predmet = m.Predmet.Naziv,
                Napomena = m.Napomena
            };

            return View(model);
        }

        public ActionResult SnimiUredi(OdrzanaNastavaUrediVM model)
        {
            MaturskiIspit m = _context.MaturskiIspit.Find(model.MaturskiIspitId);
            m.Napomena = model.Napomena;
            _context.SaveChanges();

            return RedirectToAction("Odaberi", new { skolaId = m.SkolaId, nastavnikId = m.NastavnikId });
        }
    }
}