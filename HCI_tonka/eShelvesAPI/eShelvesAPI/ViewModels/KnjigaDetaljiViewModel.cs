using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShelvesAPI.ViewModels
{
    public class KnjigaDetaljiViewModel
    {
        public int KnjigaID { get; set; }
        public string Naslov { get; set; }
        public string Autor { get; set; }
        public int AutorID { get; set; }
        public string ISBN { get; set; }
        public DateTime Objavljena { get; set; }
        public float? ProsjecnaOcjena { get; set; }
        public int? OcjenaLogiranogKorisnika { get; set; }
        public byte[] Slika { get; set; }

        public List<OcjenaInfo> Ocjene { get; set; }
        //naknadno skontat kako rijesit one akcije, u kojoj je polici i to
        public class OcjenaInfo
        {
            public int OcjenaID { get; set; }
            public string username { get; set; }
            public int Ocjena { get; set; }
            public string Opis { get; set; }
        }
    }
}