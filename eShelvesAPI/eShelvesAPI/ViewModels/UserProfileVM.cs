using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShelvesAPI.ViewModels
{
    public class UserProfileVM
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Grad { get; set; }
        public DateTime Joined { get; set; }
        public int FriendCount { get; set; }

        public List<PolicaInfo> Police { get; set; }

        public class PolicaInfo
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
            public int BookCount { get; set; }
        }
    }
}