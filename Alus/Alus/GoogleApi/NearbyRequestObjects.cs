using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alus.GoogleApi
{
    public class Viewport
    {
        [JsonProperty("northeast")]
        public Location Northeast { get; set; }

        [JsonProperty("southwest")]
        public Location Southwest { get; set; }
    }

    public class Geometry
    {
        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("viewport")]
        public Viewport Viewport { get; set; }
    }

    public class OpeningHours
    {
        [JsonProperty("open_now")]
        public bool IsOpenNow { get; set; }

        [JsonProperty("weekday_text")]
        public IList<string> WeekDayText { get; set; }
    }

    public class Photo
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("html_attributions")]
        public IList<string> HtmlAttributions { get; set; }

        [JsonProperty("photo_reference")]
        public string PhotoReference { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }

    public class NearbyRequestResult
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
        public OpeningHours OpeningHours { get; set; }

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

    public class NearbyRequestResponse
    {
        [JsonProperty("html_attributions")]
        public IList<object> HtmlAttributes { get; set; }

        [JsonProperty("next_page_token")]
        public string NextPageToken { get; set; }

        [JsonProperty("results")]
        public IList<NearbyRequestResult> Results { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}