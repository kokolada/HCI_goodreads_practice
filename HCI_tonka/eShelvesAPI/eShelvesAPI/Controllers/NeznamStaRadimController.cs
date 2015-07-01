using eShelvesAPI.DAL;
using eShelvesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eShelvesAPI.Controllers
{
    public class NeznamStaRadimController : ApiController
    {
        private MojContext db = new MojContext();

        [HttpPost]
        public void AddKnjiga(Knjiga k)
        {
            Knjiga b = db.Knjigas.Find(k.Id);
            if(b != null)
            {
                b.Id = k.Id;
                b.AutorId = k.AutorId;
                b.ISBN = k.ISBN;
                b.Naslov = k.Naslov;
                b.Objavljena = k.Objavljena;
                b.Opis = k.Opis;
                if (k.Slika != null)
                    b.Slika = k.Slika;

                db.SaveChanges();

                string[] arr = k.Kategorijas.Select(y => y.Naziv).ToArray();
                List<Kategorija> kategorije = (from p in db.Kategorijas where arr.Any(x => p.Naziv.Contains(x)) select p).ToList();
                eShelvesEntities ctx = new eShelvesEntities();
                ctx.usp_RemoveKnjigaKategorijas(b.Id);
                foreach (Kategorija kat in kategorije)
                {
                    ctx.usp_addKnjigaKategorija(b.Id, kat.Id);
                }
            }
            else
            {
                string[] arr = k.Kategorijas.Select(y => y.Naziv).ToArray();
                List<Kategorija> kategorije = (from p in db.Kategorijas where arr.Any(x => p.Naziv.Contains(x)) select p).ToList();
                k.Kategorijas = kategorije;
                db.Knjigas.Add(k);
                db.SaveChanges();
            }
        }

        [HttpGet]
        [Route("api/Knjigas/Remove/{id}")]
        public IHttpActionResult DeleteKnjiga(int id)
        {
            Knjiga k = db.Knjigas.Find(id);
            if(k != null)
            {
                db.Knjigas.Remove(k);
                db.SaveChanges();

                return Ok();
            }

            return BadRequest();
        }
    }
}
