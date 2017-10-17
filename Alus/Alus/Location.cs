using System;
using System.Device.Location;
using System.Globalization;
using Newtonsoft.Json;

namespace Alus
{
    public struct Location : IEquatable<Location>
    {
        public static readonly Location Zero = default(Location);

        public Location(double latitude, double longtitude)
        {
            Latitude = latitude;
            Longtitude = longtitude;
        }

        private static GeoCoordinateWatcher _watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);

        public static Location FindLocation(int tries = 3, Location defaultLocation = default(Location))
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

            return defaultLocation;
        }

        [JsonProperty("lat")]
        public double Latitude { get; private set; }

        [JsonProperty("lng")]
        public double Longtitude { get; private set; }

        [JsonIgnore]
        public bool IsZero
        {
            get
            {
                return Latitude == 0.0d && Longtitude == 0.0d;
            }
        }

        public override string ToString()
        {
            return string.Format($"{Latitude.ToString(CultureInfo.InvariantCulture)},{Longtitude.ToString(CultureInfo.InvariantCulture)}");
        }

        public bool Equals(Location other)
        {
            return (
                Latitude == other.Latitude &&
                Longtitude == other.Longtitude
            );
        }
    }
}
