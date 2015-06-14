package com.readmore.tonka.models;

import java.util.Date;
import java.util.List;

/**
 * Created by anton_000 on 22.4.2015..
 */
public class Korisnik {
    public int Id;
    public String Ime;
    public String Prezime;
    public String username;
    public String Email;
    public String Spol;
    public String Grad;
    public Date created_at;

    List<String> Policas;
    List<String> Ocjenas;
    List<String> Prijateljstvos;
}
