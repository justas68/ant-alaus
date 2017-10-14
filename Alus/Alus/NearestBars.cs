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
        public double lat;
        public double lon;
        public double lat2;
        public double lon2;
        public bool _ieskoti = true;
        public Location _location = new Alus.Location();
        public List<Bar> _barList;

        // use coordinates of faculty campus as default location
        public static Location defaultLocation = new Alus.Location(54.729714d, 25.263445d);

        public List<Bar> Location()
        {
            _location = Alus.Location.FindLocation(3, defaultLocation);
            lat = lat2 = _location.Latitude;
            lon = lon2 = _location.Longtitude;
            if (_ieskoti == true)
            {
                _barList = new List<Bar>();
            }
            string latlng = _location.ToString();

            if (_ieskoti == true)
            {
                using (var ms = NearbySearch(_location))
                {
                    _barList.AddRange(FindBars(ms).ToList());
                }
            }
            _ieskoti = false;
            return _barList;          
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
                        
                        yield return new Bar(result.Name, result.Geometry.Location.ToString(), result.Rating, result.Vicinity, result.OpeningHours);
                    }
                }
            }
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
