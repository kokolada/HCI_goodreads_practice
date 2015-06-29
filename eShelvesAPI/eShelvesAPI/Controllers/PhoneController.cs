using eShelvesAPI.DAL;
using eShelvesAPI.Models;
using eShelvesAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eShelvesAPI.Controllers
{
    public class PhoneController : ApiController
    {
        MojContext db = new MojContext();

        [HttpGet]
        [Route("api/Hub/{userid}")]
        public HubPageViewModel GetHubData(int userid)
        {
            HubPageViewModel defaultViewModel = new HubPageViewModel();

            defaultViewModel.BookShelves = new HubPageViewModel.ShelvesInfo();
            defaultViewModel.BookShelves.Shelves = db.Policas.Where(x => x.KorisnikID == userid).Select(y => new HubPageViewModel.ShelvesInfo.ShelfInfo
            {
                KorisnikID = y.KorisnikID,
                Naziv = y.Naziv,
                ShelfID = y.Id,
                BookCount = y.Knjigas.Count()
            }).ToList();

            defaultViewModel.Profile = new HubPageViewModel.ProfileInfo();
            defaultViewModel.Profile = db.Korisnics.Where(x => x.Id == userid).Select(z => new HubPageViewModel.ProfileInfo
            {
                KorisnikID = z.Id,
                username = z.username,
                Joined = z.created_at,
                Grad = z.Grad,
                FriendCount = z.Prijateljstvos.Count()
            }).FirstOrDefault();

            HubPageViewModel.ShelvesInfo.ShelfInfo polica = defaultViewModel.BookShelves.Shelves.Where(x => x.Naziv == "CurrentlyReading").FirstOrDefault();
            List<Knjiga> curb = db.Policas.Where(p => p.Id == polica.ShelfID).First().Knjigas;
            if (curb != null)
            {
                defaultViewModel.Profile.CurrentlyReadingBooks = curb.Select(k => new HubPageViewModel.ProfileInfo.BookInfo
                {
                    Autor = k.Autor.Ime + " " + k.Autor.Prezime,
                    KnjigaID = k.Id,
                    Naslov = k.Naslov,
                    Slika = k.Slika
                }).ToList();
            }
            else
                defaultViewModel.Profile.CurrentlyReadingBooks = null;

            Helpers.Preporuka preporuka = new Helpers.Preporuka();
            List<KnjigaVM> preporuceneKnjige = preporuka.GetPreporuceneKnjige(userid);

            defaultViewModel.Recommendations = new HubPageViewModel.RecommendationsInfo();
            defaultViewModel.Recommendations.RecommendedBooks = preporuceneKnjige.Select(k => new HubPageViewModel.RecommendationsInfo.BookInfo
            {
                Autor = k.NazivAutora,
                KnjigaID = k.Id,
                Naslov = k.Naslov,
                Slika = k.Slika
            }).ToList();

            foreach (var k in defaultViewModel.Recommendations.RecommendedBooks)
            {
                Knjiga kk = db.Knjigas.Include("Ocjenas").Where(x => x.Id == k.KnjigaID).FirstOrDefault();
                k.ProsjecnaOcjena = (float)kk.Ocjenas.Average(a => a.OcjenaIznos);
            }

            defaultViewModel.Feed = new HubPageViewModel.FeedInfo();
            defaultViewModel.Feed.FeedItems = db.TimelineItems.Select(t => new HubPageViewModel.FeedInfo.FeedItemInfo
            {
                EventDescription = t.EventDescription,
                FeedItemID = t.Id,
                Slika = t.Knjiga.Slika,
                EventInformation = "ovo treba ispraviti"
            }).ToList();

            return defaultViewModel;
        }
    }
}
