using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShelvesAPI.ViewModels
{
    public class OcjenaDetaljiViewModel
    {
        public int Id { get; set; }
        public int Ocjena { get; set; }
        public string Opis { get; set; }

        public string username { get; set; }
        public int KorisnikID { get; set; }

        public KnjigaInfo Knjiga { get; set; }

        public class KnjigaInfo
        {
            public int KnjigaID { get; set; }
            public string Naslov { get; set; }
            public string Autor { get; set; }
            public int AutorID { get; set; }
            public string ISBN { get; set; }
            public DateTime Objavljena { get; set; }
            public float ProsjecnaOcjena { get; set; }
            public byte[] Slika { get; set; }
            public string Kategorije { get; set; }
        }
    }
}