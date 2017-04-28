namespace DateTimeProvider
{
    public class UtcDateTimeProvider : IDateTimeProvider
    {
        public System.DateTimeOffset Now => System.DateTimeOffset.UtcNow;
    }
}