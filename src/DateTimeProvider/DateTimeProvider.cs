using System;
using System.Threading;
using DateTimeProviders;

// ReSharper disable CheckNamespace
// Using global:: namespace

public static class DateTimeProvider
{
    private static readonly AsyncLocal<IDateTimeProvider> ProviderAsyncLocal;

    static DateTimeProvider()
    {
        ProviderAsyncLocal = new AsyncLocal<IDateTimeProvider>();
    }

    public static DateTimeOffset Now
    {
        get
        {
            if (ProviderAsyncLocal.Value is null)
            {
                ProviderAsyncLocal.Value = new UtcDateTimeProvider();
            }

            return ProviderAsyncLocal.Value.Now;
        }
    }

    public static DateTime LocalNow => Now.LocalDateTime;
    public static DateTime UtcNow => Now.UtcDateTime;

    public static IDateTimeProvider Provider
    {
        get => ProviderAsyncLocal.Value;
        set => ProviderAsyncLocal.Value = value;
    }
}
