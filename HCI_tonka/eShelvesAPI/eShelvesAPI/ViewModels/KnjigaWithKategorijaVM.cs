using eShelvesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShelvesAPI.ViewModels
{
    public class KnjigaWithKategorijaVM
    {
        public int Id { get; set; }
        public string Naslov { get; set; }
        public string Opis { get; set; }
        public string ISBN { get; set; }
        public DateTime Objavljena { get; set; }
        public byte[] Slika { get; set; }

        public int AutorId { get; set; }
        public List<KategorijaInfo> Kategorijas { get; set; }

        public class KategorijaInfo
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
        }
    }
}