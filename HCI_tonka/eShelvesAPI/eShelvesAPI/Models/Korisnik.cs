using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShelvesAPI.Models
{
	public class Korisnik
	{
		public int Id { get; set; }
		public string Ime { get; set; }
		public string Prezime { get; set; }
		public string username { get; set; }
		public string password { get; set; }
		public string Email { get; set; }
		public string Spol { get; set; }
		public string Grad { get; set; }
		public DateTime created_at { get; set; }

		public List<Polica> Policas { get; set; }
		public List<Ocjena> Ocjenas { get; set; }
		public List<Prijateljstvo> Prijateljstvos { get; set; }
		//slika
	}
}