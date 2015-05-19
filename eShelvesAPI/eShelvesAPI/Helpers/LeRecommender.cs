using eShelvesAPI.DAL;
using eShelvesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShelvesAPI.Helpers
{
    public class LeRecommender
    {
        private MojContext db = new MojContext();

        public void GetSharedRatings()
        {
            List<Knjiga> knjige = db.Knjigas.ToList();

            foreach(Knjiga k in knjige)
            {
                List<Ocjena> ocjeneKnjige = db.Ocjenas.Where(x => x.KnjigaID == k.Id).ToList();

            }
        }
    }
}