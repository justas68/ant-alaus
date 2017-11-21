using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alus.Core.Models
{
    public class Feedback
    {
        public Feedback()
        {
        }

        public Feedback(DatabaseFeedback feedback)
        {
            EMail = feedback.EMail;
            Text = feedback.Text;
            Type = feedback.Type;
        }

        [JsonProperty("email")]
        public string EMail { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FeedbackType Type { get; set; }
    }
}
