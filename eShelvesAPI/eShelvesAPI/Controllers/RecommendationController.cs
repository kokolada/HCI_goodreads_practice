using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eShelvesAPI.Controllers
{
    public class RecommendationController : ApiController
    {
        Dictionary<int, List<Ocjene>> proizvodi = new Dictionary<int, List<Ocjene>>();

        #region Item-based preporuka

        //Funkcija koja se poziva iz web dijela aplikacije
        public List<Proizvodi> GetSlicneProizvode(int proizvodId)
        {
            UcitajProizvode(proizvodId);
            List<Ocjene> ocjene = Connection.dm.Ocjene.Where(x => x.ProizvodID == proizvodId).OrderBy(x => x.KupacID).ToList();

            List<Ocjene> zajednickeOcjene1 = new List<Ocjene>();
            List<Ocjene> zajednickeOcjene2 = new List<Ocjene>();

            List<Proizvodi> preporuceno = new List<Proizvodi>();

            //Prva petlja - lista svih proizvoda (ne uključujući onaj koji je proslijeđen u funkciju)
            foreach (var item in proizvodi)
            {
                foreach (Ocjene o in ocjene)  //Sve ocjene aktivnog proizvoda
                {
                    //Provjeriti da li je naredni proizvod (iz liste proizvodi) ocijenio isti kupac
                    if (item.Value.Where(x => x.KupacID == o.KupacID).Count() > 0)
                    {
                        zajednickeOcjene1.Add(o);
                        zajednickeOcjene2.Add(item.Value.Where(x => x.KupacID == o.KupacID).First());
                    }
                }

                //Za računanje sličnosti se uzimaju samo zajedničke ocjene, odnosno ocjene istih kupaca za oba proizvoda
                double slicnost = GetSlicnost(zajednickeOcjene1, zajednickeOcjene2);
                if (slicnost > 0.6) //Granična vrijednost (treshold)
                    preporuceno.Add(DAProizvodi.SelectById(item.Key));

                zajednickeOcjene1.Clear();
                zajednickeOcjene2.Clear();
            }

            //Lista preporučenih proizvoda
            return preporuceno;
        }

        //Učitava se kolekcija proizvoda i njihovih ocjena (osim onog koji se trenutno pregleda)
        private void UcitajProizvode(int proizvodId)
        {
            List<Proizvodi> aktivniProizvodi = Connection.dm.Proizvodi.Where(x => x.Status == true && x.ProizvodID != proizvodId).ToList();

            List<Ocjene> ocjene = new List<Ocjene>();
            foreach (Proizvodi p in aktivniProizvodi)
            {
                ocjene = Connection.dm.Ocjene.Where(x => x.ProizvodID == p.ProizvodID).OrderBy(x => x.KupacID).ToList();
                if (ocjene.Count > 0)
                    proizvodi.Add(p.ProizvodID, ocjene);

            }
        }

        //Cosine similarity
        double GetSlicnost(List<Ocjene> ocjene1, List<Ocjene> ocjene2)
        {
            if (ocjene1.Count != ocjene2.Count)
                return 0;

            int brojnik = 0;
            double int1 = 0;
            double int2 = 0;

            for (int i = 0; i < ocjene1.Count; i++)
            {
                brojnik += ocjene1[i].Ocjena * ocjene2[i].Ocjena;
                int1 += ocjene1[i].Ocjena * ocjene1[i].Ocjena;
                int2 += ocjene2[i].Ocjena * ocjene2[i].Ocjena;
            }

            int1 = Math.Sqrt(int1);
            int2 = Math.Sqrt(int2);

            double nazivnik = int1 * int2;

            if (nazivnik != 0)
                return brojnik / nazivnik;

            return 0;

        }

        #endregion
    }
}