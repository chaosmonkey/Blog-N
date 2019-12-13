using System;

namespace Blogn.Services
{
    public class DefaultTimeProvider : ITimeProvider
    {
        public DateTimeOffset NowUtc => DateTimeOffset.UtcNow;
    }
}
