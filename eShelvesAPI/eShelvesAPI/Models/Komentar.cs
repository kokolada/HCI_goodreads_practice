using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShelvesAPI.Models
{
    public class Komentar
    {
        public int Id { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime DatumKomentiranja { get; set; }

        public int KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }

        public int OcjenaID { get; set; }
        public Ocjena Ocjena { get; set; }
    }
}