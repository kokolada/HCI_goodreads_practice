package com.readmore.tonka.eshelvesnavdrawer;

import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import org.w3c.dom.Text;

/**
 * Created by anton_000 on 22.4.2015..
 */
public class LoginFragment extends Fragment{

    private static final String ARG_SECTION_NUMBER = "section_number";
    private EditText username;
    private EditText password;
    private Button loginBtn;


    public static LoginFragment newInstance(int sectionNumber) {
        LoginFragment fragment = new LoginFragment();
        Bundle args = new Bundle();
        args.putInt(ARG_SECTION_NUMBER, sectionNumber);
        fragment.setArguments(args);
        return fragment;
    }

    public LoginFragment() {}

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.login_fragment, container, false);
      //  username = (EditText)rootView.findViewById(R.id.username);
      //  password = (EditText)rootView.findViewById(R.id.password);
      //  loginBtn = (Button)rootView.findViewById(R.id.loginBtn);
        //final TextView ltv = (TextView)rootView.findViewById(R.id.loginTextView);
/*
        loginBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                FragmentManager fragmentManager = getActivity().getSupportFragmentManager();
                ltv.setText("batn kliknut");
                SharedPreferences settings = getActivity().getSharedPreferences("logpref", Context.MODE_PRIVATE);
                SharedPreferences.Editor editor = settings.edit();
                editor.putString("logiraniKorisnik", "dzemo");
                editor.commit();
                fragmentManager.beginTransaction()
                        .replace(R.id.container, TimelineFragment.newInstance(1))
                        .commit();
            }
        });*/

        return rootView;
    }

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        ((MainActivity) activity).onSectionAttached(
                getArguments().getInt(ARG_SECTION_NUMBER));
    }
}
