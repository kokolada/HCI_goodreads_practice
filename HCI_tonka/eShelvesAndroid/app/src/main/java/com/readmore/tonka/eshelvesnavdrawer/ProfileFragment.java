package com.readmore.tonka.eshelvesnavdrawer;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.readmore.tonka.activities.PolicaDetailsActivity;
import com.readmore.tonka.adapters.PoliceAdapter;
import com.readmore.tonka.fragmentactivities.FriendsFragment;
import com.readmore.tonka.helpers.Config;
import com.readmore.tonka.helpers.MyVolley;
import com.readmore.tonka.helpers.Sesija;
import com.readmore.tonka.models.Korisnik;
import com.readmore.tonka.models.Polica;

import org.apache.http.message.BasicNameValuePair;

/**
 * Created by anton_000 on 21.4.2015..
 */
public class ProfileFragment extends Fragment{

    private static final String ARG_SECTION_NUMBER = "section_number";
    ListView lv;
    TextView username;
    TextView details;
    TextView joined;

    public static ProfileFragment newInstance(int sectionNumber) {
        ProfileFragment fragment = new ProfileFragment();
        Bundle args = new Bundle();
        args.putInt(ARG_SECTION_NUMBER, sectionNumber);
        fragment.setArguments(args);
        return fragment;
    }

    public static ProfileFragment newInstance(int sectionNumber, int profileID){
        ProfileFragment fragment = new ProfileFragment();
        Bundle args = new Bundle();
        args.putInt(ARG_SECTION_NUMBER, sectionNumber);
        args.putInt("id", profileID);
        fragment.setArguments(args);
        return fragment;
    }
    public static ProfileFragment newInstance(int profileID, String username){
        ProfileFragment fragment = new ProfileFragment();
        Bundle args = new Bundle();
        args.putString("username", username);
        args.putInt("id", profileID);
        fragment.setArguments(args);
        return fragment;
    }

    public ProfileFragment() {}

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.profile_fragment, container, false);
        lv = (ListView)rootView.findViewById(R.id.policeProfilListView);
        username = (TextView) rootView.findViewById(R.id.username);
        details = (TextView) rootView.findViewById(R.id.detailsfragmentdetails);
        joined = (TextView) rootView.findViewById(R.id.detailsfragmentjoined);

        if(getArguments().get("id")!=null){
            int korisnikId = getArguments().getInt("id");
            MyVolley.get(Config.urlApi + "Korisniks", Korisnik.class, new Response.Listener<Korisnik>() {
                @Override
                public void onResponse(Korisnik response) {
                    details.setText(response.Ime + " " + response.Prezime + ", " + response.Grad);
                    joined.setText(response.created_at.toString());
                }
            }, null, new BasicNameValuePair("id", korisnikId+""));
        }

        if(getArguments().get("username")!=null)
            username.setText(getArguments().getString("username"));

        lv.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                Polica itemClicked = (Polica) parent.getAdapter().getItem(position);
                Intent intent = new Intent(getActivity(), PolicaDetailsActivity.class);
                intent.putExtra("policaID", itemClicked.Id);
                startActivity(intent);
            }
        });

        TextView fptw = (TextView) rootView.findViewById(R.id.friendsProfileTextView);
        fptw.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Sesija.UletiDublje(FriendsFragment.newInstance(), getActivity().getSupportFragmentManager());
            }
        });

        String url = Config.urlApi+"policas";

        BasicNameValuePair params = null;
        if(getArguments().get("id") != null) {
            params = new BasicNameValuePair("korisnikId", getArguments().getInt("id") + "");
        }
        else
            params = new BasicNameValuePair("korisnikId", Sesija.getLogiraniKorisnik().Id+"");

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
        }, params);

        return rootView;
    }



    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        ((MainActivity) activity).onSectionAttached(
                getArguments().getInt(ARG_SECTION_NUMBER));
    }
}
