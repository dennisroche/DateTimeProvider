using System;
using DateTimeProviders;

// ReSharper disable CheckNamespace
// Using global:: namespace

public static class DateTimeProvider
{
    [ThreadStatic]
    private static IDateTimeProvider _provider;

    static DateTimeProvider()
    {
        Provider = new UtcDateTimeProvider();
    }

    public static DateTimeOffset Now => Provider.Now;
    public static DateTime LocalNow => Provider.Now.LocalDateTime;
    public static DateTime UtcNow => Provider.Now.UtcDateTime;

    public static IDateTimeProvider Provider { get => _provider; set => _provider = value; }
}