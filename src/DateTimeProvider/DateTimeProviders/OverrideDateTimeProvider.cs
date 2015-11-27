using System;

// ReSharper disable once CheckNamespace
namespace DateTimeProviders
{
    public class OverrideDateTimeProvider : IDisposable
    {
        private readonly IDateTimeProvider _originalProvider;
        private readonly StaticDateTimeProvider _staticProvider;

        private OverrideDateTimeProvider(StaticDateTimeProvider staticProvider)
        {
            _staticProvider = staticProvider;
            _originalProvider = DateTimeProvider.Provider;

            DateTimeProvider.Provider = _staticProvider;
        }

        public OverrideDateTimeProvider()
            : this(new StaticDateTimeProvider())
        {
        }

        public OverrideDateTimeProvider(DateTimeOffset now)
            : this(new StaticDateTimeProvider(now))
        {
        }

        public OverrideDateTimeProvider(string now)
            : this(new StaticDateTimeProvider(now))
        {
        }

        public void Dispose()
        {
            DateTimeProvider.Provider = _originalProvider;
        }

        public OverrideDateTimeProvider SetNow(string now)
        {
            _staticProvider.SetNow(now);
            return this;
        }

        public OverrideDateTimeProvider MoveTimeForward(TimeSpan amount)
        {
            _staticProvider.MoveTimeForward(amount);
            return this;
        }
    }
}