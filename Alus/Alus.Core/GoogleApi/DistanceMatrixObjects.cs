using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alus.GoogleApi
{
    public class ValueElement
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }
    }

    public class Element
    {
        [JsonProperty("distance")]
        public ValueElement Distance { get; set; }

        [JsonProperty("duration")]
        public ValueElement Duration { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class Row
    {
        [JsonProperty("elements")]
        public IList<Element> Elements { get; set; }
    }

    public class DistanceMatrixRequest
    {
        [JsonProperty("destination_addresses")]
        public IList<string> DesinationAddresses { get; set; }

        [JsonProperty("origin_addresses")]
        public IList<string> OriginAddresses { get; set; }

        [JsonProperty("rows")]
        public IList<Row> Rows { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

}