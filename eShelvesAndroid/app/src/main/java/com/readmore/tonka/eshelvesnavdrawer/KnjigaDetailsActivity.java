package com.readmore.tonka.eshelvesnavdrawer;

import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.google.gson.Gson;
import com.readmore.tonka.eshelvesnavdrawer.R;
import com.readmore.tonka.helpers.Sesija;
import com.readmore.tonka.models.Knjiga;
import com.readmore.tonka.models.LogiraniKorisnik;

public class KnjigaDetailsActivity extends ActionBarActivity {
    TextView BookTitle;
    TextView BookDescription;
    int BookID;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_knjiga_details);

        BookTitle = (TextView)findViewById(R.id.BookTitle);
        BookDescription = (TextView)findViewById(R.id.BookDescription);

        Knjiga k = new Knjiga();
        k.Id = getIntent().getIntExtra("bookid", 99);
        k.Naslov = getIntent().getStringExtra("booktitle");
        k.Opis = getIntent().getStringExtra("bookdescription");

        BookTitle.setText(k.Naslov);
        BookDescription.setText(k.Opis);

        final TextView objekat = (TextView)findViewById(R.id.citavobjekat);

        String url = "http://hci111.app.fit.ba/api/Ocjenas?knjigaID=3";

        RequestQueue queue = Volley.newRequestQueue(this);

        StringRequest stringRequest = new StringRequest(Request.Method.GET, url,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        objekat.setText(response);
                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(getApplicationContext(), "greska", Toast.LENGTH_SHORT).show();
            }
        });

        queue.add(stringRequest);
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
