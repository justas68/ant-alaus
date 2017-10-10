using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Device.Location;

namespace Alus
{
    class Location
    {
        GeoCoordinateWatcher _watcher;
        private double lat;
        private double lon;
        public Location()
        {
            _watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
            if (_watcher.TryStart(false, TimeSpan.FromSeconds(3)))
            {
                lat = _watcher.Position.Location.Latitude;
                lon = _watcher.Position.Location.Longitude;
            }
            else
            {
                lat = 0;
                lon = 0;
            }
        }
        public void findLocation()
        {
            if (_watcher.TryStart(false, TimeSpan.FromSeconds(3)))
            {
                lat = _watcher.Position.Location.Latitude;
                lon = _watcher.Position.Location.Longitude;
            }
            else
            {
                lat = 0;
                lon = 0;
            }

        }
        public double Lat
        {
            get
            {
                return this.lat;
            }
        }
        public double Lon
        {
            get
            {
                return this.lon;
            }
        }


    }
}
