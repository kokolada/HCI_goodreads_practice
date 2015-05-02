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
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.google.gson.Gson;
import com.readmore.tonka.adapters.PoliceAdapter;
import com.readmore.tonka.models.Polica;

/**
 * Created by anton_000 on 21.4.2015..
 */
public class MyBooksFragment extends Fragment {
    private static final String ARG_SECTION_NUMBER = "section_number";

    public static MyBooksFragment newInstance(int sectionNumber) {
        MyBooksFragment fragment = new MyBooksFragment();
        Bundle args = new Bundle();
        args.putInt(ARG_SECTION_NUMBER, sectionNumber);
        fragment.setArguments(args);
        return fragment;
    }

    public MyBooksFragment() {}

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.mybooks_fragment, container, false);
        final ListView lv = (ListView)rootView.findViewById(R.id.mybooksListView);
        lv.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                Polica itemClicked =(Polica)parent.getAdapter().getItem(position);
                Intent intent = new Intent(getActivity(), PolicaDetailsActivity.class);
                intent.putExtra("policaID", itemClicked.Id);
                startActivity(intent);
            }
        });

        RequestQueue queue = Volley.newRequestQueue(getActivity());
        String url ="http://hci111.app.fit.ba/api/policas?korisnikId=1";
        StringRequest stringRequest = new StringRequest(Request.Method.GET, url,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        Gson gson = new Gson();
                        Polica[] vrijednosti = gson.fromJson(response, Polica[].class);
                        ListAdapter adapter = new PoliceAdapter(getActivity(), vrijednosti);
                        lv.setAdapter(adapter);
                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(getActivity(), error.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });
        queue.add(stringRequest);

        return rootView;
    }

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        ((MainActivity) activity).onSectionAttached(
                getArguments().getInt(ARG_SECTION_NUMBER));
    }
}
