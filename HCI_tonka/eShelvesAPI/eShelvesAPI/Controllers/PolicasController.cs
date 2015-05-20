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
	public class PolicasController : ApiController
	{
		private MojContext db = new MojContext();

		// GET: api/Policas
		public IQueryable<Polica> GetPolicas()
		{
			return db.Policas;
		}

		// GET: api/Policas/5
		[ResponseType(typeof(Polica))]
		public IHttpActionResult GetPolica(int id)
		{
			Polica polica = db.Policas.Find(id);
			if (polica == null)
			{
				return NotFound();
			}

			return Ok(polica);
		}

		[HttpGet]
		public List<PolicaWM> GetPolicaByUserId(int korisnikId)
		{
            return db.Policas.Where(x => x.KorisnikID == korisnikId).Select(x => new PolicaWM
            {
                Id = x.Id,
                Naziv = x.Naziv,
                BookCount = x.Knjigas.Count()
            }).ToList();
		}

		// PUT: api/Policas/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutPolica(int id, Polica polica)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != polica.Id)
			{
				return BadRequest();
			}

			db.Entry(polica).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!PolicaExists(id))
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

		// POST: api/Policas
		/*[ResponseType(typeof(PolicaWM))]
		public IHttpActionResult PostPolica(PolicaWM polica)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Policas.Add(polica);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = polica.Id }, polica);
		}
        */
        public PolicaWM PostPolica(PolicaWM polica)
        {
            Polica p = new Polica();
            p.KorisnikID = polica.KorisnikID;
            p.Naziv = polica.Naziv;

            db.Policas.Add(p);
            db.SaveChanges();

            return polica;
        }

		// DELETE: api/Policas/5
		[ResponseType(typeof(Polica))]
		public IHttpActionResult DeletePolica(int id)
		{
			Polica polica = db.Policas.Find(id);
			if (polica == null)
			{
				return NotFound();
			}

			db.Policas.Remove(polica);
			db.SaveChanges();

			return Ok(polica);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool PolicaExists(int id)
		{
			return db.Policas.Count(e => e.Id == id) > 0;
		}
	}
}