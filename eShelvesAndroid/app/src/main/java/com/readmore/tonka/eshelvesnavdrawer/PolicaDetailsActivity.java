package com.readmore.tonka.eshelvesnavdrawer;

import android.content.Context;
import android.content.Intent;
import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
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
import com.readmore.tonka.adapters.KnjigeListAdapter;
import com.readmore.tonka.helpers.Config;
import com.readmore.tonka.helpers.MyVolley;
import com.readmore.tonka.models.Knjiga;
import com.readmore.tonka.models.Polica;

import org.apache.http.message.BasicNameValuePair;


public class PolicaDetailsActivity extends ActionBarActivity {
    ListView lv;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_polica_details);

        int id = getIntent().getIntExtra("policaID", 99);
        TextView test = (TextView)findViewById(R.id.testTextView);
        lv = (ListView)findViewById(R.id.knjigeLista);

        lv.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                Knjiga itemClicked = (Knjiga)parent.getAdapter().getItem(position);
                Intent intent = new Intent(getBaseContext(), KnjigaDetailsActivity.class);
                intent.putExtra("bookid", itemClicked.Id);
                intent.putExtra("booktitle", itemClicked.Naslov);
                intent.putExtra("bookdescription", itemClicked.Opis);

                startActivity(intent);
            }
        });

        test.setText("polica id: " + id);
        String url = Config.urlApi + "knjigas";

        MyVolley.get(url, Knjiga[].class, new Response.Listener<Knjiga[]>() {
            @Override
            public void onResponse(Knjiga[] response) {
                ListAdapter adapter = new KnjigeListAdapter(getApplication(), response);
                lv.setAdapter(adapter);
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(getApplicationContext(), error.getMessage(), Toast.LENGTH_SHORT).show();
            }
        }, new BasicNameValuePair("policaID", id+""));
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_polica_details, menu);
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
