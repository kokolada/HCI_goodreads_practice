using eShelvesAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eShelvesAPI.Controllers
{
    public class ImagesController : ApiController
    {
        MojContext db = new MojContext();
        
        [HttpGet]
        public byte[] GetImage(int knjigaId)
        {
            return db.Knjigas.Where(x=>x.Id == knjigaId).Select(x => x.Slika).FirstOrDefault();
        }
    }
}
