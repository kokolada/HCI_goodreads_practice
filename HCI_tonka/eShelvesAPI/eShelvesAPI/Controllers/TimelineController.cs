using eShelvesAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eShelvesAPI.Controllers
{
    public class TimelineController : ApiController
    {
        MojContext db = new MojContext();

        [HttpGet]
        public void GetTimelineItems(int korisnikId)
        {
            //vratit listu timeline itema za tog korisnikatttttTONItest
        }
    }
}
