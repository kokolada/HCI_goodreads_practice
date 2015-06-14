package com.readmore.tonka.eshelvesnavdrawer;

import android.app.Activity;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;

import com.android.volley.Response;
import com.readmore.tonka.adapters.KnjigeListAdapter;
import com.readmore.tonka.fragmentactivities.KnjigaDetailsFragment;
import com.readmore.tonka.helpers.Config;
import com.readmore.tonka.helpers.MyVolley;
import com.readmore.tonka.helpers.Sesija;
import com.readmore.tonka.models.Knjiga;

import org.apache.http.message.BasicNameValuePair;

/**
 * Created by anton_000 on 21.4.2015..
 */
public class RecommendationsFragment extends Fragment{

    private static final String ARG_SECTION_NUMBER = "section_number";

    public static RecommendationsFragment newInstance(int sectionNumber) {
        RecommendationsFragment fragment = new RecommendationsFragment();
        Bundle args = new Bundle();
        args.putInt(ARG_SECTION_NUMBER, sectionNumber);
        fragment.setArguments(args);
        return fragment;
    }

    public RecommendationsFragment() {}

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.recommendations_fragment, container, false);

        final ListView lv = (ListView) rootView.findViewById(R.id.preporukeLista);

        MyVolley.get(Config.urlApi + "Preporuke", Knjiga[].class, new Response.Listener<Knjiga[]>() {
            @Override
            public void onResponse(Knjiga[] response) {
                ArrayAdapter adapter = new KnjigeListAdapter(getActivity(), response);
                lv.setAdapter(adapter);
            }
        }, null, new BasicNameValuePair("korisnikId", Sesija.getLogiraniKorisnik().Id+""));

        lv.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                Knjiga k = (Knjiga) adapterView.getItemAtPosition(i);
                Sesija.UletiDublje(KnjigaDetailsFragment.newInstance(k), getActivity().getSupportFragmentManager());
            }
        });

        return rootView;
    }

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        ((MainActivity) activity).onSectionAttached(
                getArguments().getInt(ARG_SECTION_NUMBER));
    }
}
