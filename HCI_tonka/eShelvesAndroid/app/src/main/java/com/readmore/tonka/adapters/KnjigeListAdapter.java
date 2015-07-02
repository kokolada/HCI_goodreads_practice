package com.readmore.tonka.adapters;

import android.app.Activity;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.support.v4.app.FragmentActivity;
import android.util.DisplayMetrics;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.ImageRequest;
import com.android.volley.toolbox.NetworkImageView;
import com.android.volley.toolbox.Volley;
import com.readmore.tonka.eshelvesnavdrawer.R;
import com.readmore.tonka.helpers.Config;
import com.readmore.tonka.models.Knjiga;
import com.readmore.tonka.models.Polica;

import java.nio.charset.Charset;

/**
 * Created by anton_000 on 30.4.2015..
 */
public class KnjigeListAdapter extends ArrayAdapter<Knjiga> {

    public KnjigeListAdapter(Context context, Knjiga[] values){
        super(context, R.layout.knjige_polica_list_item, values);
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        LayoutInflater inflater = LayoutInflater.from(getContext());

        View view = inflater.inflate(R.layout.knjige_polica_list_item, parent, false);

        Knjiga Neki = getItem(position);

        TextView naslov = (TextView) view.findViewById(R.id.BookTitle);
        naslov.setText(Neki.Naslov);
        TextView autor = (TextView) view.findViewById(R.id.BookAuthor);
        autor.setText(Neki.NazivAutora);

        if(Neki.Slika != null){
            //byte[] Pic = Neki.Slika.getBytes();
            final NetworkImageView image = (NetworkImageView) view.findViewById(R.id.slikaknjige);
            //image.setImageBitmap(BitmapFactory.decodeByteArray(Pic, 0, Pic.length));
            RequestQueue rq = Volley.newRequestQueue(getContext());
            ImageRequest ir = new ImageRequest(Config.urlApi + "Images?knjigaId=" + Neki.Id, new Response.Listener<Bitmap>() {
                @Override
                public void onResponse(Bitmap response) {
                    image.setImageBitmap(response);
                }
            }, 0, 0, null, new Response.ErrorListener() {
                @Override
                public void onErrorResponse(VolleyError error) {
                    Toast.makeText(getContext(), error.getMessage()+"greska", Toast.LENGTH_SHORT).show();
                }
            });
            rq.add(ir);
        }

        return view;
    }

    @Override
    public Knjiga getItem(int position) {
        return super.getItem(position);
    }
}
