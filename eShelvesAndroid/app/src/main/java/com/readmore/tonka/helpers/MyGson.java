package com.readmore.tonka.helpers;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

/**
 * Created by anton on 18.05.2015..
 */
public class MyGson {
    public static Gson build()
    {
        GsonBuilder builder = new GsonBuilder();
        return builder.setDateFormat("yyyy-MM-dd'T'HH:mm:ss").create();
    }
}
