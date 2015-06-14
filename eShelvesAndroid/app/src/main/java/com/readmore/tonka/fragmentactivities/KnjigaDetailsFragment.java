package com.readmore.tonka.fragmentactivities;

import android.app.DialogFragment;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.RatingBar;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.readmore.tonka.activities.ReviewDetailsActivity;
import com.readmore.tonka.adapters.ReviewsAdapter;
import com.readmore.tonka.dialogs.AddBookToShelfDialog;
import com.readmore.tonka.eshelvesnavdrawer.R;
import com.readmore.tonka.helpers.MyVolley;
import com.readmore.tonka.helpers.Sesija;
import com.readmore.tonka.models.Knjiga;
import com.readmore.tonka.models.KnjigaDetaljiVM;
import com.readmore.tonka.models.OcjenaVM;

/**
 * Created by anton on 01.06.2015..
 */
public class KnjigaDetailsFragment extends Fragment{
    TextView BookTitle;
    TextView BookDescription;
    RatingBar ratingBar;
    int BookID;
    ListView lv;
    Button addToShelfBtn;

    public static KnjigaDetailsFragment newInstance(){
        KnjigaDetailsFragment fragment = new KnjigaDetailsFragment();

        return fragment;
    }
    public static KnjigaDetailsFragment newInstance(Knjiga k){
        KnjigaDetailsFragment fragment = new KnjigaDetailsFragment();
        Bundle bundle = new Bundle();
        bundle.putSerializable("knjiga", k);
        fragment.setArguments(bundle);

        return fragment;
    }

    //poslat knjigu ili nesto u ovaj fragment
    //ucitat to u oncreate i postavit listenere

    public KnjigaDetailsFragment(){}

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.activity_knjiga_details, container, false);
        BookTitle = (TextView)rootView.findViewById(R.id.BookTitle);
        BookDescription = (TextView)rootView.findViewById(R.id.BookDescription);
        ratingBar = (RatingBar)rootView.findViewById(R.id.ratingBar);
        lv = (ListView) rootView.findViewById(R.id.citavobjekat);
        addToShelfBtn = (Button) rootView.findViewById(R.id.addtoshelfbtn);

        Knjiga k = (Knjiga) getArguments().getSerializable("knjiga");

        BookTitle.setText(k.Naslov);
        BookDescription.setText(k.Opis);
        BookID = k.Id;

        lv.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                KnjigaDetaljiVM.OcjenaInfoVM ocjena = (KnjigaDetaljiVM.OcjenaInfoVM) lv.getItemAtPosition(i);
                Sesija.UletiDublje(ReviewDetailsFragment.newInstance(ocjena), getActivity().getSupportFragmentManager());
            }
        });

        String url = "http://hci111.app.fit.ba/api/Ocjenas?knjigaID="+k.Id;

        MyVolley.get(url, KnjigaDetaljiVM.class, new Response.Listener<KnjigaDetaljiVM>() {
            @Override
            public void onResponse(KnjigaDetaljiVM response) {
                ListAdapter adapter = new ReviewsAdapter(getActivity().getApplication(), response.OcjenaInfoVMs.toArray(new KnjigaDetaljiVM.OcjenaInfoVM[]{}));
                lv.setAdapter(adapter);
                ratingBar.setRating(response.ProsjecnaOcjena);
                ratingBar.setOnRatingBarChangeListener(new RatingBar.OnRatingBarChangeListener() {
                    OcjenaVM ocjena = new OcjenaVM();

                    @Override
                    public void onRatingChanged(RatingBar ratingBar, float v, boolean b) {
                        ocjena.KorisnikID = Sesija.getLogiraniKorisnik().Id;
                        ocjena.Ocjena = (int) ratingBar.getRating();
                        ocjena.KnjigaID = BookID;

                        String url = "http://hci111.app.fit.ba/api/Ocjenas";
                        MyVolley.post(url, OcjenaVM.class, new Response.Listener<OcjenaVM>() {
                            @Override
                            public void onResponse(OcjenaVM response) {
                                Toast.makeText(getActivity().getApplicationContext(), "Uspjesno ocjenjeno! Ocjena: " + response.Ocjena, Toast.LENGTH_SHORT).show();
                            }
                        }, new Response.ErrorListener() {
                            @Override
                            public void onErrorResponse(VolleyError error) {
                                Toast.makeText(getActivity().getApplication(), error.getMessage() + " GRESKA!", Toast.LENGTH_SHORT).show();
                            }
                        }, ocjena);
                    }
                });
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(getActivity().getApplicationContext(), "greska", Toast.LENGTH_SHORT).show();
            }
        });

        addToShelfBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                AddBookToShelfDialog dialog = new AddBookToShelfDialog();
                Bundle bundle = new Bundle();
                bundle.putInt("knjigaId", BookID);
                dialog.setArguments(bundle);
                dialog.show(getFragmentManager(), "addbooktoshelf");
            }
        });

        return rootView;
    }
}
