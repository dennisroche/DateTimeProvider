using System;
using System.Threading;
using DateTimeProviders;

// ReSharper disable CheckNamespace
// Using global:: namespace

public static class DateTimeProvider
{
    private static readonly ThreadLocal<IDateTimeProvider> ProviderTheadLocal;

    static DateTimeProvider()
    {
        ProviderTheadLocal = new ThreadLocal<IDateTimeProvider>(() => new UtcDateTimeProvider());
    }

    public static DateTimeOffset Now => ProviderTheadLocal.Value.Now;
    public static DateTime LocalNow => ProviderTheadLocal.Value.Now.LocalDateTime;
    public static DateTime UtcNow => ProviderTheadLocal.Value.Now.UtcDateTime;

    public static IDateTimeProvider Provider
    {
        get => ProviderTheadLocal.Value;
        set => ProviderTheadLocal.Value = value;
    }
}