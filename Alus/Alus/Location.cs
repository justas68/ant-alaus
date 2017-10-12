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
        private double _lat;
        private double _lon;
        public Location()
        {
            _watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
            if (_watcher.TryStart(false, TimeSpan.FromSeconds(3)))
            {
                _lat = _watcher.Position.Location.Latitude;
                _lon = _watcher.Position.Location.Longitude;
            }
            else
            {
                _lat = 0;
                _lon = 0;
            }
        }
        public void FindLocation()
        {
            if (_watcher.TryStart(false, TimeSpan.FromSeconds(3)))
            {
                _lat = _watcher.Position.Location.Latitude;
                _lon = _watcher.Position.Location.Longitude;
            }
            else
            {
                _lat = 0;
                _lon = 0;
            }

        }
        public double Lat
        {
            get
            {
                return this._lat;
            }
        }
        public double Lon
        {
            get
            {
                return this._lon;
            }
        }


    }
}
