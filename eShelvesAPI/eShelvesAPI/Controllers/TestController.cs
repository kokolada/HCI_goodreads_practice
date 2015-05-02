using eShelvesAPI.DAL;
using eShelvesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace eShelvesAPI.Controllers
{
	public class TestController : ApiController
	{
		/*public HttpResponseMessage Get()
		{
			string result = "Hello world! Time is";
			var resp = new HttpResponseMessage(HttpStatusCode.OK);
			resp.Content = new StringContent(result, Encoding.UTF8, "text/plain");
			return resp;

		}*/
		MojContext ctx = new MojContext();
		public LogiraniKorisnik Get(string username, string password)
		{
			LogiraniKorisnik k = ctx.Korisnics.Where(x => x.username == username)
				.Where(y => y.password == password).Select(x => new LogiraniKorisnik
				{
					Id = x.Id,
					Ime = x.Ime,
					Prezime = x.Prezime,
					username = username,
					password = password,
					Email = x.Email,
					Policas = x.Policas.Where(t => t.KorisnikID == x.Id).Select(p => new eShelvesAPI.Controllers.LogiraniKorisnik.Police {
						Id = p.Id,
						Naziv = p.Naziv
					}).ToList()
				}).SingleOrDefault();

			return k;
		}
	}

	public class LogiraniKorisnik
	{
		public int Id { get; set; }
		public string Ime { get; set; }
		public string Prezime { get; set; }
		public string username { get; set; }
		public string password { get; set; }
		public string Email { get; set; }

		public List<Police> Policas { get; set; }

		public class Police
		{
			public int Id { get; set; }
			public string Naziv { get; set; }
		}
	}
}
