using System;

namespace DateTimeProviders
{
    public class DeterministicDateTimeProvider : IDateTimeProvider
    {
        private readonly Func<DateTimeOffset, DateTimeOffset> _getNext;
        private DateTimeOffset _current;

        public DeterministicDateTimeProvider(
            DateTimeOffset seed,
            Func<DateTimeOffset, DateTimeOffset>? getNext = null)
        {
            _current = seed;
            _getNext = getNext ?? GetNextStrategies.Static;
        }

        public DateTimeOffset Now
        {
            get
            {
                var current = _current;
                _current = _getNext(current);
                return current;
            }
        }

        public static class GetNextStrategies
        {
            public static Func<DateTimeOffset, DateTimeOffset> Static { get; } = static v => v;

            public static Func<DateTimeOffset, DateTimeOffset> IncreasingSeconds { get; } = static v => v.AddSeconds(1);

            public static Func<DateTimeOffset, DateTimeOffset> IncreasingMinutes { get; } = static v => v.AddMinutes(1);

            public static Func<DateTimeOffset, DateTimeOffset> IncreasingHours { get; } = static v => v.AddHours(1);

            public static Func<DateTimeOffset, DateTimeOffset> IncreasingDays { get; } = static v => v.AddDays(1);
        }
    }
}
