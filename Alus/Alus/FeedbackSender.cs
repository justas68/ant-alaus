﻿using System.Threading.Tasks;
using Alus.Client;
using Alus.Core.Models;

namespace Alus
{
    public class FeedbackSender : IFeedbackSender
    {
        public static FeedbackSender Instance { get; private set; }

        static FeedbackSender()
        {
            Instance = new FeedbackSender(new AlusClient());
        }

        private readonly AlusClient _client;

        public FeedbackSender(AlusClient client)
        {
            _client = client;
        }

        public async Task SendAsync(Feedback feedback)
        {
            await _client.AddAsync(feedback);
        }
    }
}
