namespace DateTimeProvider
{
    public class LocalDateTimeProvider : IDateTimeProvider
    {
        public System.DateTimeOffset Now => System.DateTimeOffset.Now;
    }
}