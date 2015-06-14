package com.readmore.tonka.fragmentactivities;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.readmore.tonka.eshelvesnavdrawer.ProfileFragment;
import com.readmore.tonka.eshelvesnavdrawer.R;
import com.readmore.tonka.helpers.Sesija;
import com.readmore.tonka.models.KnjigaDetaljiVM;

/**
 * Created by anton on 01.06.2015..
 */
public class ReviewDetailsFragment extends Fragment{

    public static ReviewDetailsFragment newInstance(){
        ReviewDetailsFragment fragment = new ReviewDetailsFragment();

        return fragment;
    }

    public static ReviewDetailsFragment newInstance(KnjigaDetaljiVM.OcjenaInfoVM ocjena){
        ReviewDetailsFragment fragment = new ReviewDetailsFragment();
        Bundle bundle = new Bundle();
        bundle.putSerializable("ocjena", ocjena);
        fragment.setArguments(bundle);

        return fragment;
    }

    public ReviewDetailsFragment(){}

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.activity_review_details, container, false);

        TextView ocjenjivac = (TextView)rootView.findViewById(R.id.ocjenjivac);
        TextView opisocjene = (TextView)rootView.findViewById(R.id.opisocjene);
        TextView iznosocjene = (TextView) rootView.findViewById(R.id.iznosocjene);

        final KnjigaDetaljiVM.OcjenaInfoVM ocjena = (KnjigaDetaljiVM.OcjenaInfoVM) getArguments().getSerializable("ocjena");

        ocjenjivac.setText(ocjena.username);
        opisocjene.setText(ocjena.Opis);
        iznosocjene.setText(ocjena.Ocjena+"");

        ocjenjivac.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Sesija.UletiDublje(ProfileFragment.newInstance(ocjena.KorisnikId, ocjena.username), getActivity().getSupportFragmentManager());
            }
        });

        return rootView;
    }
}
