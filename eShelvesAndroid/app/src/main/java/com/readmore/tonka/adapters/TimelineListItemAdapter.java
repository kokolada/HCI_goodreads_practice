package com.readmore.tonka.adapters;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import com.readmore.tonka.eshelvesnavdrawer.R;

/**
 * Created by anton_000 on 22.4.2015..
 */
public class TimelineListItemAdapter extends ArrayAdapter<String>{

    public TimelineListItemAdapter(Context context, String[] values) {
        super(context, R.layout.timeline_list_item, values);

    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        LayoutInflater inflater = LayoutInflater.from(getContext());

        View view = inflater.inflate(R.layout.timeline_list_item, parent, false);

        String Neki = getItem(position);

        TextView theTextView = (TextView) view.findViewById(R.id.label);
        TextView theTextView2 = (TextView) view.findViewById(R.id.textviewtitle);

        theTextView.setText(Neki);
        theTextView2.setText(Neki);

        return view;
    }
}
