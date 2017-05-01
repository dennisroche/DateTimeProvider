using System;
using System.Threading;
using DateTimeProviders;

// ReSharper disable CheckNamespace
// Using global:: namespace

public static class DateTimeProvider
{
    private static ThreadLocal<IDateTimeProvider> _provider;

    static DateTimeProvider()
    {
        _provider = new ThreadLocal<IDateTimeProvider>(() => new UtcDateTimeProvider());
    }

    public static DateTimeOffset Now => Provider.Now;
    public static DateTime LocalNow => Provider.Now.LocalDateTime;
    public static DateTime UtcNow => Provider.Now.UtcDateTime;

    public static IDateTimeProvider Provider { get => _provider.Value; set => _provider = new ThreadLocal<IDateTimeProvider>(() => value); }
}