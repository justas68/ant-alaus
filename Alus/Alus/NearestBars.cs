using Alus.GoogleApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Drawing;
using Alus.Core.Models;

namespace Alus
{
    public class NearestBars
    {
        private static string key = "AIzaSyARqcyQXKX0gz1NG4ulXlDdnqDCNS_bJrU";

        private Location _location;

        public Location Location
        {
            get {
                return _location;
            }
        }

        // use coordinates of faculty campus as default location
        public static Location defaultLocation = new Alus.Location(54.729714d, 25.263445d);

        public List<Bar> FindBars()
        {
            if (_location.IsZero)
            {
                _location = Alus.Location.FindLocation(defaultLocation: defaultLocation);
            }

            var barList = new List<Bar>();

            using (var ms = NearbySearch(_location))
            {
                barList.AddRange(FindBars(ms).ToList());
            }

            return barList;
        }

        public IEnumerable<Bar> FindBars(Stream stream)
        {
            using (var reader = new JsonTextReader(new StreamReader(stream)))
            {
                var serializer = new JsonSerializer();
                var response = serializer.Deserialize<NearbyRequestResponse>(reader);
                if (response.Status == "OK")
                {
                    foreach (var result in response.Results)
                    {
                        yield return new Bar(result.Name, result.Geometry.Location.ToString(), result.Rating, result.Vicinity, result.PlaceId, null, 0, 0);
                    }
                }
            }
        }

        public String GetStringFromUrl(string url)
        {
            return new WebClient().DownloadString(url);
        }

        public GoogleApi.OpeningHour FindBarWorkingTime(String placeID)
        {
            string path = $"https://maps.googleapis.com/maps/api/place/details/json?placeid={placeID}&key={key}";
            using (var reader = new JsonTextReader(new StreamReader(GetStreamFromUrl(path))))
            {
                var serializer = new JsonSerializer();
                var response = serializer.Deserialize<BarRequest>(reader);
                if (response.Status == "OK")
                {
                   return response.Results.OpeningHour;
                }
                else
                {
                    return null;
                }
            }
        }

        public Stream NearbySearch(Location location)
        {
            string path = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={location}&rankby=distance&type=bar&key={key}";
            return GetStreamFromUrl(path);
        }

        public Stream GetStreamFromUrl(string url)
        {
            using (WebClient wc = new WebClient())
            {
                return new MemoryStream(wc.DownloadData(url));
            }
        }

        public Stream GetMap(MapRequest request, IEnumerable<Label> labels, IEnumerable<Location> directions = null)
        {
            string size = $"{request.Size.Width}x{request.Size.Height}";
            string path = $"https://maps.googleapis.com/maps/api/staticmap?key={key}&center={request.Center}&zoom={request.Zoom}&size={size}";

            var markers = labels.Select(label => $"markers=color:{HexConverter(label.Color)}%7Clabel:{label.Name}%7C{label.Location}");

            path = path + "&" + string.Join("&", markers);

            if (directions != null)
            {
                path = path + "&path=color:0x0000ff80|weight:3|" + string.Join("|", directions.Select(l => l.ToString()));
                path = path + "+&sensor=true";
            }

            return GetStreamFromUrl(path);
        }

        private static string HexConverter(Color c)
        {
            return "0x" + c.R.ToString("x2") + c.G.ToString("x2") + c.B.ToString("x2");
        }

        public Alus.GoogleApi.Element GetDistanceElement(Location origin, Location destination)
        {
            string url = $"https://maps.googleapis.com/maps/api/distancematrix/json?units=metric&origins={origin}&destinations={destination}&key={key}";
            using (var reader = new JsonTextReader(new StreamReader(GetStreamFromUrl(url))))
            {
                var serializer = new JsonSerializer();
                var response = serializer.Deserialize<DistanceMatrixRequest>(reader);
                if (response.Status == "OK")
                {
                    return response.Rows[0].Elements[0];
                }
                else
                {
                    return null;
                }
            }
        }

        private GoogleApi.Route GetRoute(Location origin, Location destination)
        {
            string url = $"https://maps.googleapis.com/maps/api/directions/json?origin={origin}&destination={destination}&key={key}";
            using (var reader = new JsonTextReader(new StreamReader(GetStreamFromUrl(url))))
            {
                var serializer = new JsonSerializer();
                var response = serializer.Deserialize<GoogleApi.DirectionsRequestResponse>(reader);
                if (response.Status == "OK")
                {
                    return response.Routes[response.Routes.Count() - 1];
                }
                return null;
            }
        }

        public IEnumerable<Location> GetDirections(Location origin, Location destination)
        {
            var element = GetDistanceElement(origin, destination);
            return Decode(GetRoute(origin, destination).OverviewPolyline.Points);
        }

        private IEnumerable<Location> Decode(string polylineString)
        {
            if (string.IsNullOrEmpty(polylineString))
            {
                throw new ArgumentNullException(nameof(polylineString));
            }

            var polylineChars = polylineString.ToCharArray();
            var index = 0;
            var currentLat = 0;
            var currentLng = 0;
            while (index < polylineChars.Length)
            {
                // Next lat
                var sum = 0;
                var shifter = 0;
                int nextFiveBits;
                do
                {
                    nextFiveBits = polylineChars[index++] - 63;
                    sum |= (nextFiveBits & 31) << shifter;
                    shifter += 5;
                } while (nextFiveBits >= 32 && index < polylineChars.Length);

                if (index >= polylineChars.Length)
                {
                    break;
                }
                currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
                // Next lng
                sum = 0;
                shifter = 0;
                do
                {
                    nextFiveBits = polylineChars[index++] - 63;
                    sum |= (nextFiveBits & 31) << shifter;
                    shifter += 5;
                } while (nextFiveBits >= 32 && index < polylineChars.Length);

                if (index >= polylineChars.Length && nextFiveBits >= 32)
                {
                    break;
                }
                currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
                yield return new Location(Convert.ToDouble(currentLat) / 1E5, Convert.ToDouble(currentLng) / 1E5);
            }
        }
    }

    public class MapRequest
    {
        public Location Center { get; set; }
        public int Zoom { get; set; }
        public Size Size { get; set; }

    }

    public class Label
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public Location Location { get; set; }
    }
}
