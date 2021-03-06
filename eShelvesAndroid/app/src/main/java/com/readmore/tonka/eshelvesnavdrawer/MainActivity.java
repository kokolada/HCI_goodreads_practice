package com.readmore.tonka.eshelvesnavdrawer;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.SearchManager;
import android.content.DialogInterface;
import android.content.SharedPreferences;
import android.support.v7.app.ActionBarActivity;
import android.support.v7.app.ActionBar;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.content.Context;
import android.os.Build;
import android.os.Bundle;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.support.v4.widget.DrawerLayout;
import android.widget.ArrayAdapter;
import android.widget.SearchView;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.google.gson.Gson;
import com.readmore.tonka.fragmentactivities.SearchFragment;
import com.readmore.tonka.helpers.Config;
import com.readmore.tonka.helpers.MyApp;
import com.readmore.tonka.helpers.MyVolley;
import com.readmore.tonka.helpers.Sesija;
import com.readmore.tonka.models.LogiraniKorisnik;

import org.apache.http.message.BasicNameValuePair;
import org.w3c.dom.Text;


public class MainActivity extends ActionBarActivity
        implements NavigationDrawerFragment.NavigationDrawerCallbacks {

    /**
     * Fragment managing the behaviors, interactions and presentation of the navigation drawer.
     */
    private NavigationDrawerFragment mNavigationDrawerFragment;

    /**
     * Used to store the last screen title. For use in {@link #restoreActionBar()}.
     */
    private CharSequence mTitle;

    String logiraniKorisnikString;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        mNavigationDrawerFragment = (NavigationDrawerFragment)
                getSupportFragmentManager().findFragmentById(R.id.navigation_drawer);
        mTitle = getTitle();

        // Set up the drawer.
        mNavigationDrawerFragment.setUp(
                R.id.navigation_drawer,
                (DrawerLayout) findViewById(R.id.drawer_layout));
    }

    public void logout(View v){
        FragmentManager fragmentManager = getSupportFragmentManager();
        Sesija.setLogiraniKorisnik(null);
        fragmentManager.beginTransaction()
                .replace(R.id.container, LoginFragment.newInstance(1))
                .commit();
    }

    public void login(View v){
        String url = Config.urlApi + "test";
        TextView username = (TextView)v.getRootView().findViewById(R.id.username);
        TextView password = (TextView)v.getRootView().findViewById(R.id.password);

        MyVolley.get(url, LogiraniKorisnik.class, new Response.Listener<LogiraniKorisnik>() {
            @Override
            public void onResponse(LogiraniKorisnik response) {
                if (response != null) {
                    Sesija.setLogiraniKorisnik(response);
                    ZamjeniFragment();
                } else {
                    Toast.makeText(getApplicationContext(), "pogresan username ili password", Toast.LENGTH_SHORT).show();
                }
            }
        }, null, new BasicNameValuePair("username", username.getText() + ""), new BasicNameValuePair("password", password.getText().toString()));
    }

    @Override
    public void onBackPressed() {
        if(Sesija.getDubina() == 0) {
            //super.onBackPressed();
            new AlertDialog.Builder(this)
                    .setIcon(android.R.drawable.ic_dialog_alert)
                    .setTitle("Exiting Application")
                    .setMessage("Are you sure you want to close eShelves?")
                    .setPositiveButton("Yes", new DialogInterface.OnClickListener()
                    {
                        @Override
                        public void onClick(DialogInterface dialog, int which) {
                            finish();
                        }

                    })
                    .setNegativeButton("No", null)
                    .show();
        }
        else {
            Sesija.IdiNazad(getSupportFragmentManager());
        }
    }

    void ZamjeniFragment(){
        FragmentManager fragmentManager = getSupportFragmentManager();
        int position = mNavigationDrawerFragment.getmCurrentSelectedPosition();
        switch(position){
            case 0: fragmentManager.beginTransaction()
                    .replace(R.id.container, TimelineFragment.newInstance(position + 1))
                    .commit();
                break;
            case 1: fragmentManager.beginTransaction()
                    .replace(R.id.container, ProfileFragment.newInstance(position + 1))
                    .commit();
                break;
            case 2: fragmentManager.beginTransaction()
                    .replace(R.id.container, MyBooksFragment.newInstance(position + 1))
                    .commit();
                break;
            case 3: fragmentManager.beginTransaction()
                    .replace(R.id.container, RecommendationsFragment.newInstance(position + 1))
                    .commit();
                break;
            case 4: fragmentManager.beginTransaction()
                    .replace(R.id.container, SettingsFragment.newInstance(position + 1))
                    .commit();
                break;
            default: fragmentManager.beginTransaction()
                    .replace(R.id.container, PlaceholderFragment.newInstance(position + 1))
                    .commit();
                break;
        }
    }

    @Override
    public void onNavigationDrawerItemSelected(int position) {
        // update the main content by replacing fragments
        FragmentManager fragmentManager = getSupportFragmentManager();
        if(Sesija.getLogiraniKorisnik() == null){
            fragmentManager.beginTransaction()
                    .replace(R.id.container, LoginFragment.newInstance(position + 1))
                    .commit();
        } else {
            Fragment fragment;
            switch(position){
                case 0: fragmentManager.beginTransaction()
                        .replace(R.id.container, fragment = TimelineFragment.newInstance(position + 1))
                        .commit();
                    Sesija.currentFragment = fragment;
                    break;
                case 1: fragmentManager.beginTransaction()
                        .replace(R.id.container, fragment = ProfileFragment.newInstance(position + 1, Sesija.getLogiraniKorisnik().Id))
                        .commit();
                    Sesija.currentFragment = fragment;
                    break;
                case 2: fragmentManager.beginTransaction()
                        .replace(R.id.container, fragment = MyBooksFragment.newInstance(position + 1))
                        .commit();
                    Sesija.currentFragment = fragment;
                    break;
                case 3: fragmentManager.beginTransaction()
                        .replace(R.id.container, fragment = RecommendationsFragment.newInstance(position + 1))
                        .commit();
                    Sesija.currentFragment = fragment;
                    break;
                case 4: fragmentManager.beginTransaction()
                        .replace(R.id.container, fragment = SettingsFragment.newInstance(position + 1))
                        .commit();
                    Sesija.currentFragment = fragment;
                    break;
                default: fragmentManager.beginTransaction()
                        .replace(R.id.container, PlaceholderFragment.newInstance(position + 1))
                        .commit();
                    break;
            }
        }
    }

    public void onSectionAttached(int number) {
        switch (number) {
            case 1:
                mTitle = getString(R.string.title_section1);
                break;
            case 2:
                mTitle = getString(R.string.title_section2);
                break;
            case 3:
                mTitle = getString(R.string.title_section3);
                break;
            case 4:
                mTitle = getString(R.string.title_section4);
                break;
            case 5:
                mTitle = getString(R.string.title_section5);
                break;
        }
    }

    public void restoreActionBar() {
        ActionBar actionBar = getSupportActionBar();
        actionBar.setNavigationMode(ActionBar.NAVIGATION_MODE_STANDARD);
        actionBar.setDisplayShowTitleEnabled(true);
        actionBar.setTitle(mTitle);
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        if (!mNavigationDrawerFragment.isDrawerOpen()) {
            // Only show items in the action bar relevant to this screen
            // if the drawer is not showing. Otherwise, let the drawer
            // decide what to show in the action bar.

            getMenuInflater().inflate(R.menu.main, menu);
            restoreActionBar();
            return true;
        }

        return super.onCreateOptionsMenu(menu);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }
        if(id == R.id.search){
            Sesija.UletiDublje(SearchFragment.newInstance(), getSupportFragmentManager());
        }

        return super.onOptionsItemSelected(item);
    }

    /**
     * A placeholder fragment containing a simple view.
     */
    public static class PlaceholderFragment extends Fragment {
        /**
         * The fragment argument representing the section number for this
         * fragment.
         */
        private static final String ARG_SECTION_NUMBER = "section_number";

        /**
         * Returns a new instance of this fragment for the given section
         * number.
         */
        public static PlaceholderFragment newInstance(int sectionNumber) {
            PlaceholderFragment fragment = new PlaceholderFragment();
            Bundle args = new Bundle();
            args.putInt(ARG_SECTION_NUMBER, sectionNumber);
            fragment.setArguments(args);
            return fragment;
        }

        public PlaceholderFragment() {
        }

        @Override
        public View onCreateView(LayoutInflater inflater, ViewGroup container,
                                 Bundle savedInstanceState) {
            View rootView = inflater.inflate(R.layout.fragment_main, container, false);
            return rootView;
        }

        @Override
        public void onAttach(Activity activity) {
            super.onAttach(activity);
            ((MainActivity) activity).onSectionAttached(
                    getArguments().getInt(ARG_SECTION_NUMBER));
        }
    }

}