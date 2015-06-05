using eShelvesAPI.DAL;
using eShelvesAPI.Models;
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