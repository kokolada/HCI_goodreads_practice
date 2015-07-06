using eShelvesAPI.DAL;
using eShelvesAPI.Models;
using eShelvesAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShelvesAPI.Helpers
{
    public class Preporuka
    {
        Dictionary<int?, List<Ocjena>> knjige = new Dictionary<int?, List<Ocjena>>();
        private MojContext db = new MojContext();

        public List<KnjigaVM> GetPreporuceneKnjige(int korisnikId)
        {
            Korisnik k = db.Korisnics.Where(x => x.Id == korisnikId).First();
            List<Korisnik> korisnici = db.Korisnics.Include("Ocjenas").Where(x => x.Id != korisnikId && x.Ocjenas.Count() > 0).ToList();

            Dictionary<int, double> slicnosti = new Dictionary<int, double>();
            
            foreach (Korisnik i in korisnici)
            {
                double slicnost = IzracunajSlicnostKorisnika(k, i);
                //ovde moze samo slicni, ali sam stavio ili prijatelji iz sljedeceg razloga
                // mislim da bi bilo dobro da korisnik dobiva za preporuku knjige koje se njihovim prijateljima svidjaju, iako mozda
                // korisnik i njegov prijatelj nisu slicni... bez obzira na to algoritam za preporuku bi trebao biti ispravan!
                if (slicnost > 0.2 || SuPrijatelji(k.Id, i.Id))
                {
                    if(Double.IsNaN(slicnost))
                        slicnosti.Add(i.Id ,0);
                    else
                        slicnosti.Add(i.Id, slicnost);
                }
            }

            var l = slicnosti.OrderByDescending(x => x.Value);
            Dictionary<int, double> sortirani = l.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);

            List<Knjiga> preporuceneKnjige = new List<Knjiga>();
            
            //proci kroz svakog korisnika
            foreach (KeyValuePair<int,double> item in sortirani)
            {
                //uzeti knjige koje je on ocjenio dobro a nas korisnik nije nikako
                List<Ocjena> ocjene = db.Ocjenas.Where(x => x.KorisnikID == item.Key).ToList();
                foreach (Ocjena o in ocjene)
                {
                     if(o.OcjenaIznos >= 3 && db.Ocjenas.Where(z => z.KnjigaID == o.KnjigaID && z.KorisnikID == korisnikId).Count() == 0)
                        preporuceneKnjige.Add(db.Knjigas.Where(x => x.Id == o.KnjigaID).First());
                }
            }
            List<KnjigaVM> kvm = preporuceneKnjige.Select(x => new KnjigaVM
            {
                AutorId = x.AutorId,
                Id = x.Id,
                ISBN = x.ISBN,
                Naslov = x.Naslov,
                Slika = x.Slika,
                NazivAutora = db.Autors.Find(x.AutorId).Ime + " " + db.Autors.Find(x.AutorId).Prezime
            }).ToList();
            return kvm;
        }

        private bool SuPrijatelji(int id1, int id2)
        {
            return db.Prijateljstvos.Where(x => x.Korisnik1ID == id1 && x.Korisnik2ID == id2).Count() > 0;
        }

        private double IzracunajSlicnostKorisnika(Korisnik k1, Korisnik k2)
        {
            List<Ocjena> ocjene1;
            List<Ocjena> ocjene2;

            ocjene1 = db.Ocjenas.Where(x => x.KorisnikID == k1.Id).OrderBy(c => c.KnjigaID).ToList();
            ocjene2 = db.Ocjenas.Where(x => x.KorisnikID == k2.Id).OrderBy(c => c.KnjigaID).ToList();

            List<Ocjena> zajednicke1 = new List<Ocjena>();
            List<Ocjena> zajednicke2 = new List<Ocjena>();

            foreach (Ocjena o in ocjene1)
            {
                if (ocjene2.Where(x => x.KnjigaID == o.KnjigaID).Count() > 0)
                {
                    zajednicke1.Add(o);
                    zajednicke2.Add(ocjene2.Where(x => x.KnjigaID == o.KnjigaID).First());
                }
            }
            if (zajednicke1.Count == 0)
                return 0;

            double prosjek1 = zajednicke1.Average(x => x.OcjenaIznos);
            double prosjek2 = zajednicke2.Average(x => x.OcjenaIznos);

            double brojnik = 0;
            double sumakvadrata1 = 0;
            double sumakvadrata2 = 0;

            for (int i = 0; i < zajednicke1.Count(); i++)
            {
                brojnik += ((zajednicke1[i].OcjenaIznos - prosjek1)*(zajednicke2[i].OcjenaIznos-prosjek2));
                sumakvadrata1 += (zajednicke1[i].OcjenaIznos - prosjek1) * (zajednicke1[i].OcjenaIznos - prosjek1);
                sumakvadrata2 += (zajednicke2[i].OcjenaIznos - prosjek2) * (zajednicke2[i].OcjenaIznos - prosjek2);
            }

            double slicnost = brojnik / (Math.Sqrt(sumakvadrata1) * Math.Sqrt(sumakvadrata2));
            return slicnost;
        }


        //----------------------------- dio ispod je onaj algoritam za item based preporuku, njega nisam koristio

        //Funkcija koja se poziva iz mobilnog dijela aplikacije
        public List<Knjiga> GetSlicneKnjige(int knjigaId)
        {
            UcitajKnjige(knjigaId);
            List<Ocjena> ocjene = db.Ocjenas.Where(x => x.KnjigaID == knjigaId).OrderBy(x => x.KorisnikID).ToList();

            List<Ocjena> zajednickeOcjene1 = new List<Ocjena>();
            List<Ocjena> zajednickeOcjene2 = new List<Ocjena>();

            List<Knjiga> preporuceno = new List<Knjiga>();

            //Prva petlja - lista svih proizvoda (ne uključujući onaj koji je proslijeđen u funkciju)
            foreach (var item in knjige)
            {
                foreach (Ocjena o in ocjene)  //Sve ocjene aktivnog proizvoda
                {
                    //Provjeriti da li je naredni proizvod (iz liste proizvodi) ocijenio isti kupac
                    if (item.Value.Where(x => x.KorisnikID == o.KorisnikID).Count() > 0)
                    {
                        zajednickeOcjene1.Add(o);
                        zajednickeOcjene2.Add(item.Value.Where(x => x.KorisnikID == o.KorisnikID).First());
                    }
                }

                //Za računanje sličnosti se uzimaju samo zajedničke ocjene, odnosno ocjene istih kupaca za oba proizvoda
                double slicnost = GetSlicnost(zajednickeOcjene1, zajednickeOcjene2);
                if (slicnost > 0.6) //Granična vrijednost (treshold)
                    preporuceno.Add(db.Knjigas.Where(x => x.Id == item.Key).FirstOrDefault());


                zajednickeOcjene1.Clear();
                zajednickeOcjene2.Clear();
            }

            //Lista preporučenih proizvoda
            return preporuceno;
        }

        //Učitava se kolekcija proizvoda i njihovih ocjena (osim onog koji se trenutno pregleda)
        private void UcitajKnjige(int knjigaId)
        {
            List<Knjiga> listaKnjiga = db.Knjigas.Where(x => x.Id != knjigaId).ToList();

            List<Ocjena> ocjene;
            foreach (Knjiga p in listaKnjiga)
            {
                ocjene = db.Ocjenas.Where(x => x.Id == p.Id).OrderBy(x => x.KorisnikID).ToList();
                if (ocjene.Count > 0)
                    knjige.Add(p.Id, ocjene);

            }
        }

        //Sličnost proizvoda - Cosine similarity
        double GetSlicnost(List<Ocjena> ocjene1, List<Ocjena> ocjene2)
        {
            if (ocjene1.Count != ocjene2.Count)
                return 0;

            int brojnik = 0;
            double int1 = 0;
            double int2 = 0;

            for (int i = 0; i < ocjene1.Count; i++)
            {
                brojnik += ocjene1[i].OcjenaIznos * ocjene2[i].OcjenaIznos;
                int1 += ocjene1[i].OcjenaIznos * ocjene1[i].OcjenaIznos;
                int2 += ocjene2[i].OcjenaIznos * ocjene2[i].OcjenaIznos;
            }

            int1 = Math.Sqrt(int1);
            int2 = Math.Sqrt(int2);

            double nazivnik = int1 * int2;

            if (nazivnik != 0)
                return brojnik / nazivnik;

            return 0;

        }
    }
}