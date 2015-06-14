using eShelvesAPI.DAL;
using eShelvesAPI.ViewModels;
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
        public List<TimelineItemVM> GetTimelineItems(int korisnikId)
        {
            return db.TimelineItems.Select(x => new TimelineItemVM
            {
                EventDate = x.EventDate,
                EventDescription = x.EventDescription,
                Id = x.Id,
                IsOcjena = x.IsOcjena,
                KnjigaId = x.KnjigaID,
                KorisnikId = x.KorisnikID,
                Naslov = x.Knjiga.Naslov,
                Ocjena = db.Ocjenas.Where(z => z.KnjigaID == x.KnjigaID && z.KorisnikID == x.KorisnikID).FirstOrDefault().OcjenaIznos,
                username = x.Korisnik.username
            }).ToList();
        }
    }
}
