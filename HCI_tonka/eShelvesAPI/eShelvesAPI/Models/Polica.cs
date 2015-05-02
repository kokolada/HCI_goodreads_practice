using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace eShelvesAPI.Models
{
	public class Polica
	{
		public int Id { get; set; }
		public string Naziv { get; set; }

		public int KorisnikID { get; set; }
		public Korisnik Korisnik { get; set; }

		public List<Knjiga> Knjigas { get; set; }
	}
}