package com.readmore.tonka.eshelvesnavdrawer;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentActivity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.google.gson.Gson;
import com.readmore.tonka.adapters.PoliceAdapter;
import com.readmore.tonka.helpers.Config;
import com.readmore.tonka.helpers.MyApp;
import com.readmore.tonka.helpers.MyVolley;
import com.readmore.tonka.helpers.Sesija;
import com.readmore.tonka.models.Polica;

import org.apache.http.message.BasicNameValuePair;

/**
 * Created by anton_000 on 21.4.2015..
 */
public class ProfileFragment extends Fragment{

    private static final String ARG_SECTION_NUMBER = "section_number";
    ListView lv;

    public static ProfileFragment newInstance(int sectionNumber) {
        ProfileFragment fragment = new ProfileFragment();
        Bundle args = new Bundle();
        args.putInt(ARG_SECTION_NUMBER, sectionNumber);
        fragment.setArguments(args);
        return fragment;
    }

    public ProfileFragment() {}

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.profile_fragment, container, false);
        lv = (ListView)rootView.findViewById(R.id.policeProfilListView);

        lv.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                Polica itemClicked =(Polica)parent.getAdapter().getItem(position);
                Intent intent = new Intent(getActivity(), PolicaDetailsActivity.class);
                intent.putExtra("policaID", itemClicked.Id);
                startActivity(intent);
            }
        });

        TextView fptw = (TextView) rootView.findViewById(R.id.friendsProfileTextView);
        fptw.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(MyApp.getAppContext(), FriendsActivity.class);
                startActivity(intent);
            }
        });

        String url = Config.urlApi+"policas";

        MyVolley.get(url, Polica[].class, new Response.Listener<Polica[]>() {
            @Override
            public void onResponse(Polica[] response) {
                ListAdapter adapter = new PoliceAdapter(getActivity(), response);
                lv.setAdapter(adapter);
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(getActivity(), error.getMessage(), Toast.LENGTH_SHORT).show();
            }
        }, new BasicNameValuePair("korisnikId", Sesija.getLogiraniKorisnik().Id + ""));

        return rootView;
    }

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        ((MainActivity) activity).onSectionAttached(
                getArguments().getInt(ARG_SECTION_NUMBER));
    }
}
