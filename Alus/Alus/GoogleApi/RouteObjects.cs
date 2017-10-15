using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alus.GoogleApi
{
    public class GeocodedWaypoint
    {
        [JsonProperty("geocoder_status")]
        public string GeocoderStatus { get; set; }

        [JsonProperty("place_id")]
        public string place_id { get; set; }

        [JsonProperty("types")]
        public IList<string> types { get; set; }
    }

    public class Bounds
    {
        [JsonProperty("northeast")]
        public Location Northeast { get; set; }

        [JsonProperty("southwest")]
        public Location Southwest { get; set; }
    }


    public class Polyline
    {

        [JsonProperty("points")]
        public string points { get; set; }
    }

    public class Step
    {

        [JsonProperty("distance")]
        public ValueElement distance { get; set; }

        [JsonProperty("duration")]
        public ValueElement duration { get; set; }

        [JsonProperty("end_location")]
        public Location EndLocation { get; set; }

        [JsonProperty("html_instructions")]
        public string html_instructions { get; set; }

        [JsonProperty("polyline")]
        public Polyline polyline { get; set; }

        [JsonProperty("start_location")]
        public Location StartLocation { get; set; }

        [JsonProperty("travel_mode")]
        public string TravelMode { get; set; }

        [JsonProperty("maneuver")]
        public string Maneuver { get; set; }
    }

    public class Leg
    {

        [JsonProperty("distance")]
        public ValueElement distance { get; set; }

        [JsonProperty("duration")]
        public ValueElement duration { get; set; }

        [JsonProperty("end_address")]
        public string end_address { get; set; }

        [JsonProperty("end_location")]
        public Location end_location { get; set; }

        [JsonProperty("start_address")]
        public string start_address { get; set; }

        [JsonProperty("start_location")]
        public Location start_location { get; set; }

        [JsonProperty("steps")]
        public IList<Step> steps { get; set; }

        [JsonProperty("traffic_speed_entry")]
        public IList<object> traffic_speed_entry { get; set; }

        [JsonProperty("via_waypoint")]
        public IList<object> via_waypoint { get; set; }
    }

    public class OverviewPolyline
    {
        [JsonProperty("points")]
        public string points { get; set; }
    }

    public class Route
    {
        [JsonProperty("bounds")]
        public Bounds bounds { get; set; }

        [JsonProperty("copyrights")]
        public string copyrights { get; set; }

        [JsonProperty("legs")]
        public IList<Leg> legs { get; set; }

        [JsonProperty("overview_polyline")]
        public OverviewPolyline overview_polyline { get; set; }

        [JsonProperty("summary")]
        public string summary { get; set; }

        [JsonProperty("warnings")]
        public IList<object> warnings { get; set; }

        [JsonProperty("waypoint_order")]
        public IList<object> waypoint_order { get; set; }
    }

    public class DirectionsRequestResponse
    {

        [JsonProperty("geocoded_waypoints")]
        public IList<GeocodedWaypoint> GeocodedWaypoints { get; set; }

        [JsonProperty("routes")]
        public IList<Route> Routes { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
