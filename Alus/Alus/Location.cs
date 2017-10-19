using System;
using System.Device.Location;
using System.Globalization;
using Newtonsoft.Json;

namespace Alus
{
    public struct Location : IEquatable<Location>
    {
        public static readonly Location Zero = default(Location);

        public Location(string str)
        {
            var coordinates = str.Split(',');
            Latitude = double.Parse(coordinates[0]);
            Longitude = double.Parse(coordinates[1]);
        }

        public Location(double latitude, double longtitude)
        {
            Latitude = latitude;
            Longitude = longtitude;
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
        public double Longitude { get; private set; }

        [JsonIgnore]
        public bool IsZero
        {
            get
            {
                return Latitude == 0.0d && Longitude == 0.0d;
            }
        }

        public override string ToString()
        {
            return string.Format($"{Latitude.ToString(CultureInfo.InvariantCulture)},{Longitude.ToString(CultureInfo.InvariantCulture)}");
        }

        public bool Equals(Location other)
        {
            return (
                Latitude == other.Latitude &&
                Longitude == other.Longitude
            );
        }

        public double this[int index]
        {
            get
            {
                if (index < 0 && index > 2)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                return index == 0 ? Latitude : Longitude;
            }
            set
            {
                if (index < 0 && index > 2)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                if (index == 0)
                {
                    Latitude = value;
                }
                else
                {
                    Longitude = value;
                }
            }
        }

        public static Location operator+(Location location, Vector2d vector)
        {
            return new Location(location.Latitude + vector.X, location.Longitude + vector.Y);
        }
    }
}
