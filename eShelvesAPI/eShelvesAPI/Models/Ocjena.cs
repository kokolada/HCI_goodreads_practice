using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShelvesAPI.Models
{
	public class Ocjena
	{
		public int Id { get; set; }
		public int OcjenaIznos { get; set; }
		public string Opis { get; set; }

		public int KnjigaID { get; set; }
		public Knjiga Knjiga { get; set; }

		public int KorisnikID { get; set; }
		public Korisnik Korisnik { get; set; }

		public DateTime? DatumOcjene { get; set; }
	}
}