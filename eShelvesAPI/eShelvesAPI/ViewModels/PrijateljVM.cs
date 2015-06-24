using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShelvesAPI.ViewModels
{
    public class PrijateljVM
    {
        public int PrijateljID { get; set; }
        public string username { get; set; }
        public string imeprezime { get; set; }
        public string joined { get; set; }
    }
}