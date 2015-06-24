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
    }
}
