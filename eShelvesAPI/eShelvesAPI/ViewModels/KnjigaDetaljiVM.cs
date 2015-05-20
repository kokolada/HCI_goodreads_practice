using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShelvesAPI.ViewModels
{
    public class KnjigaDetaljiVM
    {
        public string Naslov { get; set; }
        public int AutorID { get; set; }
        public string NazivAutora { get; set; }
        public double ProsjecnaOcjena { get; set; }
        public string ISBN { get; set; }
        public string Opis { get; set; }

        public List<OcjenaInfoVM> OcjenaInfoVMs { get; set; }

        public class OcjenaInfoVM
        {

            public int KorisnikId { get; set; }
            public string username { get; set; }
            public int Ocjena { get; set; }
            public string Opis { get; set; }
        }
    }
}