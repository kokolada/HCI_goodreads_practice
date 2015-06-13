using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShelvesAPI.Models
{
    public class TimelineItem
    {
        public int Id { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        public bool IsOcjena { get; set; }

        public int KnjigaID { get; set; }
        public Knjiga Knjiga { get; set; }

        public int KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }

    }
}