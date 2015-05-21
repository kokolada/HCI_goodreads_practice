package com.readmore.tonka.helpers;

import com.android.volley.Request;
import com.android.volley.Response;

import org.apache.http.client.utils.URLEncodedUtils;
import org.apache.http.message.BasicNameValuePair;

import java.util.Arrays;

/**
 * Created by anton on 18.05.2015..
 */
public class MyVolley {
    public static <T>  void get(String urlStr, Class<T> responseType, Response.Listener<T> listener, Response.ErrorListener errorListener, BasicNameValuePair... inputParams)
    {
        String urlParam = URLEncodedUtils.format(Arrays.asList(inputParams), "utf-8");
        String url;
        if(urlParam.length() > 0)
            url = urlStr + "?" + urlParam;
        else
            url = urlStr;
        GsonRequest<T> gsonRequest = new GsonRequest<>(Request.Method.GET, url, responseType, null, null, listener, errorListener);
        VolleySingleton.getInstance(MyApp.getAppContext()).addToRequestQueue(gsonRequest);
    }

    public static <T>  void post(String urlStr,  Class<T> responseType, final Response.Listener<T> listener, Response.ErrorListener errorListener, Object postObject)
    {
        String jsonStr = MyGson.build().toJson(postObject);
        GsonRequest<T> gsonRequest = new GsonRequest<>(Request.Method.POST, urlStr, responseType, null, jsonStr, listener, errorListener);
        VolleySingleton.getInstance(MyApp.getAppContext()).addToRequestQueue(gsonRequest);
    }
}
