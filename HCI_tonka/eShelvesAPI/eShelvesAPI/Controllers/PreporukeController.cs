using eShelvesAPI.Helpers;
using eShelvesAPI.Models;
using eShelvesAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eShelvesAPI.Controllers
{
    public class PreporukeController : ApiController
    {
        [HttpGet]
        public List<KnjigaVM> getPreporuceneKnjige(int korisnikId)
        {
            Preporuka p = new Preporuka();
            return p.GetPreporuceneKnjige(korisnikId);
        }
    }
}