using eShelvesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShelvesAPI.ViewModels
{
    public class AutorVMM
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime Rodjen { get; set; }
        public DateTime? Umro { get; set; }
        public string MjestoRodjenja { get; set; }
        public string WebStranica { get; set; }
        public string Opis { get; set; }

        public List<Kategorija> Kategorijas { get; set; }
    }
}