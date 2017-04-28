using DateTimeProvider;

namespace DateTimeProviders
{
    public class LocalDateTimeProvider : IDateTimeProvider
    {
        public System.DateTimeOffset Now => System.DateTimeOffset.Now;
    }
}