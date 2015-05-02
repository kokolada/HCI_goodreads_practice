package com.readmore.tonka.eshelvesnavdrawer;

import android.app.Activity;
import android.os.Bundle;
import android.support.v4.app.ListFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListAdapter;
import android.widget.ListView;

import com.readmore.tonka.adapters.TimelineListItemAdapter;

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
        View rootView = inflater.inflate(R.layout.timeline_fragment, container, false);

        String[] lista = {"jedan", "dva", "tri"};

        ListAdapter adapter = new TimelineListItemAdapter(getActivity(), lista);

        ListView theListView = (ListView) rootView.findViewById(R.id.timelineListView);
        theListView.setAdapter(adapter);

        return rootView;
    }

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        ((MainActivity) activity).onSectionAttached(
                getArguments().getInt(ARG_SECTION_NUMBER));
    }
}
