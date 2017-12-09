using System;

using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Alus.Core;
using Alus.Client;
using System.Collections.Generic;
using Alus.Core.Models;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;
using Android.Views;

namespace BeerApplication
{
    [Activity(Label = "Nearest bars", Theme = "@style/Theme.DesignDemo")]
    public class NearestBars : Activity, IOnMapReadyCallback, ILocationListener
    {

        GoogleMap map;
        public static Location currentLocation;
        LocationManager locMgr;
        String provider;
        bool firstTime = true;
        float[] hue = new float[22];
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        List<Bar> bars;

        public void OnMapReady(GoogleMap googleMap)
        {
            map = googleMap;
            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            googleMap.MoveCamera(CameraUpdateFactory.ZoomIn());
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Random rand = new Random();
            for (int i = 0; i < 22; i++)
            {
                hue[i] = rand.Next(0, 360);
            }
            SetContentView(Resource.Layout.activity_nearest_bars);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            setupDrawerContent(navigationView);
            MapFragment mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);
            locMgr = (LocationManager)GetSystemService(LocationService);
            Criteria crit = new Criteria
            {
                Accuracy = Accuracy.Coarse,
                PowerRequirement = Power.Medium
            };
            provider = locMgr.GetBestProvider(crit, true);
        }

        protected override void OnResume()
        {
            base.OnResume();
            locMgr.RequestLocationUpdates(provider, 0, 0, this);
        }

        protected override void OnPause()
        {
            base.OnPause();
            locMgr.RemoveUpdates(this);
        }

        public void OnLocationChanged(Android.Locations.Location location)
        {
            currentLocation = location;
            if (currentLocation == null)
            {
                MarkerOptions markerTemp = new MarkerOptions();
                markerTemp.SetPosition(new LatLng(54.73157, 25.26187));
                markerTemp.SetTitle("Did not find");
                map.AddMarker(markerTemp);
            }
            else
            {
                Double lat, lng;
                lat = currentLocation.Latitude;
                lng = currentLocation.Longitude;
                if (firstTime == true)
                {
                    MarkerOptions marker = new MarkerOptions();
                    marker.SetPosition(new LatLng(lat, lng));
                    marker.SetTitle("Current location");
                    marker.SetSnippet("That's the place you are in");
                    map.AddMarker(marker);
                    CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
                    builder.Target(new LatLng(lat, lng));
                    CameraPosition cameraPosition = builder.Build();
                    cameraPosition.Zoom = 12;
                    CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
                    map.MoveCamera(cameraUpdate);
                    firstTime = false;
                    Alus.NearestBars findBars = new Alus.NearestBars(new AndroidLocationFinder());
                    bars = findBars.FindBars();
                    int i = 0;
                    foreach (var bar in bars)
                    {
                        MarkerOptions tempMarker = new MarkerOptions();
                        String[] cords = bar.Coordinates.Split(',');
                        tempMarker.SetPosition(new LatLng(Convert.ToDouble(cords[0], System.Globalization.CultureInfo.InvariantCulture), Convert.ToDouble(cords[1], System.Globalization.CultureInfo.InvariantCulture)));
                        tempMarker.SetTitle(bar.Name);
                        tempMarker.SetSnippet(bar.Address);
                        tempMarker.SetIcon(BitmapDescriptorFactory.DefaultMarker(hue[i]));
                        map.AddMarker(tempMarker);
                        i++;
                        var menu = navigationView.Menu;
                        var menuItem = menu.FindItem(2131362006 + i);
                        menuItem.SetTitle(bar.Name);
                    }
                }
            }
        }

        public void OnProviderDisabled(string provider)
        {
        }

        public void OnProviderEnabled(string provider)
        {
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
        }

        void setupDrawerContent(NavigationView navigationView)
        {
            navigationView.NavigationItemSelected += (sender, e) =>
            {
                LatLng cord;
                e.MenuItem.SetChecked(true);
                if (e.MenuItem.ItemId == 2131362006)
                {
                    Double lat, lng;
                    lat = currentLocation.Latitude;
                    lng = currentLocation.Longitude;
                    cord = new LatLng(lat, lng);
                }
                else
                {
                    var bar = bars[e.MenuItem.ItemId - 2131362007];
                    String[] cords = bar.Coordinates.Split(',');
                    cord = new LatLng(Convert.ToDouble(cords[0], System.Globalization.CultureInfo.InvariantCulture), Convert.ToDouble(cords[1], System.Globalization.CultureInfo.InvariantCulture));
                }
                CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
                builder.Target(cord);
                CameraPosition cameraPosition = builder.Build();
                cameraPosition.Zoom = 18;
                CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
                map.MoveCamera(cameraUpdate);
                drawerLayout.CloseDrawers();
            };
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            navigationView.InflateMenu(Resource.Menu.nav_menu); //Navigation Drawer Layout Menu Creation  
            return true;
        }
    }
}
