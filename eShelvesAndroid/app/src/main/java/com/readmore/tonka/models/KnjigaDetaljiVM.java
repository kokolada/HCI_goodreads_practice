package com.readmore.tonka.models;

import java.io.Serializable;
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

    public class OcjenaInfoVM implements Serializable{
        public int KorisnikId;
        public String username;
        public int Ocjena;
        public String Opis;
    }
}
