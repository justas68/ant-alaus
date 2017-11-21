using System;

namespace Alus.Core.Models
{
    public class DatabaseFeedback : Feedback
    {
        public long Id { get; set; }
        public DateTime Created { get; set; }
    }
}
