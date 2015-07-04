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
    public class DesktopController : ApiController
    {
        MojContext db = new MojContext();

        [HttpGet]
        [Route("api/Desktop/RemoveKnjiga/{policaid}/{knjigaid}")]
        public bool RemoveKnjiga(int policaid, int knjigaid)
        {
            Polica p = db.Policas.Include("Knjigas").Where(x => x.Id == policaid).FirstOrDefault();
            Knjiga k = db.Knjigas.Find(knjigaid);

            p.Knjigas.Remove(k);

            db.SaveChanges();

            return true;
        }

        [HttpGet]
        [Route("api/Desktop/AddKnjigaToPolica/{policaid}/{knjigaid}")]
        public bool AddKnjigaToPolica(int policaid, int knjigaid)
        {
            Polica p = db.Policas.Include("Knjigas").Where(x => x.Id == policaid).FirstOrDefault();
            Knjiga k = db.Knjigas.Find(knjigaid);

            p.Knjigas.Add(k);

            db.SaveChanges();

            return true;
        }

        [HttpGet]
        [Route("api/Desktop/Kategorijas")]
        public List<Kategorija> GetKategorijas()
        {
            return db.Kategorijas.ToList();
        }

        [HttpGet]
        [Route("api/Desktop/Kategorijas/Remove/{id}")]
        public IHttpActionResult RemoveKategorijaById(int id)
        {
            Kategorija k = db.Kategorijas.Find(id);
            if(k != null)
            {
                db.Kategorijas.Remove(k);
                db.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("api/Desktop")]
        public Kategorija PostKategorija(Kategorija k)
        {
            db.Kategorijas.Add(k);
            db.SaveChanges();

            return k;
        }

        [HttpGet]
        [Route("api/Username/{username}")]
        public bool IsUnique(string username)
        {
            return !(db.Korisnics.Where(k => k.username == username).Count() > 0);
        }
    }
}
