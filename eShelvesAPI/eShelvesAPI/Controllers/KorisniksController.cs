using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using eShelvesAPI.DAL;
using eShelvesAPI.Models;
using eShelvesAPI.Helpers;

namespace eShelvesAPI.Controllers
{
    public class KorisniksController : ApiController
    {
        private MojContext db = new MojContext();

        // GET: api/Korisniks
        public IQueryable<Korisnik> GetKorisnics()
        {
            return db.Korisnics;
        }

        // GET: api/Korisniks/5
        [ResponseType(typeof(Korisnik))]
        public IHttpActionResult GetKorisnik(int id)
        {
            Korisnik korisnik = db.Korisnics.Find(id);
            if (korisnik == null)
            {
                return NotFound();
            }

            return Ok(korisnik);
        }

        [HttpGet]
        [Route("api/Korisniks/SearchKorisnici/{username}")]
        public List<Korisnik> SearchKorisnici(string username)
        {
            var tokens = username.Split(' ');
            return db.Korisnics.Where(x => tokens.All(t => x.username.Contains(t))).ToList();
        }
        [HttpGet]
        [Route("api/Korisniks/SearchKorisnici/")]
        public List<Korisnik> SearchKorisnici()
        {
            return db.Korisnics.ToList();
        }

        // PUT: api/Korisniks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKorisnik(int id, Korisnik korisnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != korisnik.Id)
            {
                return BadRequest();
            }

            db.Entry(korisnik).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KorisnikExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Korisniks
        [ResponseType(typeof(Korisnik))]
        public IHttpActionResult PostKorisnik(Korisnik korisnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (korisnik.Id > 0)
            {
                Korisnik k = db.Korisnics.Find(korisnik.Id);
                k.Grad = korisnik.Grad;
                k.created_at = korisnik.created_at;
                k.Email = korisnik.Email;
                k.Ime = korisnik.Ime;
                k.Prezime = korisnik.Prezime;
                k.Spol = korisnik.Spol;
                db.SaveChanges();
            }
            else
            {
                korisnik.password = KorisniciHelper.GenerateHash(korisnik.password, "nema");
                db.Korisnics.Add(korisnik);
                db.SaveChanges();
            }

            return CreatedAtRoute("DefaultApi", new { id = korisnik.Id }, korisnik);
        }

        // DELETE: api/Korisniks/5
        [ResponseType(typeof(Korisnik))]
        public IHttpActionResult DeleteKorisnik(int id)
        {
            Korisnik korisnik = db.Korisnics.Find(id);
            if (korisnik == null)
            {
                return NotFound();
            }

            db.Korisnics.Remove(korisnik);
            db.SaveChanges();

            return Ok(korisnik);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KorisnikExists(int id)
        {
            return db.Korisnics.Count(e => e.Id == id) > 0;
        }
    }
}