using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShelvesAPI.ViewModels
{
    public class AutorPageViewModel
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public DateTime Rodjen { get; set; }
        public string Grad { get; set; }
        public string WebStranica { get; set; }
        public string Opis { get; set; }
        public string Kategorije { get; set; }

        public List<KnjigaInfo> Knjige { get; set; }

        public class KnjigaInfo
        {
            public int KnjigaID { get; set; }
            public string Naslov { get; set; }
            public float? ProsjecnaOcjena { get; set; }
            public byte[] Slika { get; set; }
        }
    }
}