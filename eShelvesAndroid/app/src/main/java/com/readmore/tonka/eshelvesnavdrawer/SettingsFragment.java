package com.readmore.tonka.eshelvesnavdrawer;

import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.readmore.tonka.helpers.Sesija;
import com.readmore.tonka.models.LogiraniKorisnik;

/**
 * Created by anton_000 on 21.4.2015..
 */
public class SettingsFragment extends Fragment{

    private static final String ARG_SECTION_NUMBER = "section_number";

    public static SettingsFragment newInstance(int sectionNumber) {
        SettingsFragment fragment = new SettingsFragment();
        Bundle args = new Bundle();
        args.putInt(ARG_SECTION_NUMBER, sectionNumber);
        fragment.setArguments(args);
        return fragment;
    }

    public SettingsFragment() {}

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.settings_fragment, container, false);
        TextView userInfo = (TextView) rootView.findViewById(R.id.loguserInfo);
        LogiraniKorisnik k = Sesija.getLogiraniKorisnik();
        userInfo.setText(k.Id + " " + k.Ime + " " + k.Prezime + " " + k.Email + " " + k.username);
        return rootView;
    }

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        ((MainActivity) activity).onSectionAttached(
                getArguments().getInt(ARG_SECTION_NUMBER));
    }

}
