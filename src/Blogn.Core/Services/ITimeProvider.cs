using System;

namespace Blogn.Services
{
    public interface ITimeProvider
    {
        DateTimeOffset NowUtc { get; }
    }
}