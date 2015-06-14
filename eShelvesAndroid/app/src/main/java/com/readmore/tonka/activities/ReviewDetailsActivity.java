package com.readmore.tonka.activities;

import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.TextView;

import com.google.gson.Gson;
import com.readmore.tonka.eshelvesnavdrawer.R;
import com.readmore.tonka.models.KnjigaDetaljiVM;


public class ReviewDetailsActivity extends ActionBarActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_review_details);
        KnjigaDetaljiVM.OcjenaInfoVM ocjena = (KnjigaDetaljiVM.OcjenaInfoVM)getIntent().getSerializableExtra("reviewID");

        TextView ocjenjivac = (TextView)findViewById(R.id.ocjenjivac);
        TextView opisocjene = (TextView)findViewById(R.id.opisocjene);

        ocjenjivac.setText(ocjena.username + " " + ocjena.Ocjena);
        opisocjene.setText(ocjena.Opis);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_review_details, menu);
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
