using System;
using System.Threading.Tasks;
using Alus.Client;
using Alus.Core.Models;

namespace Alus
{
    public class FeedbackSender : IFeedbackSender
    {
        private readonly AlusClient _client;

        public FeedbackSender(AlusClient client)
        {
            _client = client;
        }

        public async Task SendAsync(Feedback feedback)
        {
            try
            {
                await _client.AddAsync(feedback);
            }
            catch (ArgumentNullException ex) when (ex.Message == "Buffer cannot be null.")
            {
                // RestSharp is eating up the exception and spitting some
                // really misleading exception, so I just wrap the misleading
                // exception in an exception, which makes more sense
                throw new FeedbackSenderException("Can't connect to remote", ex);
            }
        }
    }
}
