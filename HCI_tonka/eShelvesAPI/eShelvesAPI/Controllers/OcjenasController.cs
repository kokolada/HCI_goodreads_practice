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

namespace eShelvesAPI.Controllers
{
    public class OcjenasController : ApiController
    {
        private MojContext db = new MojContext();

        // GET: api/Ocjenas
        public IQueryable<Ocjena> GetOcjenas()
        {
            return db.Ocjenas;
        }

        // GET: api/Ocjenas/5
        [ResponseType(typeof(Ocjena))]
        public IHttpActionResult GetOcjena(int id)
        {
            Ocjena ocjena = db.Ocjenas.Find(id);
            if (ocjena == null)
            {
                return NotFound();
            }

            return Ok(ocjena);
        }

        [HttpGet]
        public List<Ocjena> GetOcjeneByKnjiga(int knjigaID)
        {
            return db.Ocjenas.Where(x => x.KnjigaID == knjigaID).Include("Korisnik").Include("Knjiga").ToList();
        }

        // PUT: api/Ocjenas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOcjena(int id, Ocjena ocjena)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ocjena.Id)
            {
                return BadRequest();
            }

            db.Entry(ocjena).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OcjenaExists(id))
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

        // POST: api/Ocjenas
        [ResponseType(typeof(Ocjena))]
        public IHttpActionResult PostOcjena(Ocjena ocjena)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ocjenas.Add(ocjena);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ocjena.Id }, ocjena);
        }

        // DELETE: api/Ocjenas/5
        [ResponseType(typeof(Ocjena))]
        public IHttpActionResult DeleteOcjena(int id)
        {
            Ocjena ocjena = db.Ocjenas.Find(id);
            if (ocjena == null)
            {
                return NotFound();
            }

            db.Ocjenas.Remove(ocjena);
            db.SaveChanges();

            return Ok(ocjena);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OcjenaExists(int id)
        {
            return db.Ocjenas.Count(e => e.Id == id) > 0;
        }
    }
}