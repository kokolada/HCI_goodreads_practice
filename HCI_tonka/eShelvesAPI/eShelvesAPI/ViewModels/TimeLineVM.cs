using eShelvesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShelvesAPI.ViewModels
{
	public class TimeLineVM
	{
		class TimeLineItemInfo
		{
			public string ImeiPrezime { get; set; }
			public string NaslovKnjige { get; set; }
			public string nazivAkcije { get; set; }
			public string ImeAutora { get; set; }
		}

		//public List<TimeLineItemInfo> Itemi { get; set; }
	}
}