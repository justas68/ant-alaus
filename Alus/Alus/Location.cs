using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Device.Location;
using System.Globalization;

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

        public static Location FindLocation(int tries = 1, Location defaultLocation = null)
        {
            for (int i = 0; i < tries; i++) {
                if (_watcher.TryStart(false, TimeSpan.FromSeconds(3)))
                {
                    if (_watcher.Position.Location.IsUnknown)
                    {
                        continue;
                    }
                    return new Location(
                        _watcher.Position.Location.Latitude,
                        _watcher.Position.Location.Longitude
                    );
                }
            }
            if (defaultLocation != null)
            {
                return defaultLocation;
            }
            return new Location();
        }
        public double Latitude { get; private set; }
        public double Longtitude { get; private set; }

        public bool IsZero
        {
            get
            {
                return Latitude == 0.0d && Longtitude == 0.0d;
            }
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, $"{Latitude},{Longtitude}");
        }
    }
}
