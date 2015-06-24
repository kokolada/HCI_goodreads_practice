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
    public class NeznamStaRadimController : ApiController
    {
        private MojContext db = new MojContext();

        [HttpPost]
        public void AddKnjiga(Knjiga k)
        {
            db.Knjigas.Add(k);
            db.SaveChanges();
        }
    }
}
