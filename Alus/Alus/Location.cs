using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Device.Location;

namespace Alus
{
    public class Location
    {
        public Location()
        {
        }

        public Location(double latitude, double longtitude)
        {
            Latitude = latitude;
            Longtitude = longtitude;
        }

        private static GeoCoordinateWatcher _watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);

        public static Location FindLocation()
        {
            if (_watcher.TryStart(false, TimeSpan.FromSeconds(3)))
            {
                return new Location(
                    _watcher.Position.Location.Latitude,
                    _watcher.Position.Location.Longitude
                );
            }
            else
            {
                return new Location();
            }

        }
        public double Latitude { get; private set; }
        public double Longtitude { get; private set; }
    }
}
