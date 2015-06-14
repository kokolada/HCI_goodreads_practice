package com.readmore.tonka.fragmentactivities;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.Toast;

import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.readmore.tonka.adapters.PrijateljiAdapter;
import com.readmore.tonka.eshelvesnavdrawer.ProfileFragment;
import com.readmore.tonka.eshelvesnavdrawer.R;
import com.readmore.tonka.helpers.Config;
import com.readmore.tonka.helpers.MyVolley;
import com.readmore.tonka.helpers.Sesija;
import com.readmore.tonka.models.Prijatelj;

import org.apache.http.message.BasicNameValuePair;

/**
 * Created by anton on 31.05.2015..
 */
public class FriendsFragment extends Fragment{

    ListView lv;

    public static FriendsFragment newInstance(){
        FriendsFragment friendsFragment = new FriendsFragment();
        return friendsFragment;
    }

    public FriendsFragment(){}

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.activity_friends, container, false);
        lv = (ListView)rootView.findViewById(R.id.friendslistview);

        lv.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                Prijatelj p = (Prijatelj) adapterView.getItemAtPosition(i);
                Sesija.UletiDublje(ProfileFragment.newInstance(p.PrijateljID, p.username), getActivity().getSupportFragmentManager());
            }
        });

        String url = Config.urlApi+"prijateljstvoes";

        MyVolley.get(url, Prijatelj[].class, new Response.Listener<Prijatelj[]>() {
            @Override
            public void onResponse(Prijatelj[] response) {
                ListAdapter adapter = new PrijateljiAdapter(getActivity().getApplicationContext(), response);
                lv.setAdapter(adapter);
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(getActivity().getApplicationContext(), error.getMessage(), Toast.LENGTH_SHORT).show();
            }
        }, new BasicNameValuePair("userId", Sesija.getLogiraniKorisnik().Id+""));

        return rootView;
    }
}
