package com.readmore.tonka.adapters;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import com.readmore.tonka.eshelvesnavdrawer.R;
import com.readmore.tonka.models.Knjiga;
import com.readmore.tonka.models.KnjigaDetaljiVM;

/**
 * Created by anton on 22.05.2015..
 */
public class ReviewsAdapter extends ArrayAdapter<KnjigaDetaljiVM.OcjenaInfoVM> {
    public ReviewsAdapter(Context context, KnjigaDetaljiVM.OcjenaInfoVM[] values){
        super(context, R.layout.review_list_item, values);
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        LayoutInflater inflater = LayoutInflater.from(getContext());

        View view = inflater.inflate(R.layout.review_list_item, parent, false);

        KnjigaDetaljiVM.OcjenaInfoVM Neki = getItem(position);

        TextView username = (TextView) view.findViewById(R.id.reviewerUsername);
        username.setText(Neki.username + " (" + Neki.Ocjena + ")");
        TextView opis = (TextView) view.findViewById(R.id.reviewerDescription);
        opis.setText(Neki.Opis);

        return view;
    }

    @Override
    public KnjigaDetaljiVM.OcjenaInfoVM getItem(int position) {
        return super.getItem(position);
    }
}
