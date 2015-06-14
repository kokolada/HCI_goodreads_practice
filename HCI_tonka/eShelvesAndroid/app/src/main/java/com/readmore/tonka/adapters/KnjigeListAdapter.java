package com.readmore.tonka.adapters;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import com.readmore.tonka.eshelvesnavdrawer.R;
import com.readmore.tonka.models.Knjiga;
import com.readmore.tonka.models.Polica;

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

        return view;
    }

    @Override
    public Knjiga getItem(int position) {
        return super.getItem(position);
    }
}
