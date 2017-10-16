using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Alus
{
    public class FeedbackFileSender : IFeedbackSender
    {
        public static IFeedbackSender Instance { get; private set; }

        static FeedbackFileSender()
        {
            Instance = new FeedbackFileSender("feedback.txt");
        }

        public string Filename { get; private set; }

        public FeedbackFileSender(string filename)
        {
            Filename = filename;
        }

        public void Send(Feedback feedback)
        {
            File.AppendAllText(Filename, JsonConvert.SerializeObject(feedback) + "\n");
        }
    }
}
