package com.readmore.tonka.dialogs;

import android.app.AlertDialog;
import android.app.Dialog;
import android.content.DialogInterface;
import android.os.Bundle;
import android.support.v4.app.DialogFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.Toast;

import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.readmore.tonka.adapters.PoliceAdapter;
import com.readmore.tonka.eshelvesnavdrawer.R;
import com.readmore.tonka.helpers.Config;
import com.readmore.tonka.helpers.MyVolley;
import com.readmore.tonka.helpers.Sesija;
import com.readmore.tonka.models.KnjigaPolicaVM;
import com.readmore.tonka.models.Polica;

import org.apache.http.message.BasicNameValuePair;

/**
 * Created by anton on 13.06.2015..
 */
public class AddBookToShelfDialog extends DialogFragment {
    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {

        final Polica[] KliknutaPolica = {new Polica()};

        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
        final LayoutInflater inflater = getActivity().getLayoutInflater();
        final View view  = inflater.inflate(R.layout.addbooktoshelf_dialog, null);

        final ListView lv = (ListView) view.findViewById(R.id.dialoglista);

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

        builder.setMessage("Choose a shelf")
                .setView(view)
                .setNegativeButton("cancel", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        dismiss();
                    }
                })
                .setPositiveButton("add book", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        String url = "http://hci111.app.fit.ba/api/Knjigas";
                        KnjigaPolicaVM kp = new KnjigaPolicaVM();
                        kp.policaId = KliknutaPolica[0].Id;
                        kp.knjigaId = getArguments().getInt("knjigaId");
                        kp.korisnikId = Sesija.getLogiraniKorisnik().Id;
                        MyVolley.post(url, null, null, null, kp);
                    }
                });

        lv.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                KliknutaPolica[0] =(Polica)adapterView.getAdapter().getItem(i);
            }
        });

        return builder.create();

    }
}
