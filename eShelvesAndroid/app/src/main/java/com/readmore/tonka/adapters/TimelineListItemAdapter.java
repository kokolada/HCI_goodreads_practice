package com.readmore.tonka.adapters;

import android.app.Activity;
import android.content.Context;
import android.support.v4.app.FragmentActivity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.RatingBar;
import android.widget.TextView;

import com.android.volley.Response;
import com.readmore.tonka.eshelvesnavdrawer.ProfileFragment;
import com.readmore.tonka.eshelvesnavdrawer.R;
import com.readmore.tonka.fragmentactivities.KnjigaDetailsFragment;
import com.readmore.tonka.helpers.Config;
import com.readmore.tonka.helpers.MyVolley;
import com.readmore.tonka.helpers.Sesija;
import com.readmore.tonka.models.Knjiga;
import com.readmore.tonka.models.TimelineListItemVM;

import org.apache.http.message.BasicNameValuePair;

/**
 * Created by anton_000 on 22.4.2015..
 */
public class TimelineListItemAdapter extends ArrayAdapter<TimelineListItemVM>{

    public TimelineListItemAdapter(Context context, TimelineListItemVM[] values) {
        super(context, R.layout.timeline_list_item, values);

    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        LayoutInflater inflater = LayoutInflater.from(getContext());

        View view = inflater.inflate(R.layout.timeline_list_item, parent, false);

        final TimelineListItemVM Neki = getItem(position);

        TextView theTextView = (TextView) view.findViewById(R.id.label);
        TextView theTextView2 = (TextView) view.findViewById(R.id.textviewtitle);
        TextView timelineuser = (TextView) view.findViewById(R.id.timelineuser);
        RatingBar rbar = (RatingBar) view.findViewById(R.id.timelineratingbar);
        rbar.setVisibility(View.GONE);
        Button btn = (Button) view.findViewById(R.id.addtoshelfbtn);
        btn.setVisibility(View.GONE);

        timelineuser.setText(Neki.username);
        timelineuser.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                FragmentActivity fa = (FragmentActivity) getContext();
                Sesija.UletiDublje(ProfileFragment.newInstance(Neki.KorisnikId, Neki.username), fa.getSupportFragmentManager());
            }
        });
        LinearLayout ll = (LinearLayout) view.findViewById(R.id.drugi);
        ll.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                final FragmentActivity fa = (FragmentActivity) getContext();
                MyVolley.get(Config.urlApi + "Knjigas", Knjiga.class, new Response.Listener<Knjiga>() {
                    @Override
                    public void onResponse(Knjiga response) {
                        Sesija.UletiDublje(KnjigaDetailsFragment.newInstance(response), fa.getSupportFragmentManager());
                    }
                }, null, new BasicNameValuePair("id", Neki.KnjigaId+""));
            }
        });
        theTextView.setText(" " + Neki.EventDescription + " " + Neki.Naslov + " sa ocjenom " + Neki.Ocjena);
        theTextView2.setText(Neki.Naslov);

        return view;
    }

    @Override
    public TimelineListItemVM getItem(int position) {
        return super.getItem(position);
    }
}
