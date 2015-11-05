namespace DateTimeProvider
{
    public class LocalDateTimeProvider : IDateTimeProvider
    {
        public System.DateTimeOffset Now
        {
            get { return System.DateTimeOffset.Now; }
        }
    }
}