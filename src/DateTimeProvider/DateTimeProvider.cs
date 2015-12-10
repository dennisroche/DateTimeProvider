using DateTimeProviders;

// ReSharper disable CheckNamespace
// Using global:: namespace

public static class DateTimeProvider
{
    static DateTimeProvider()
    {
        Provider = new UtcDateTimeProvider();
    }

    public static System.DateTimeOffset Now => Provider.Now;
    public static System.DateTime LocalNow => Provider.Now.LocalDateTime;
    public static System.DateTime UtcNow => Provider.Now.UtcDateTime;

    public static IDateTimeProvider Provider { get; set; }
}