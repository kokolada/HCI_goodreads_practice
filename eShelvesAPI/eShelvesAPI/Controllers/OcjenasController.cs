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
        public KnjigaDetaljiVM GetOcjeneByKnjiga(int knjigaID)
        {
            return db.Knjigas.Where(x => x.Id == knjigaID).Select(x => new KnjigaDetaljiVM
            {
                Naslov = x.Naslov,
                ISBN = x.ISBN,
                Opis = x.Opis,
                ProsjecnaOcjena = db.Ocjenas.Where(z => z.KnjigaID == x.Id).Average(g => g.OcjenaIznos),
                AutorID = x.AutorId,
                NazivAutora = x.Autor.Ime + " " + x.Autor.Prezime,
                OcjenaInfoVMs = db.Ocjenas.Where(v => v.KnjigaID == knjigaID).Select(b => new eShelvesAPI.ViewModels.KnjigaDetaljiVM.OcjenaInfoVM
                {
                    KorisnikId = b.KorisnikID,
                    Ocjena = b.OcjenaIznos,
                    Opis = b.Opis,
                    username = b.Korisnik.username
                }).ToList()
            }).Single();
        }

        [HttpGet]
        [Route("api/Ocjenas/{knjigaid}")]
        public List<KnjigaDetaljiVM.OcjenaInfoVM> GetOcjeneKnjige(int knjigaid)
        {
            return db.Ocjenas.Where(v => v.KnjigaID == knjigaid).Select(b => new eShelvesAPI.ViewModels.KnjigaDetaljiVM.OcjenaInfoVM
            {
                KorisnikId = b.KorisnikID,
                Ocjena = b.OcjenaIznos,
                Opis = b.Opis,
                username = b.Korisnik.username
            }).ToList();
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

       /* // POST: api/Ocjenas
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
        }*/

        [HttpPost]
        public OcjenaVM PostOcjena(OcjenaVM ocjena)
        {
            Ocjena o = db.Ocjenas.Where(x => x.KorisnikID == ocjena.KorisnikID && x.KnjigaID == ocjena.KnjigaID).First();
            if (o != null)
                o.OcjenaIznos = ocjena.Ocjena;
            else
            {
                o = new Ocjena();
                o.KnjigaID = ocjena.KnjigaID;
                o.KorisnikID = ocjena.KorisnikID;
                o.OcjenaIznos = ocjena.Ocjena;
                o.Opis = "asd";
                o.DatumOcjene = DateTime.Now;
                db.Ocjenas.Add(o);
                
                TimelineItem timeline = new TimelineItem();
                timeline.EventDate = DateTime.Now;
                timeline.IsOcjena = true;
                timeline.KnjigaID = ocjena.KnjigaID;
                timeline.KorisnikID = ocjena.KorisnikID;
                timeline.EventDescription = " je ocjenio ";
                db.TimelineItems.Add(timeline);
            }

            db.SaveChanges();

            return ocjena;
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