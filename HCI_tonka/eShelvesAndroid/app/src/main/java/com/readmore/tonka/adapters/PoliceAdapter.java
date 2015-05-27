package com.readmore.tonka.adapters;

import android.content.Context;
import android.content.res.ColorStateList;
import android.graphics.Color;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import com.readmore.tonka.eshelvesnavdrawer.R;
import com.readmore.tonka.models.Polica;

import org.w3c.dom.Text;

/**
 * Created by anton_000 on 30.4.2015..
 */
public class PoliceAdapter extends ArrayAdapter<Polica>{

    public PoliceAdapter(Context context, Polica[] values) {
        super(context, R.layout.timeline_list_item, values);
    }


    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        LayoutInflater inflater = LayoutInflater.from(getContext());

        View view = inflater.inflate(R.layout.polica_list_item, parent, false);

        Polica Neki = getItem(position);

        TextView theTextView = (TextView) view.findViewById(R.id.policaNaziv);
        TextView policaBookCount = (TextView) view.findViewById(R.id.policaBookCount);

        theTextView.setText(Neki.Naziv);
        if(Neki.BookCount > 0){
            theTextView.setTextColor(Color.BLUE);
            policaBookCount.setTextColor(Color.BLACK);
            policaBookCount.setText("(" + Neki.BookCount + ")");
        }
        else{
            policaBookCount.setText("(0)");
        }

        return view;
    }

    @Override
    public Polica getItem(int position) {
        return super.getItem(position);
    }
}
