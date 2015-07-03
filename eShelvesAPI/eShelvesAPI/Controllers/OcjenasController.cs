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

        // GET: api/Ocjenas/Ocjena/5
        [HttpGet]
        [Route("api/Ocjenas/Ocjena/{id}")]
        public OcjenaDetaljiViewModel GetOcjena(int id)
        {
            return db.Ocjenas.Where(x => x.Id == id).Select(o => new OcjenaDetaljiViewModel
            {
                Id = o.Id,
                KorisnikID = o.KorisnikID,
                Ocjena = o.OcjenaIznos,
                Opis = o.Opis,
                username = o.Korisnik.username,
                Knjiga = new OcjenaDetaljiViewModel.KnjigaInfo
                {
                    AutorID = o.Knjiga.AutorId,
                    Autor = o.Knjiga.Autor.Ime + " " + o.Knjiga.Autor.Prezime,
                    ISBN = o.Knjiga.ISBN,
                    KnjigaID = o.KnjigaID,
                    Naslov = o.Knjiga.Naslov,
                    Objavljena = o.Knjiga.Objavljena,
                    ProsjecnaOcjena = (float)o.Knjiga.Ocjenas.Average(t => t.OcjenaIznos),
                    Slika = o.Knjiga.Slika
                }
            }).FirstOrDefault();
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
                username = b.Korisnik.username,
                OcjenaID = b.Id
            }).ToList();
        }
        [HttpGet]
        [Route("api/Ocjenas/user/{userid}")]
        public List<KnjigaDetaljiVM.OcjenaInfoVM> GetOcjeneUsera(int userid)
        {
            return db.Ocjenas.Where(v => v.KorisnikID == userid).Select(b => new eShelvesAPI.ViewModels.KnjigaDetaljiVM.OcjenaInfoVM
            {
                KorisnikId = b.KorisnikID,
                Ocjena = b.OcjenaIznos,
                Opis = b.Opis,
                username = b.Korisnik.username,
                OcjenaID = b.Id
            }).ToList();
        }

        [HttpGet]
        [Route("api/Ocjenas/remove/{id}")]
        public IHttpActionResult RemoveOcjena(int id)
        {
            Ocjena o = db.Ocjenas.Find(id);
            if (o != null)
            {
                db.Ocjenas.Remove(o);
                db.SaveChanges();

                return Ok();
            }

            return BadRequest();
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

        [HttpGet]
        [Route("api/Ocjenass/{knjigaid}/{userid}")]
        public OcjeniKnjiguPageViewModel GetOcjenaWP(int knjigaid, int userid)
        {
            OcjeniKnjiguPageViewModel model = db.Ocjenas.Include("Knjiga").Where(x => x.KorisnikID == userid && x.KnjigaID == knjigaid).Select(o => new OcjeniKnjiguPageViewModel
            {
                KnjigaID = o.KnjigaID,
                Naslov = o.Knjiga.Naslov,
                OcjenaID = o.Id,
                OcjenaIznos = o.OcjenaIznos,
                Opis = o.Opis,
                KorisnikID = o.KorisnikID
            }).FirstOrDefault();

            if (model == null)
            {
                model = new OcjeniKnjiguPageViewModel();
                model.KnjigaID = knjigaid;
                model.KorisnikID = userid;
                Knjiga k = db.Knjigas.Find(knjigaid);
                model.Naslov = k.Naslov;
            }

            return model;
        }

        [HttpPost]
        [Route("api/Ocjenass")]
        public void PostOcjenaWP(Ocjena o)
        {
            if (o.Id > 0)
            {
                Ocjena original = db.Ocjenas.Find(o.Id);
                original.DatumOcjene = DateTime.Now;
                original.OcjenaIznos = o.OcjenaIznos;
                original.Opis = o.Opis;

                db.SaveChanges();
            }
            else
            {
                o.DatumOcjene = DateTime.Now;
                db.Ocjenas.Add(o);
                db.SaveChanges();

                TimelineItem t = new TimelineItem();
                t.EventDate = DateTime.Now;
                t.EventDescription = " je ocjenio ";
                t.IsOcjena = true;
                t.KnjigaID = o.KnjigaID;
                t.KorisnikID = o.KorisnikID;

                db.TimelineItems.Add(t);
                db.SaveChanges();
            }
        }

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