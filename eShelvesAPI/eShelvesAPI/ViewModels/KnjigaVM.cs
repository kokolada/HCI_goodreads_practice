using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShelvesAPI.ViewModels
{
    public class KnjigaVM
    {
        public int Id { get; set; }
        public string Naslov { get; set; }
        public string ISBN { get; set; }
        public int AutorId { get; set; }
        public string NazivAutora { get; set; }
        public byte[] Slika { get; set; }
    }
}