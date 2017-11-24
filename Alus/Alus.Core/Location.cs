using System;
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
