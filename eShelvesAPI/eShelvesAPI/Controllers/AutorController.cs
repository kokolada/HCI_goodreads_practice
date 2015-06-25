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
        public Autor GetAutor(int id)
        {
            return db.Autors.Find(id);
        }

        [HttpGet]
        [Route("api/Autor/Search/{query?}")]
        public List<Autor> GetAutorsByName(string query)
        {
            return db.Autors.Where(x => (x.Ime + " " + x.Prezime).Contains(query) || query=="").ToList();
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
            db.Autors.Add(a);
            db.SaveChanges();

            return a;
        }
    }

    public class AutorVM
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
    }
}
