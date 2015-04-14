using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eShelvesAPI.Models
{
	public class Prijateljstvo
	{
		[Key, Column(Order = 0)]
		public int Korisnik1ID { get; set; }
		[Key, Column(Order = 1)]
		public int Korisnik2ID { get; set; }
		public int Status { get; set; }
		public int UputioZahtjevID { get; set; }

		public Korisnik Korisnik1 { get; set; }
		public Korisnik Korisnik2 { get; set; }
		//vidjet kako za Status koristik helper enumeraciju da ne stoji ruzni brojevi
		//(tipa StatusType.Blocked, StatusType.Pending i tako)
	}
}