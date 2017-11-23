using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Alus.Core.Models;

namespace Alus
{
    public class FeedbackFileSender : IFeedbackSender
    {
        public string Filename { get; private set; }

        public FeedbackFileSender(string filename)
        {
            Filename = filename;
        }

        public Task SendAsync(Feedback feedback)
        {
            return Task.Run(() => File.AppendAllText(Filename, JsonConvert.SerializeObject(feedback) + "\n"));
        }
    }
}
