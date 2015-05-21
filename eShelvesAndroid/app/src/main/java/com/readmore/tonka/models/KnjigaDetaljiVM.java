package com.readmore.tonka.models;

import java.util.List;

/**
 * Created by anton on 20.05.2015..
 */
public class KnjigaDetaljiVM {
    public String Naslov;
    public int AutorID;
    public String NazivAutora;
    public float ProsjecnaOcjena;
    public String ISBN;
    public String Opis;

    public List<OcjenaInfoVM> OcjenaInfoVMs;

    public class OcjenaInfoVM{
        public int KorisnikId;
        public String username;
        public int Ocjena;
        public String Opis;
    }
}
