package com.readmore.tonka.models;

import java.io.Serializable;
import java.util.Date;

/**
 * Created by anton_000 on 30.4.2015..
 */
public class Knjiga implements Serializable{
    public int Id;
    public String Naslov;
    public String Opis;
    public String ISBN;
    public int AutorID;
    public String NazivAutora;
}
