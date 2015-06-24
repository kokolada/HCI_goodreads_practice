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
	public class KnjigasController : ApiController
	{
		private MojContext db = new MojContext();

		// GET: api/Knjigas
		public List<KnjigaWithKategorijaVM> GetKnjigas()
		{
            return db.Knjigas.Select(x => new KnjigaWithKategorijaVM()
            {
                Id = x.Id,
                AutorId = x.AutorId,
                ISBN = x.ISBN,
                Kategorijas = x.Kategorijas.Select(z => new KnjigaWithKategorijaVM.KategorijaInfo()
                {
                    Id = z.Id,
                    Naziv = z.Naziv
                }).ToList(),
                Naslov = x.Naslov,
                Objavljena = x.Objavljena,
                Opis = x.Opis,
                Slika = x.Slika
            }).ToList();
		}

        [HttpGet]
        public KnjigaWithKategorijaVM GetKnjigas(int id)
        {
            return db.Knjigas.Select(x => new KnjigaWithKategorijaVM()
            {
                Id = x.Id,
                AutorId = x.AutorId,
                ISBN = x.ISBN,
                Kategorijas = x.Kategorijas.Select(z => new KnjigaWithKategorijaVM.KategorijaInfo()
                {
                    Id = z.Id,
                    Naziv = z.Naziv
                }).ToList(),
                Naslov = x.Naslov,
                Objavljena = x.Objavljena,
                Opis = x.Opis,
                Slika = x.Slika
            }).Where(x => x.Id == id).First();
        }

        [HttpGet]
		public List<Knjiga> GetKnjigasByShelf(int PolicaID)
		{
			return db.Policas.Where(x => x.Id == PolicaID).Select(x => x.Knjigas).FirstOrDefault();
		}
 
		// PUT: api/Knjigas/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutKnjiga(int id, Knjiga knjiga)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != knjiga.Id)
			{
				return BadRequest();
			}

			db.Entry(knjiga).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!KnjigaExists(id))
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

		// POST: api/Knjigas
		/*[ResponseType(typeof(Knjiga))]
		public IHttpActionResult PostKnjiga(Knjiga knjiga)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Knjigas.Add(knjiga);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = knjiga.Id }, knjiga);
		}*/

        [HttpPost]
        public KnjigaPolicaVM PostKnjiga(KnjigaPolicaVM kp)
        {
            Knjiga k = db.Knjigas.Find(kp.knjigaId);
            Polica p = db.Policas.Find(kp.policaId);
            p.Knjigas.Add(k);
            db.Policas.Add(p);

            TimelineItem ti = new TimelineItem();
            ti.EventDate = DateTime.Now;
            ti.EventDescription = "je dodao u policu";
            ti.IsOcjena = false;
            ti.KnjigaID = kp.knjigaId;
            ti.KorisnikID = kp.korisnikId;
            db.TimelineItems.Add(ti);

            db.SaveChanges();

            return kp;
        }

		// DELETE: api/Knjigas/5
		[ResponseType(typeof(Knjiga))]
		public IHttpActionResult DeleteKnjiga(int id)
		{
			Knjiga knjiga = db.Knjigas.Find(id);
			if (knjiga == null)
			{
				return NotFound();
			}

			db.Knjigas.Remove(knjiga);
			db.SaveChanges();

			return Ok(knjiga);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool KnjigaExists(int id)
		{
			return db.Knjigas.Count(e => e.Id == id) > 0;
		}
	}
}