package com.readmore.tonka.eshelvesnavdrawer;

import android.app.Activity;
import android.os.Bundle;
import android.support.v4.app.ListFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListAdapter;
import android.widget.ListView;

import com.android.volley.Response;
import com.readmore.tonka.adapters.TimelineListItemAdapter;
import com.readmore.tonka.helpers.Config;
import com.readmore.tonka.helpers.MyVolley;
import com.readmore.tonka.helpers.Sesija;
import com.readmore.tonka.models.TimelineListItemVM;

import org.apache.http.message.BasicNameValuePair;

/**
 * Created by anton_000 on 21.4.2015..
 */
public class TimelineFragment extends ListFragment {

    private static final String ARG_SECTION_NUMBER = "section_number";

    public static TimelineFragment newInstance(int sectionNumber) {
        TimelineFragment fragment = new TimelineFragment();
        Bundle args = new Bundle();
        args.putInt(ARG_SECTION_NUMBER, sectionNumber);
        fragment.setArguments(args);
        return fragment;
    }

    public TimelineFragment() {}

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        final View rootView = inflater.inflate(R.layout.timeline_fragment, container, false);

        MyVolley.get(Config.urlApi + "Timeline", TimelineListItemVM[].class, new Response.Listener<TimelineListItemVM[]>() {
            @Override
            public void onResponse(TimelineListItemVM[] response) {
                ListView theListView = (ListView) rootView.findViewById(R.id.timelineListView);
                ListAdapter adapter = new TimelineListItemAdapter(getActivity(), response);
                theListView.setAdapter(adapter);
            }
        }, null, new BasicNameValuePair("korisnikId", Sesija.getLogiraniKorisnik().Id+""));

        return rootView;
    }

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        ((MainActivity) activity).onSectionAttached(
                getArguments().getInt(ARG_SECTION_NUMBER));
    }
}
