package com.readmore.tonka.helpers;

import android.content.Context;
import android.content.SharedPreferences;

import com.google.gson.Gson;
import com.readmore.tonka.models.LogiraniKorisnik;

/**
 * Created by anton_000 on 2.5.2015..
 */
public class Sesija {

    private static LogiraniKorisnik LogiraniKorisnik;
    private static int BrojNeprocitanihPoruka;

    public static int getBrojNeprocitanihPoruka(){ return BrojNeprocitanihPoruka; }
    public static void setBrojNeprocitanihPoruka(int brojNeprocitanihPoruka) {
        BrojNeprocitanihPoruka = brojNeprocitanihPoruka;
    }

    public static LogiraniKorisnik getLogiraniKorisnik(){
        if(LogiraniKorisnik != null)
            return LogiraniKorisnik;

        SharedPreferences sharedPreferences = MyApp.getAppContext().getSharedPreferences("moja_aplikacija", Context.MODE_PRIVATE);
        String jsonKorisnik = sharedPreferences.getString("logiraniKorisnik", "");

        if(jsonKorisnik.length() == 0)
            return null;

        Gson gson = new Gson();
        LogiraniKorisnik = gson.fromJson(jsonKorisnik, LogiraniKorisnik.class);

        return LogiraniKorisnik;
    }

    public static void setLogiraniKorisnik(LogiraniKorisnik logiraniKorisnik){
        LogiraniKorisnik = logiraniKorisnik;

        SharedPreferences sharedPreferences = MyApp.getAppContext().getSharedPreferences("moja_aplikacija", Context.MODE_PRIVATE);
        final SharedPreferences.Editor editor = sharedPreferences.edit();
        Gson gson = new Gson();
        editor.putString("logiraniKorisnik", gson.toJson(LogiraniKorisnik));
        editor.commit();
    }


}
