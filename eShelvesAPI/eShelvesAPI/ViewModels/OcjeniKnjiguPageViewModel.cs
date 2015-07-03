using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShelvesAPI.ViewModels
{
    public class OcjeniKnjiguPageViewModel
    {
        public int OcjenaID { get; set; }
        public int OcjenaIznos { get; set; }
        public string Opis { get; set; }

        public int KorisnikID { get; set; }

        public int KnjigaID { get; set; }
        public string Naslov { get; set; }
    }
}