using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShelvesAPI.ViewModels
{
    public class TimelineItemVM
    {
        public int Id { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        public bool IsOcjena { get; set; }
        public int Ocjena { get; set; }

        public int KnjigaId { get; set; }
        public string Naslov { get; set; }

        public int KorisnikId { get; set; }
        public string username { get; set; }
    }
}