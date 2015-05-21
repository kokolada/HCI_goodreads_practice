using eShelvesAPI.DAL;
using eShelvesAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eShelvesAPI.Controllers
{
    public class ProfileController : ApiController
    {
        private MojContext db = new MojContext();

        [HttpGet]
        public UserProfileVM GetProfile(int userID)
        {
            return db.Korisnics.Where(x => x.Id == userID).Select(y => new UserProfileVM
            {
                Id = y.Id,
                Username = y.username,
                Grad = y.Grad,
                Joined = y.created_at,
                FriendCount = y.Prijateljstvos.Count(),
                Police = db.Policas.Where(z => z.KorisnikID == y.Id).Select(g => new UserProfileVM.PolicaInfo 
                {
                    Id = g.Id,
                    Naziv = g.Naziv,
                    BookCount = g.Knjigas.Count()
                }).ToList()
            }).Single();
        }
    }
}
