package com.readmore.tonka.helpers;

import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;

import com.google.gson.Gson;
import com.readmore.tonka.eshelvesnavdrawer.R;
import com.readmore.tonka.models.LogiraniKorisnik;

import java.util.Stack;

/**
 * Created by anton_000 on 2.5.2015..
 */
public class Sesija {

    public static class NavContainer{
        public Class<? extends Fragment> fragment;
        public Bundle bundle;

        public NavContainer(Class aClass, Bundle bundle){
            fragment = aClass;
            this.bundle = bundle;
        }
    }

    public static Stack<NavContainer> navigacija = new Stack<>();
    public static Fragment currentFragment = null;

    public static int getDubina(){
        return navigacija.size();
    }

    public static void addFragment(Class aClass){ navigacija.add(new NavContainer(aClass, null)); }

    public static void addFragment(Class aClass, Bundle bundle){ navigacija.add(new NavContainer(aClass, bundle)); }

    public static Fragment getReturnFragment(){
        NavContainer nav =  navigacija.pop();
        Fragment fragment = null;
        try {
            fragment = nav.fragment.newInstance();
            fragment.setArguments(nav.bundle);
        } catch (InstantiationException e) {
            e.printStackTrace();
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        }

        currentFragment = fragment;
        return fragment;
    }

    public static void UletiDublje(Class aClass, Bundle bundle, FragmentManager fragmentManager){
        addFragment(currentFragment.getClass(), currentFragment.getArguments());

        try {
            currentFragment = (Fragment)aClass.newInstance();
        } catch (InstantiationException e) {
            e.printStackTrace();
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        }

        fragmentManager.beginTransaction().replace(R.id.container, Sesija.currentFragment).commit();
    }

    public static void IdiNazad(FragmentManager fragmentManager){
        fragmentManager.beginTransaction().replace(R.id.container, getReturnFragment()).commit();
    }

    //---------------------------------------------------------------------------

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
