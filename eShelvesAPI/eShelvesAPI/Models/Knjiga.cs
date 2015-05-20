using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace eShelvesAPI.Models
{
	public class Knjiga
	{
		public int Id { get; set; }
		public string Naslov { get; set; }
		public string Opis { get; set; }
		public string ISBN { get; set; }
		public DateTime Objavljena { get; set; }

		public int AutorId { get; set; }
		public Autor Autor { get; set; }

		public List<Ocjena> Ocjenas { get; set; }
		public List<Polica> Policas { get; set; }
		public List<Kategorija> Kategorijas { get; set; }
		//slika
	}
}