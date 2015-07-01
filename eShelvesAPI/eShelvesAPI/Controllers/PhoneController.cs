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
            Polica p = db.Policas.Include("Knjigas").Where(c => c.Id == polica.ShelfID).FirstOrDefault();
            List<Knjiga> curb = p.Knjigas;
            if (curb != null)
            {
                defaultViewModel.Profile.CurrentlyReadingBooks = new List<HubPageViewModel.ProfileInfo.BookInfo>();
                defaultViewModel.Profile.CurrentlyReadingBooks = curb.Select(k => new HubPageViewModel.ProfileInfo.BookInfo
                {
                    Autor = "Neki Autor",
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
                EventInformation = "ovo treba ispraviti",
                IsOcjena = t.IsOcjena,
                KnjigaID = t.KnjigaID,
                OcjenaID = t.Knjiga.Ocjenas.Where(o => o.KnjigaID == t.KnjigaID && o.KorisnikID == t.KorisnikID).FirstOrDefault().Id
            }).ToList();

            return defaultViewModel;
        }

        [HttpGet]
        [Route("api/PhoneKnjigaDetalji/{id}/{userid}")]
        public KnjigaDetaljiViewModel GetDetalji(int id, int userid)
        {
            KnjigaDetaljiViewModel kdvm = db.Knjigas.Where(x => x.Id == id).Select(z => new KnjigaDetaljiViewModel
            {
                KnjigaID = z.Id,
                Naslov = z.Naslov,
                ISBN = z.ISBN,
                Objavljena = z.Objavljena,
                AutorID = z.AutorId,
                Autor = z.Autor.Ime + " " + z.Autor.Prezime,
                ProsjecnaOcjena = (float)z.Ocjenas.Average(a => a.OcjenaIznos),
                Ocjene = z.Ocjenas.Select(o => new KnjigaDetaljiViewModel.OcjenaInfo 
                {
                    Ocjena = o.OcjenaIznos,
                    OcjenaID = o.Id,
                    Opis = o.Opis,
                    username = o.Korisnik.username
                }).ToList(),
                Slika = z.Slika,
                OcjenaLogiranogKorisnika = z.Ocjenas.Where(g => g.KorisnikID == userid).FirstOrDefault().OcjenaIznos
            }).FirstOrDefault();

            return kdvm;
        }

        [HttpGet]
        [Route("api/PhoneProfil/{userid}")]
        public HubPageViewModel.ProfileInfo GetProfileData(int userid)
        {
            HubPageViewModel.ProfileInfo profil = db.Korisnics.Where(x => x.Id == userid).Select(z => new HubPageViewModel.ProfileInfo
            {
                KorisnikID = z.Id,
                username = z.username,
                Joined = z.created_at,
                Grad = z.Grad,
                FriendCount = z.Prijateljstvos.Count()
            }).FirstOrDefault();

            List<HubPageViewModel.ShelvesInfo.ShelfInfo> police = db.Policas.Where(x => x.KorisnikID == userid).Select(p => new HubPageViewModel.ShelvesInfo.ShelfInfo 
            {
                BookCount = p.Knjigas.Count(),
                KorisnikID = p.KorisnikID,
                Naziv = p.Naziv,
                ShelfID = p.Id
            }).ToList();
            HubPageViewModel.ShelvesInfo.ShelfInfo polica = police.Where(x => x.Naziv == "CurrentlyReading").FirstOrDefault();
            if (polica != null)
            {
                Polica gk = db.Policas.Include("Knjigas").Where(c => c.Id == polica.ShelfID).FirstOrDefault();
                List<Knjiga> curb = gk.Knjigas;
                if (curb != null)
                {
                    profil.CurrentlyReadingBooks = new List<HubPageViewModel.ProfileInfo.BookInfo>();
                    profil.CurrentlyReadingBooks = curb.Select(k => new HubPageViewModel.ProfileInfo.BookInfo
                    {
                        Autor = "Neki Autor",
                        KnjigaID = k.Id,
                        Naslov = k.Naslov,
                        Slika = k.Slika
                    }).ToList();
                }
                else
                    profil.CurrentlyReadingBooks = null;
            }
            else
                profil.CurrentlyReadingBooks = null;

            return profil;
        }

        [HttpGet]
        [Route("api/Prijatelj/{id1}/{id2}")]
        public IHttpActionResult MakeAFriend(int id1, int id2)
        {
            Prijateljstvo p = new Prijateljstvo();
            p.Korisnik1ID = id1;
            p.Korisnik2ID = id2;
            p.UputioZahtjevID = id1;
            p.Status = 0;

            db.Prijateljstvos.Add(p);

            Prijateljstvo pr = new Prijateljstvo();
            pr.Korisnik1ID = id2;
            pr.Korisnik2ID = id1;
            pr.UputioZahtjevID = id1;
            pr.Status = 0;

            db.Prijateljstvos.Add(pr);

            db.SaveChanges();

            return Ok();
        }
    }
}
