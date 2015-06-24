using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShelvesAPI.Models
{
    public class NarudzbeReklama
    {
        public int Id { get; set; }
        public int DanaZakupljeno { get; set; }
        public float Cijena { get; set; }
        public int BrojPrikaza { get; set; }

        public int ReklamaID { get; set; }
        public Reklama Reklama { get; set; }

        public int KupacID { get; set; }
        public Kupac Kupac { get; set; }
    }
}