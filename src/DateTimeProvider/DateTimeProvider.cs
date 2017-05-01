using System;
using System.Threading;
using DateTimeProviders;

// ReSharper disable CheckNamespace
// Using global:: namespace

public static class DateTimeProvider
{
    private static readonly ThreadLocal<IDateTimeProvider> ProviderThreadLocal;

    static DateTimeProvider()
    {
        ProviderThreadLocal = new ThreadLocal<IDateTimeProvider>(() => new UtcDateTimeProvider());
    }

    public static DateTimeOffset Now => ProviderThreadLocal.Value.Now;
    public static DateTime LocalNow => ProviderThreadLocal.Value.Now.LocalDateTime;
    public static DateTime UtcNow => ProviderThreadLocal.Value.Now.UtcDateTime;

    public static IDateTimeProvider Provider
    {
        get => ProviderThreadLocal.Value;
        set => ProviderThreadLocal.Value = value;
    }
}