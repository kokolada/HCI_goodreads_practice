package com.readmore.tonka.eshelvesnavdrawer;

import android.app.Activity;
import android.app.Dialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.DialogFragment;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.Button;
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
import com.readmore.tonka.dialogs.AddNewShelfDialog;
import com.readmore.tonka.helpers.Config;
import com.readmore.tonka.helpers.MyVolley;
import com.readmore.tonka.helpers.Sesija;
import com.readmore.tonka.models.Polica;

import org.apache.http.message.BasicNameValuePair;

/**
 * Created by anton_000 on 21.4.2015..
 */
public class MyBooksFragment extends Fragment {
    private static final String ARG_SECTION_NUMBER = "section_number";

    Button addNewShelf;

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
                if(itemClicked.BookCount > 0) {
                    Intent intent = new Intent(getActivity(), PolicaDetailsActivity.class);
                    intent.putExtra("policaID", itemClicked.Id);
                    startActivity(intent);
                } else{
                    Toast.makeText(getActivity(),"There are no books in this shelf!", Toast.LENGTH_SHORT).show();
                }
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
        }, new BasicNameValuePair("korisnikId", Sesija.getLogiraniKorisnik().Id+""));

        addNewShelf = (Button) rootView.findViewById(R.id.addShelfBtn);
        addNewShelf.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                DialogFragment dialog = new AddNewShelfDialog();
                dialog.show(getFragmentManager(), "addshelf");
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
