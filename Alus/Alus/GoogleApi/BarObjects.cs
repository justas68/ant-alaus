using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alus.GoogleApi
{
    public class OpeningHour
    {
        [JsonProperty("open_now")]
        public bool IsOpenNow { get; set; }

        [JsonProperty("weekday_text")]
        public IList<string> WeekDayText { get; set; }
    }

    public class BarRequestResult
    {
        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("opening_hours")]
        public OpeningHour OpeningHour { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("types")]
        public IList<string> Types { get; set; }

        [JsonProperty("vicinity")]
        public string Vicinity { get; set; }

        [JsonProperty("photos")]
        public IList<Photo> Photos { get; set; }
    }

    public class BarRequest
    {
        [JsonProperty("html_attributions")]
        public IList<object> HtmlAttributes { get; set; }

        [JsonProperty("result")]
        public BarRequestResult Results { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
