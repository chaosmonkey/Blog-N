using System;

namespace Blogn.Models
{
    public class ResetToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateExpired { get; set; }
        public DateTimeOffset? DateConsumed { get; set; }

        public Credentials Credentials { get; set; }

        public bool IsValid(DateTimeOffset now)
        {
            return !string.IsNullOrWhiteSpace(Token) && DateExpired>=now && !DateConsumed.HasValue;
        }

        public void Consume(DateTimeOffset now)
        {
            DateConsumed = now;
            if (Credentials != null)
            {
                Credentials.DateUpdated = now;
            }
        }
    }
}
