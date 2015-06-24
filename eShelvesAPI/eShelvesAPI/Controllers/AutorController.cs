using eShelvesAPI.DAL;
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
    }

    public class AutorVM
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
    }
}
