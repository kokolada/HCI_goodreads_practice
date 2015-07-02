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
                NazivAutora = x.Autor.Ime + " " + x.Autor.Prezime,
                Slika = x.Slika
            }).Distinct().ToList());

            return lista;
        }
        [HttpGet]
        [Route("api/Search/{query}")]
        public List<KnjigaVM> GetKnjigeByParamsDesktopTest(string query)
        {
            var tokens = query.Split(' ');
            List<KnjigaVM> lista = new List<KnjigaVM>();
            lista.AddRange(db.Knjigas.Where(x => tokens.All(t => x.Naslov.Contains(t)) || tokens.All(t => (x.Autor.Ime + " " + x.Autor.Prezime).Contains(t))).Select(x => new KnjigaVM
            {
                Id = x.Id,
                AutorId = x.AutorId,
                ISBN = x.ISBN,
                Naslov = x.Naslov,
                NazivAutora = x.Autor.Ime + " " + x.Autor.Prezime,
                Slika = x.Slika
            }).Distinct().ToList());

            return lista;
        }

        [HttpGet]
        [Route("api/Search/")]
        public List<KnjigaVM> GetAllKnjige()
        {
            List<KnjigaVM> lista = new List<KnjigaVM>();
            lista.AddRange(db.Knjigas.Select(x => new KnjigaVM
            {
                Id = x.Id,
                AutorId = x.AutorId,
                ISBN = x.ISBN,
                Naslov = x.Naslov,
                NazivAutora = x.Autor.Ime + " " + x.Autor.Prezime,
                Slika = x.Slika
            }).Distinct().ToList());

            return lista;
        }
    }
}