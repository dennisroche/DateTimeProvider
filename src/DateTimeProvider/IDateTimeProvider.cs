using System;

namespace DateTimeProvider
{
    public interface IDateTimeProvider
    {
        DateTimeOffset Now { get; }
    }
}