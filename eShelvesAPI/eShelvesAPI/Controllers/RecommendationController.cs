using eShelvesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eShelvesAPI.Controllers
{
    public class RecommendationController : ApiController
    {
        [HttpGet]
        public List<Knjiga> GetSlicneProizvode(int id)
        {
            Helpers.Preporuka preporuka = new Helpers.Preporuka();

            return preporuka.GetSlicneKnjige(id);
        }
    }
}