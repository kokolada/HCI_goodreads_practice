package com.readmore.tonka.fragmentactivities;

import android.app.Activity;
import android.content.Context;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.inputmethod.InputMethodManager;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Toast;

import com.android.volley.Response;
import com.readmore.tonka.adapters.KnjigeListAdapter;
import com.readmore.tonka.eshelvesnavdrawer.MainActivity;
import com.readmore.tonka.eshelvesnavdrawer.R;
import com.readmore.tonka.helpers.Config;
import com.readmore.tonka.helpers.MyVolley;
import com.readmore.tonka.helpers.Sesija;
import com.readmore.tonka.models.Knjiga;

import org.apache.http.message.BasicNameValuePair;

/**
 * Created by anton on 08.06.2015..
 */
public class SearchFragment extends Fragment{

    EditText searchTerm;
    Button searchButton;
    ListView lv;

    public static SearchFragment newInstance(){
        SearchFragment fragment = new SearchFragment();

        return fragment;
    }

    public SearchFragment(){}

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        final View rootView = inflater.inflate(R.layout.activity_searchable, container, false);

        searchTerm = (EditText) rootView.findViewById(R.id.searchEditText);
        lv = (ListView) rootView.findViewById(R.id.searchListView);
        searchButton = (Button) rootView.findViewById(R.id.searchButton);

        /*if(getArguments().getString("query")!=null){
            searchTerm.setText(getArguments().getString("query"));
            ArrayAdapter adapter = new KnjigeListAdapter(getActivity(), (Knjiga[])getArguments().getSerializable("knjige"));
            lv.setAdapter(adapter);
        }*/

        lv.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                Knjiga itemClicked = (Knjiga)adapterView.getAdapter().getItem(i);

                Sesija.UletiDublje(KnjigaDetailsFragment.newInstance(itemClicked), getActivity().getSupportFragmentManager());
            }
        });

        searchButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                hideSoftKeyboard(getActivity(), rootView);
                searchTerm.clearFocus();
                if(searchTerm.getText().length() == 0)
                    Toast.makeText(getActivity(), "Please enter a search term.", Toast.LENGTH_SHORT).show();
                else{
                    final String query = searchTerm.getText().toString();
                    String url = Config.urlApi + "Search";
                    MyVolley.get(url, Knjiga[].class, new Response.Listener<Knjiga[]>() {
                        @Override
                        public void onResponse(Knjiga[] response) {
                            ArrayAdapter adapter = new KnjigeListAdapter(getActivity(), response);
                            lv.setAdapter(adapter);
                            /*Bundle bundle = new Bundle();
                            bundle.putSerializable("knjige", response);
                            bundle.putString("query", query);
                            setArguments(bundle);*/
                        }
                    }, null, new BasicNameValuePair("query", query));
                }
            }
        });

        return rootView;
    }

    public static void hideSoftKeyboard (Activity activity, View view)
    {
        InputMethodManager imm = (InputMethodManager)activity.getSystemService(Context.INPUT_METHOD_SERVICE);
        imm.hideSoftInputFromWindow(view.getApplicationWindowToken(), 0);
    }
}
