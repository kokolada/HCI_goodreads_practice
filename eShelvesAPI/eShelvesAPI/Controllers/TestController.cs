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
		public HttpResponseMessage Get()
		{
			string result = "Hello world! Time is";
			var resp = new HttpResponseMessage(HttpStatusCode.OK);
			resp.Content = new StringContent(result, Encoding.UTF8, "text/plain");
			return resp;
		}
	}
}
