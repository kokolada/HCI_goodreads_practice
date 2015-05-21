using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShelvesAPI.ViewModels
{
    public class TimelineItemVM
    {
        public int Id { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }

        public KorisnikInfo Korisnik { get; set; }
        public KnjigaInfo Knjiga { get; set; }
        public AutorInfo Autor { get; set; }
        public OcjenaInfo Ocjena { get; set; }


        public class KorisnikInfo
        {
            public int Id { get; set; }
            public string Username { get; set; }
        }

        public class KnjigaInfo
        {
            public int Id { get; set; }
            public string Naslov { get; set; }
        }

        public class AutorInfo 
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
        }

        public class OcjenaInfo
        {
            public int Id { get; set; }
            public int OcjenaIznos { get; set; }
            public string Opis { get; set; }
        }

    }
}