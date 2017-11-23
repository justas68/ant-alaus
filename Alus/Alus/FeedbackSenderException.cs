using System;

namespace Alus
{
    public class FeedbackSenderException : Exception
    {
        public FeedbackSenderException()
        {
        }

        public FeedbackSenderException(string message)
            : base(message)
        {
        }

        public FeedbackSenderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
