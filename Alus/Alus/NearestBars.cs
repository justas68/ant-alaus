using Alus.GoogleApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Alus
{
    class NearestBars
    {
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
            if (_location == null)
            {
                _location = Alus.Location.FindLocation(3, defaultLocation);
            }

            var barList = new List<Bar>();

            using (var ms = NearbySearch(_location))
            {
                return FindBars(ms).ToList();
            }
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
                        
                        yield return new Bar(result.Name, result.Geometry.Location.ToString(), result.Rating, result.Vicinity, result.PlaceId, null);
                    }
                }
            }
        }

        public String GetStringFromUrl(string url)
        {
            return new WebClient().DownloadString(url);
        }

        public bool FindBarWorkingTime(String placeID)
        {
            string path = "https://maps.googleapis.com/maps/api/place/details/json?placeid=" + placeID + "&key=AIzaSyARqcyQXKX0gz1NG4ulXlDdnqDCNS_bJrU";
            string barJson = GetStringFromUrl(path);
            dynamic bar = JsonConvert.DeserializeObject(barJson);
            return bar.result.opening_hours.open_now;
        }

        public Stream NearbySearch(Location location)
        {
            string path = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&rankby=distance&type=bar&key=AIzaSyARqcyQXKX0gz1NG4ulXlDdnqDCNS_bJrU";
            return GetStreamFromUrl(path);
        }

        public Stream GetStreamFromUrl(string url)
        {
            using (WebClient wc = new WebClient())
            {
                return new MemoryStream(wc.DownloadData(url));
            }
        }
    }
}
