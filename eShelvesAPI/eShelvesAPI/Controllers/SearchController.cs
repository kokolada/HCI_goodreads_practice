using eShelvesAPI.DAL;
using eShelvesAPI.Models;
using eShelvesAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace eShelvesAPI.Controllers
{
    public class SearchController : ApiController
    {
        private MojContext db = new MojContext();

        [HttpGet]
        public List<KnjigaVM> GetKnjigeByParams(string query)
        {
            List<KnjigaVM> lista = new List<KnjigaVM>();
            lista.AddRange(db.Knjigas.Where(x => (x.Naslov + " " + x.Opis).Contains(query) || (x.Autor.Ime + " " + x.Autor.Prezime).Contains(query)).Select( x => new KnjigaVM 
            { 
                Id = x.Id,
                AutorId = x.AutorId,
                ISBN = x.ISBN,
                Naslov = x.Naslov,
                NazivAutora = x.Autor.Ime + " " + x.Autor.Prezime
            }).Distinct().ToList());

            return lista;
        }
    }
}