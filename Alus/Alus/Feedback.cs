using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alus
{
    public class Feedback
    {
        [JsonProperty("email")]
        public string EMail { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FeedbackType Type { get; set; }
    }
}
