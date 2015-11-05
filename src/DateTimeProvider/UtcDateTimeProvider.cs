namespace DateTimeProvider
{
    public class UtcDateTimeProvider : IDateTimeProvider
    {
        public System.DateTimeOffset Now
        {
            get { return System.DateTimeOffset.UtcNow; }
        }
    }
}