using System;

namespace DateTimeProviders
{
    public class StaticDateTimeProvider : IDateTimeProvider
    {
        private DateTimeOffset _now;

        public StaticDateTimeProvider()
        {
            _now = DateTimeOffset.UtcNow;
        }

        public StaticDateTimeProvider(DateTimeOffset now)
        {
            _now = now;
        }

        public StaticDateTimeProvider(string now)
        {
            _now = DateTimeOffset.Parse(now);
        }

        public DateTimeOffset Now
        {
            get { return _now; }
        }

        public StaticDateTimeProvider SetNow(string now)
        {
            _now = DateTimeOffset.Parse(now);
            return this;
        }

        public StaticDateTimeProvider MoveTimeForward(TimeSpan amount)
        {
            _now = _now.Add(amount);
            return this;
        }
    }
}