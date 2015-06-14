package com.readmore.tonka.activities;

import android.content.Intent;
import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.RatingBar;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.readmore.tonka.adapters.ReviewsAdapter;
import com.readmore.tonka.eshelvesnavdrawer.R;
import com.readmore.tonka.helpers.MyVolley;
import com.readmore.tonka.helpers.Sesija;
import com.readmore.tonka.models.Knjiga;
import com.readmore.tonka.models.KnjigaDetaljiVM;
import com.readmore.tonka.models.OcjenaVM;

public class KnjigaDetailsActivity extends ActionBarActivity {
    TextView BookTitle;
    TextView BookDescription;
    RatingBar ratingBar;
    int BookID;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_knjiga_details);

        BookTitle = (TextView)findViewById(R.id.BookTitle);
        BookDescription = (TextView)findViewById(R.id.BookDescription);
        ratingBar = (RatingBar)findViewById(R.id.ratingBar);

        ratingBar.setOnRatingBarChangeListener(new RatingBar.OnRatingBarChangeListener() {
            OcjenaVM ocjena = new OcjenaVM();

            @Override
            public void onRatingChanged(RatingBar ratingBar, float v, boolean b) {
                ocjena.KorisnikID = Sesija.getLogiraniKorisnik().Id;
                ocjena.Ocjena = (int)ratingBar.getRating();
                ocjena.KnjigaID = getIntent().getIntExtra("bookid", 99);

                String url = "http://hci111.app.fit.ba/api/Ocjenas";
                MyVolley.post(url, OcjenaVM.class, new Response.Listener<OcjenaVM>() {
                    @Override
                    public void onResponse(OcjenaVM response) {
                        Toast.makeText(getApplicationContext(), "Uspjesno ocjenjeno! Ocjena: " + response.Ocjena, Toast.LENGTH_SHORT).show();
                    }
                }, new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        Toast.makeText(getApplication(), error.getMessage()+ " GRESKA!", Toast.LENGTH_SHORT).show();
                    }
                }, ocjena);
            }
        });

        Knjiga k = new Knjiga();
        k.Id = getIntent().getIntExtra("bookid", 99);
        k.Naslov = getIntent().getStringExtra("booktitle");
        k.Opis = getIntent().getStringExtra("bookdescription");

        BookTitle.setText(k.Naslov);
        BookDescription.setText(k.Opis);

        final ListView objekat = (ListView)findViewById(R.id.citavobjekat);

        objekat.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                KnjigaDetaljiVM.OcjenaInfoVM ocjena = (KnjigaDetaljiVM.OcjenaInfoVM) objekat.getItemAtPosition(i);
                Intent intent = new Intent(getApplication(), ReviewDetailsActivity.class);
                intent.putExtra("reviewID", ocjena);
                startActivity(intent);
            }
        });

        String url = "http://hci111.app.fit.ba/api/Ocjenas?knjigaID="+k.Id;

        MyVolley.get(url, KnjigaDetaljiVM.class, new Response.Listener<KnjigaDetaljiVM>() {
            @Override
            public void onResponse(KnjigaDetaljiVM response) {
                ListAdapter adapter = new ReviewsAdapter(getApplication(), response.OcjenaInfoVMs.toArray(new KnjigaDetaljiVM.OcjenaInfoVM[]{}));
                objekat.setAdapter(adapter);
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(getApplicationContext(), "greska", Toast.LENGTH_SHORT).show();
            }
        });
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_knjiga_details, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }
}
