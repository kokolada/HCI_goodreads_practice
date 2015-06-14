package com.readmore.tonka.fragmentactivities;

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
import com.readmore.tonka.adapters.KnjigeListAdapter;
import com.readmore.tonka.eshelvesnavdrawer.R;
import com.readmore.tonka.helpers.Config;
import com.readmore.tonka.helpers.MyVolley;
import com.readmore.tonka.helpers.Sesija;
import com.readmore.tonka.models.Knjiga;

import org.apache.http.message.BasicNameValuePair;

/**
 * Created by anton on 01.06.2015..
 */
public class PolicaDetailsFragment extends Fragment {

    ListView lv;

    public static PolicaDetailsFragment newInstance(){
        PolicaDetailsFragment fragment = new PolicaDetailsFragment();

        return fragment;
    }

    public static PolicaDetailsFragment newInstance(int id){
        PolicaDetailsFragment fragment = new PolicaDetailsFragment();
        Bundle bundle = new Bundle();
        bundle.putInt("id", id);
        fragment.setArguments(bundle);

        return fragment;
    }

    public PolicaDetailsFragment(){}

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.activity_polica_details, container, false);

        int id = getArguments().getInt("id");
        TextView text = (TextView) rootView.findViewById(R.id.testTextView);
        lv = (ListView) rootView.findViewById(R.id.knjigeLista);

        lv.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                Knjiga itemClicked = (Knjiga)parent.getAdapter().getItem(position);
                Sesija.UletiDublje(KnjigaDetailsFragment.newInstance(itemClicked), getActivity().getSupportFragmentManager());
            }
        });

        text.setText("polica id: " + id);
        String url = Config.urlApi + "knjigas";

        MyVolley.get(url, Knjiga[].class, new Response.Listener<Knjiga[]>() {
            @Override
            public void onResponse(Knjiga[] response) {
                ListAdapter adapter = new KnjigeListAdapter(getActivity().getApplication(), response);
                lv.setAdapter(adapter);
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(getActivity().getApplicationContext(), error.getMessage(), Toast.LENGTH_SHORT).show();
            }
        }, new BasicNameValuePair("policaID", id + ""));

        return rootView;
    }
}
