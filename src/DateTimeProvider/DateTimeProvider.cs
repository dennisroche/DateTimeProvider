namespace DateTimeProvider
{
    public static class DateTimeProvider
    {
        static DateTimeProvider()
        {
            Provider = new UtcDateTimeProvider();
        }

        public static System.DateTimeOffset Now => Provider.Now;
        public static IDateTimeProvider Provider { get; set; }
    }
}