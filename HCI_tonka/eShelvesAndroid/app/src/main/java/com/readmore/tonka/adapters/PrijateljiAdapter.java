package com.readmore.tonka.adapters;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import com.readmore.tonka.eshelvesnavdrawer.R;
import com.readmore.tonka.models.Polica;
import com.readmore.tonka.models.Prijatelj;

/**
 * Created by anton on 09.05.2015..
 */
public class PrijateljiAdapter extends ArrayAdapter<Prijatelj> {

    public PrijateljiAdapter(Context context, Prijatelj[] values) {
        super(context, R.layout.timeline_list_item, values);
    }


    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        LayoutInflater inflater = LayoutInflater.from(getContext());

        View view = inflater.inflate(R.layout.polica_list_item, parent, false);

        Prijatelj Neki = getItem(position);

        TextView theTextView = (TextView) view.findViewById(R.id.policaNaziv);

        theTextView.setText(Neki.username);

        return view;
    }

    @Override
    public Prijatelj getItem(int position) {
        return super.getItem(position);
    }
}
