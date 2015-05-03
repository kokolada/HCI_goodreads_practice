package com.readmore.tonka.helpers;

import android.app.Application;
import android.content.Context;

/**
 * Created by anton_000 on 2.5.2015..
 */
public class MyApp extends Application
{
    private static Context context;
    public void onCreate()
    {
        super.onCreate();
        context = getApplicationContext();
    }
    public static Context getAppContext()
    {
        return context;
    }
}