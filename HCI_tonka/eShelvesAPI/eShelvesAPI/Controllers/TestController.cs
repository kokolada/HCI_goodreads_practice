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
		public Korisnik Get(string username, string password)
		{
			if (username == "dzemo" && password == "gigant")
			{
				ctx.Korisnics.ToList();
				return new Korisnik
				{
					Id = 1,
					Ime = "Dzemo",
					Prezime = "Gigant",
					username = username,
					password = password
				};
			}


			return null;
		}
	}
}
