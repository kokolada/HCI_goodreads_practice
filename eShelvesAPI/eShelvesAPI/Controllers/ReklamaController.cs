﻿using eShelvesAPI.DAL;
using eShelvesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eShelvesAPI.Controllers
{
    public class ReklamaController : ApiController
    {
        MojContext db = new MojContext();

        [HttpGet]
        [Route("api/Reklama/Aktivne")]
        public List<Reklama> GetAktivneReklame()
        {
            return db.Reklamas.Where(x => x.PocetakPrikazivanja.AddDays(x.TrajanjeDana) > DateTime.Now).ToList();
        }

        [HttpGet]
        [Route("api/Reklama/RandomAktivna")]
        public Reklama GetRandomAktivnuReklamu()
        {
            List<NarudzbeReklama> narudzbe;
            narudzbe = db.NarudzbaReklamas.Include("Reklama").Where(
                x => x.BrojPrikaza > 0).OrderByDescending(t => t.BrojPrikaza).ToList();

            NarudzbeReklama jedna;
            if (narudzbe != null)
            {
                jedna = narudzbe.Where(
                x => (x.Reklama.PocetakPrikazivanja.AddDays(x.Reklama.TrajanjeDana).Date - DateTime.Now.AddDays(7).Date).TotalDays == 1).FirstOrDefault();
                if (jedna != null)
                {
                    jedna.BrojPrikaza--;
                    db.SaveChanges();
                    return jedna.Reklama;
                }

                jedna = narudzbe.Where(
                x => (x.Reklama.PocetakPrikazivanja.AddDays(x.Reklama.TrajanjeDana).Date - DateTime.Now.AddDays(7).Date).TotalDays <= 7).FirstOrDefault();
                if (jedna != null)
                {
                    jedna.BrojPrikaza--;
                    db.SaveChanges();
                    return jedna.Reklama;
                }

                jedna = narudzbe.Where(
                x => x.Reklama.PocetakPrikazivanja.AddDays(x.Reklama.TrajanjeDana).Date > DateTime.Now.Date).FirstOrDefault();
                if (jedna != null)
                {
                    jedna.BrojPrikaza--;
                    db.SaveChanges();
                    return jedna.Reklama;
                }
            }
            return narudzbe.FirstOrDefault().Reklama;
        }

        [HttpGet]
        [Route("api/Reklama")]
        public List<Reklama> GetReklame()
        {
            return db.Reklamas.ToList();
        }

        [HttpGet]
        [Route("api/Kupac")]
        public List<Kupac> GetKupci()
        {
            return db.Kupacs.ToList();
        }

        [HttpGet]
        [Route("api/Narudzba")]
        public List<NarudzbeReklama> GetNarudzbe()
        {
            return db.NarudzbaReklamas.ToList();
        }

        [HttpGet]
        [Route("api/Reklama/GetById/{id}")]
        public Reklama GetReklamaById(int id)
        {
            return db.Reklamas.Find(id);
        }

        [HttpGet]
        [Route("api/Kupac/GetById/{id}")]
        public Kupac GetKupacById(int id)
        {
            return db.Kupacs.Find(id);
        }

        [HttpGet]
        [Route("api/Narudzba/GetById/{id}")]
        public NarudzbeReklama GetNarudzbaById(int id)
        {
            return db.NarudzbaReklamas.Find(id);
        }

        [HttpPost]
        [Route("api/Reklama/Post")]
        public Reklama PostReklama(Reklama r)
        {
            db.Reklamas.Add(r);
            db.SaveChanges();

            return r;
        }

        [HttpPost]
        [Route("api/Kupac/Post")]
        public Kupac PostKupac(Kupac k)
        {
            db.Kupacs.Add(k);
            db.SaveChanges();

            return k;
        }

        [HttpPost]
        [Route("api/Narudzba/Post")]
        public NarudzbeReklama PostNarudzba(NarudzbeReklama nr)
        {
            db.NarudzbaReklamas.Add(nr);
            db.SaveChanges();

            return nr;
        }
    }
}
