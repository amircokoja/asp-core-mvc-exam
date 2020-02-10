using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eUniverzitet.Web.Helper;
using Ispit.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Ispit.Data;
using Ispit.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using Ispit.Web.ViewModels;

namespace Ispit.Web.Controllers
{
    [Autorizacija]
    public class OznaceniDogadajiController : Controller
    {
        private MyContext _context;
        public OznaceniDogadajiController(MyContext db)
        {
            _context = db;
        }

        public float ProcenatZavrsenih(int DogadjajId)
        {
            List<Obaveza> listaObaveza = _context.Obaveza.Where(o => o.DogadjajID == DogadjajId).ToList();
            float procenatZavrsenih = 0;

            foreach (var x in listaObaveza)
            {
                StanjeObaveze sO = _context.StanjeObaveze.Where(so => so.ObavezaID == x.ID).FirstOrDefault();

                if (sO != null)
                {
                    procenatZavrsenih += sO.IzvrsenoProcentualno;
                }
            }

            float procenat = 0;

            if (listaObaveza.Count() != 0)
            {
                procenat = procenatZavrsenih / listaObaveza.Count();
            }

            return procenat;
        }

        public IActionResult Index()
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            if (korisnik == null)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa";
                return RedirectToAction("Index", "Autentifikacija");
            }

            var sviDogadjaji = _context.Dogadjaj.Include(d => d.Nastavnik).ToList();
            OznaceniDogadajiVM model = new OznaceniDogadajiVM
            {
                korisnikId = korisnik.Id,
                NeoznaceniDogadjaji = new List<OznaceniDogadajiVM.Neoznaceni>(),
                OznaceniDogadjaji = new List<OznaceniDogadajiVM.Oznaceni>()
            };

            foreach (var x in sviDogadjaji)
            {
                int brojOznacenihObaveza = _context.OznacenDogadjaj.Where(od => od.DogadjajID == x.ID && od.StudentID == korisnik.Id).Count();

                if(brojOznacenihObaveza == 0)
                {
                    model.NeoznaceniDogadjaji.Add(
                        new OznaceniDogadajiVM.Neoznaceni
                        {
                            DatumDogadjaja = x.DatumOdrzavanja,
                            Nastavnik = x.Nastavnik.ImePrezime,
                            OpisDogadjaja = x.Opis,
                            BrojObaveza = _context.Obaveza.Where(o => o.DogadjajID == x.ID).Count(),
                            DogadjajId = x.ID
                        }
                        );
                } else
                {
                    model.OznaceniDogadjaji.Add(new OznaceniDogadajiVM.Oznaceni
                    {
                        DatumDogadjaja = x.DatumOdrzavanja,
                        Nastavnik = x.Nastavnik.ImePrezime,
                        OpisDogadjaja = x.Opis,
                        RealizovanoObaveza = ProcenatZavrsenih(x.ID),
                        DogadjajId = x.ID
                    });
                }
            }

            return View(model);
        }

        public ActionResult Dodaj(int dogadjajId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();

            Dogadjaj d = _context.Dogadjaj.Find(dogadjajId);

            OznacenDogadjaj oD = new OznacenDogadjaj
            {
                DatumDodavanja = DateTime.Now,
                DogadjajID = d.ID,
                StudentID = korisnik.Id
            };
            _context.OznacenDogadjaj.Add(oD);
            _context.SaveChanges();


            List<Obaveza> listaObaveza = _context.Obaveza.Where(o => o.DogadjajID == dogadjajId).ToList();

            foreach (var obaveza in listaObaveza)
            {
                _context.StanjeObaveze.Add(new StanjeObaveze
                {
                     DatumIzvrsenja = new DateTime(2000, 1, 1),
                     IsZavrseno = false,
                     IzvrsenoProcentualno = 0,
                     NotifikacijaDanaPrije = 0,
                     NotifikacijeRekurizivno = false,
                     ObavezaID = obaveza.ID,
                     OznacenDogadjajID = _context.OznacenDogadjaj.Where(od => od.DogadjajID == dogadjajId && od.StudentID == korisnik.Id).Single().ID
                });

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Detalji(int dogadjajId)
        {
            OznacenDogadjaj d = _context.OznacenDogadjaj.Where(od => od.DogadjajID == dogadjajId)
                .Include(od => od.Dogadjaj)
                .Include(od => od.Dogadjaj.Nastavnik)
                .Single();
            var model = new OznaceniDogadajiDetaljiVM
            {
                OznaceniDogadjajId = d.ID,
                DatumDodavanja = d.DatumDodavanja,
                DatumDogadjaja = d.Dogadjaj.DatumOdrzavanja,
                Nastavnik = d.Dogadjaj.Nastavnik.ImePrezime,
                Opis = d.Dogadjaj.Opis
            };

            return View(model);
        }


    }
}