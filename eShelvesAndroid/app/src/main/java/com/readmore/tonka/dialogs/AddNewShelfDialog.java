package com.readmore.tonka.dialogs;

import android.app.AlertDialog;
import android.app.Dialog;
import android.content.DialogInterface;
import android.os.Bundle;
import android.support.v4.app.DialogFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.google.gson.Gson;
import com.readmore.tonka.eshelvesnavdrawer.R;
import com.readmore.tonka.helpers.MyVolley;
import com.readmore.tonka.helpers.Sesija;
import com.readmore.tonka.models.Polica;

import org.json.JSONObject;
import org.w3c.dom.Text;

/**
 * Created by anton on 17.05.2015..
 */
public class AddNewShelfDialog extends DialogFragment {
    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {

        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
        final LayoutInflater inflater = getActivity().getLayoutInflater();
        final View view  = inflater.inflate(R.layout.addshelf_dialog, null);
        builder.setMessage("Add a new shelf")
                .setView(view)
                .setNegativeButton("cancel", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        dismiss();
                    }
                })
                .setPositiveButton("submit", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        Polica p = new Polica();
                        EditText addshelfText = (EditText) view.findViewById(R.id.addshelftextedit);
                        p.Naziv = addshelfText.getText().toString();
                        p.KorisnikID = Sesija.getLogiraniKorisnik().Id;

                        String url = "http://hci111.app.fit.ba/api/Policas";

                        MyVolley.post(url, null, null, null, p);
                    }
                });


        return builder.create();

    }
}
