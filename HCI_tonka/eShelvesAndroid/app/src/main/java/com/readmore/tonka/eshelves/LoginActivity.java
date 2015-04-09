package com.readmore.tonka.eshelves;

import android.content.Context;
import android.content.Intent;
import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;

import org.apache.http.message.BasicNameValuePair;

import helper.MyVolley;


public class LoginActivity extends ActionBarActivity {

    private TextView v;
    private EditText txtUsername;
    private EditText txtPassword;
    private Context context = this;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        v = (TextView)findViewById(R.id.loginText);
        txtUsername = (EditText) findViewById(R.id.username);
        txtPassword = (EditText) findViewById(R.id.password);
    }

    public void Login(View view){
        v.setText("ja klikno batn");

        RequestQueue rq = Volley.newRequestQueue(this);
        String url = "http://192.168.0.102:50753/api/test";
        String urlParametri = url + "?username=" + txtUsername.getText() + "&password=" + txtPassword.getText();

        StringRequest r = new StringRequest(Request.Method.GET, urlParametri,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        v.setText(response);
                        Intent intent = new Intent(getBaseContext(), GlavniAktivity.class);
                        startActivity(intent);
                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                v.setText("ne radi");
            }
        });

        rq.add(r);
    }
/*
    private void do_btnLogin_click()
    {
        MyVolley.get(this, AutentifikacijaProvjeraVM.class, new Response.Listener<AutentifikacijaProvjeraVM>() {
                    @Override
                    public void onResponse(AutentifikacijaProvjeraVM response) {
                        Sesija.logiraniKorisnik = response;
                        if (response == null)
                            Toast.makeText(context, "Pogrešan username ili password", Toast.LENGTH_LONG).show();
                        else {
                            if (response.StudiranjeInfo != null) {
                                Sesija.odabraniStudij = response.StudiranjeInfo.get(0);
                                Intent intent = new Intent(context, OdabirStudijaActivity.class);
                                startActivity(intent);
                            }
                            if (response.ZaposlenjaInfo != null && response.ZaposlenjaInfo.size() > 0) {
                                Toast.makeText(context, "Opcije za zaposlenika nisu implementirano ", Toast.LENGTH_LONG).show();
                            }
                        }
                    }
                }, AutentifikacijaProvjeraVM.url,
                new BasicNameValuePair("username", txtUsername.getText().toString()),
                new BasicNameValuePair("password", txtPassword.getText().toString())
        );
    }
    */
}
