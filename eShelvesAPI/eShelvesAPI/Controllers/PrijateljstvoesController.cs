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
using eShelvesAPI.ViewModels;

namespace eShelvesAPI.Controllers
{
    public class PrijateljstvoesController : ApiController
    {
        private MojContext db = new MojContext();

        // GET: api/Prijateljstvoes
        public IQueryable<Prijateljstvo> GetPrijateljstvos()
        {
            return db.Prijateljstvos;
        }

        [HttpGet]
        public List<PrijateljVM> GetPrijateljstvosByUserId(int userId)
        {
            return db.Prijateljstvos.Where(x => x.Korisnik1ID == userId).Select(x => new PrijateljVM
            {
                PrijateljID = x.Korisnik2ID,
                username = x.Korisnik2.username,
                imeprezime = x.Korisnik2.Ime + " " + x.Korisnik2.Prezime,
                joined = x.Korisnik2.created_at.Day + "." + x.Korisnik2.created_at.Month + "." + x.Korisnik2.created_at.Year
            }).ToList();
        }

        // GET: api/Prijateljstvoes/5
        [ResponseType(typeof(Prijateljstvo))]
        public IHttpActionResult GetPrijateljstvo(int id)
        {
            Prijateljstvo prijateljstvo = db.Prijateljstvos.Find(id);
            if (prijateljstvo == null)
            {
                return NotFound();
            }

            return Ok(prijateljstvo);
        }

        // PUT: api/Prijateljstvoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPrijateljstvo(int id, Prijateljstvo prijateljstvo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prijateljstvo.Korisnik1ID)
            {
                return BadRequest();
            }

            db.Entry(prijateljstvo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrijateljstvoExists(id))
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

        // POST: api/Prijateljstvoes
        [ResponseType(typeof(Prijateljstvo))]
        public IHttpActionResult PostPrijateljstvo(Prijateljstvo prijateljstvo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Prijateljstvos.Add(prijateljstvo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PrijateljstvoExists(prijateljstvo.Korisnik1ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = prijateljstvo.Korisnik1ID }, prijateljstvo);
        }

        // DELETE: api/Prijateljstvoes/5
        [ResponseType(typeof(Prijateljstvo))]
        public IHttpActionResult DeletePrijateljstvo(int id)
        {
            Prijateljstvo prijateljstvo = db.Prijateljstvos.Find(id);
            if (prijateljstvo == null)
            {
                return NotFound();
            }

            db.Prijateljstvos.Remove(prijateljstvo);
            db.SaveChanges();

            return Ok(prijateljstvo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PrijateljstvoExists(int id)
        {
            return db.Prijateljstvos.Count(e => e.Korisnik1ID == id) > 0;
        }
    }
}