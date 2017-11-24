using System;

using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;

namespace BeerApplication
{
    [Activity(Label = "Nearest bars")]
    class NearestBars : Activity, IOnMapReadyCallback, ILocationListener
    {

        GoogleMap map;
        Location currentLocation;
        LocationManager locMgr;
        String provider;
        bool firstTime = true;

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
            SetContentView(Resource.Layout.activity_nearest_bars);
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

        public void OnLocationChanged(Location location)
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
                    CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
                    builder.Target(new LatLng(lat, lng));
                    CameraPosition cameraPosition = builder.Build();
                    cameraPosition.Zoom = 12;
                    CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
                    map.MoveCamera(cameraUpdate);
                    firstTime = false;
                }
                MarkerOptions marker = new MarkerOptions();
                marker.SetPosition(new LatLng(lat, lng));
                marker.SetTitle("Current location");
                marker.SetSnippet("That's the place you are in");
                map.AddMarker(marker);
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

    }
}