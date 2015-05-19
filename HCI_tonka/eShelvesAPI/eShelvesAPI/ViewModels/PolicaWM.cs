using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShelvesAPI.ViewModels
{
    public class PolicaWM
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int BookCount { get; set; }
        public int KorisnikID { get; set; }
    }
}