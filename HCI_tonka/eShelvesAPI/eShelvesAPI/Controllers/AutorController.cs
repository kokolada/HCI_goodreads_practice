using eShelvesAPI.DAL;
using eShelvesAPI.Models;
using eShelvesAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eShelvesAPI.Controllers
{
    public class AutorController : ApiController
    {
        private MojContext db = new MojContext();

        [HttpGet]
        public List<AutorVM> GetAutors()
        {
            return db.Autors.Select(x => new AutorVM
            {
                Id = x.Id,
                Naziv = x.Ime + " " + x.Prezime
            }).ToList();
        }

        [HttpGet]
        public AutorVMM GetAutor(int id)
        {
            AutorVM a = new AutorVM();
            return db.Autors.Where(x => x.Id == id).Select(s => new AutorVMM 
            {
                Id = s.Id,
                Ime = s.Ime,
                Kategorijas = s.Kategorijas,
                MjestoRodjenja = s.MjestoRodjenja,
                Opis = s.Opis,
                Prezime = s.Prezime,
                Rodjen = s.Rodjen,
                Umro = s.Umro,
                WebStranica = s.WebStranica
            }).FirstOrDefault();
        }

        [HttpGet]
        [Route("api/Autor/Search/{query?}")]
        public List<Autor> GetAutorsByName(string query)
        {
            var tokens = query.Split(' ');
            return db.Autors.Where(x => tokens.All(t => (x.Ime + " " + x.Prezime).Contains(t))).ToList();
        }

        [HttpGet]
        [Route("api/Autor/Search")]
        public List<Autor> GetAllAutors()
        {
            return db.Autors.ToList();
        }

        [HttpGet]
        [Route("api/Autor/Remove/{id}")]
        public IHttpActionResult RemoveAutor(int id)
        {
            Autor a = db.Autors.Find(id);
            if (a != null)
            {
                db.Autors.Remove(a);
                db.SaveChanges();

                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        public Autor PostAutor(Autor a)
        {
            if (a.Id > 0)
            {
                Autor stari = db.Autors.Find(a.Id);
                stari.Ime = a.Ime;
                stari.Prezime = a.Prezime;
                stari.Rodjen = a.Rodjen;
                stari.MjestoRodjenja = a.MjestoRodjenja;
                stari.Opis = a.Opis;
                stari.Umro = a.Umro;
                stari.WebStranica = a.WebStranica;
                db.SaveChanges();

                string[] arr = a.Kategorijas.Select(y => y.Naziv).ToArray();
                List<Kategorija> kategorije = (from p in db.Kategorijas where arr.Any(x => p.Naziv.Contains(x)) select p).ToList();
                eShelvesEntities ctx = new eShelvesEntities();
                ctx.usp_RemoveAutorKategorijas(stari.Id);
                foreach (Kategorija kat in kategorije)
                {
                    ctx.usp_addAutorKategorija(kat.Id, stari.Id);
                }
                db.SaveChanges();
            }
            else
            {
                string[] arr = a.Kategorijas.Select(y => y.Naziv).ToArray();
                List<Kategorija> kategorije = (from p in db.Kategorijas where arr.Any(x => p.Naziv.Contains(x)) select p).ToList();
                a.Kategorijas = kategorije;
                db.Autors.Add(a);
                db.SaveChanges();
            }
            
            return a;
        }
    }

    public class AutorVM
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
    }
}
