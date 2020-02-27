using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class PopravniIspitController : Controller
    {
        private MojContext _context;

        public PopravniIspitController(MojContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            PopravniIspitIndexVM model = new PopravniIspitIndexVM
            {
                ListaSkola = _context.Skola.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                { 
                    Value = s.Id.ToString(),
                    Text = s.Naziv
                }).ToList(),
                ListaSkolskihGodina = _context.SkolskaGodina.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Naziv
                }).ToList()
            };

            return View(model);
        }


        public ActionResult Odaberi(PopravniIspitIndexVM podaci)
        {
            PopravniIspitOdaberiVM model = new PopravniIspitOdaberiVM
            {
                Razred = podaci.Razred,
                SkolaId = podaci.SkolaId,
                SkolskaGodinaId = podaci.SkolskaGodinaId,
                Skola = _context.Skola.Find(podaci.SkolaId).Naziv,
                SkolskaGodina = _context.SkolskaGodina.Find(podaci.SkolskaGodinaId).Naziv,
                rows = _context.PopravniIspit.Where(p => p.SkolaID == podaci.SkolaId && p.SkolskaGodinaID == podaci.SkolskaGodinaId && p.Predmet.Razred == podaci.Razred).Select(p => new PopravniIspitOdaberiVM.Row
                {
                    PopravniIspitId = p.Id,
                    Datum = p.Datum,
                    Predmet = p.Predmet.Naziv,
                    BrojUcenikaNaPopravnom = _context.PopravniUcenik.Where(a => a.PopravniId == p.Id).Count(),
                    BrojUcenikaKojiSuPolozili = _context.PopravniUcenik.Where(a => a.PopravniId == p.Id).Count(a => a.Rezultat > 50)
                }).ToList()
            };

            return View(model);
        }

        public ActionResult Dodaj(int skolaId, int skolskaGodinaId, int razred)
        {
            PopravniIspitDodajVM model = new PopravniIspitDodajVM
            {
                ListaNastavnika = _context.Nastavnik.Select(n => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = n.Ime + " " + n.Prezime
                }).ToList(),
                ListaPredmeta = _context.Predmet.Where(a => a.Razred == razred).Select(n => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = n.Naziv
                }).ToList(),
                Razred = razred,
                SkolaID = skolaId,
                Skola = _context.Skola.Find(skolaId).Naziv,
                SkolskaGodinaID = skolskaGodinaId,
                SkolskaGodina = _context.SkolskaGodina.Find(skolskaGodinaId).Naziv
            };
            return View(model);
        }

        public ActionResult Snimi(PopravniIspitDodajVM model)
        {
            PopravniIspit p = new PopravniIspit
            {
                ClanKomisije1Id = model.ClanKomisije1Id,
                ClanKomisije2Id = model.ClanKomisije2Id,
                ClanKomisije3Id = model.ClanKomisije3Id,
                Datum = model.Datum,
                PredmetID = model.PredmetId,
                SkolaID = model.SkolaID,
                SkolskaGodinaID = model.SkolskaGodinaID
            };

            _context.Add(p);
            _context.SaveChanges();

            List<OdjeljenjeStavka> listaOS = _context.DodjeljenPredmet.Where(a => a.PredmetId == model.PredmetId && a.ZakljucnoKrajGodine == 1).Select(a => a.OdjeljenjeStavka).ToList();

            foreach (var x in listaOS)
            {
                PopravniUcenik newPU = new PopravniUcenik
                {
                    ImaPravoNaPopravni = true,
                    OdjeljenjeStavkaId = x.Id,
                    PopravniId = p.Id,
                    Pristupio = false
                };


                int brojNegativnih = _context.DodjeljenPredmet.Where(a => a.OdjeljenjeStavkaId == x.Id).Count(a => a.ZakljucnoKrajGodine == 1);
                if (brojNegativnih >= 3)
                {
                    newPU.Rezultat = 0;
                    newPU.ImaPravoNaPopravni = false;
                }

                _context.PopravniUcenik.Add(newPU);
                _context.SaveChanges();
            }


            PopravniIspitIndexVM podaci = new PopravniIspitIndexVM
            {
                Razred = model.Razred,
                SkolaId = model.SkolaID,
                SkolskaGodinaId = model.SkolskaGodinaID
            };

            return RedirectToAction("Odaberi", podaci);
        }

        public ActionResult Uredi(int popravniIspitId)
        {
            PopravniIspit p = _context.PopravniIspit.Where(a => a.Id == popravniIspitId)
                .Include(a => a.Predmet)
                .Include(a => a.SkolskaGodina)
                .Include(a => a.Skola)
                .Single();

            PopravniIspitDodajVM model = new PopravniIspitDodajVM
            {
                ClanKomisije1 = _context.Nastavnik.Find(p.ClanKomisije1Id).Ime + " " + _context.Nastavnik.Find(p.ClanKomisije1Id).Prezime,
                ClanKomisije2 = _context.Nastavnik.Find(p.ClanKomisije2Id).Ime + " " + _context.Nastavnik.Find(p.ClanKomisije2Id).Prezime,
                ClanKomisije3 = _context.Nastavnik.Find(p.ClanKomisije3Id).Ime + " " + _context.Nastavnik.Find(p.ClanKomisije3Id).Prezime,
                Datum = p.Datum,
                Predmet = p.Predmet.Naziv,
                PopravniIspitId = p.Id,
                Razred = (int)p.Razred,
                Skola = p.Skola.Naziv,
                SkolskaGodina = p.SkolskaGodina.Naziv
            };

            return View(model);
        }

        public ActionResult DodajPopravniIspit(int popravniIspitId)
        {
            PopravniIspit p = _context.PopravniIspit.Find(popravniIspitId);

            return RedirectToAction("Dodaj", new { skolaId = p.SkolaID, skolskaGodinaId = p.SkolskaGodinaID, razred = p.Razred });
        }

        public ActionResult DodajUcenika(int popravniIspitId)
        {
            
            StavkeUrediVM model = new StavkeUrediVM
            {
                PopravniIspitId = popravniIspitId,
                ListaUcenika = _context.OdjeljenjeStavka.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Ucenik.ImePrezime + " " + u.Odjeljenje.Oznaka
                }).ToList()
            };

            return PartialView("../Stavke/Dodaj", model);
        }

    }
}