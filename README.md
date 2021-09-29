DateTimeProvider [![Build Master](https://ci.appveyor.com/api/projects/status/9rmer97iudefls62/branch/master?svg=true)](https://ci.appveyor.com/project/dennisroche/datetimeprovider) [![NuGet Version](http://img.shields.io/nuget/v/DateTimeProvider.svg?style=flat)](https://www.nuget.org/packages/DateTimeProvider/) [![.NET Standard 2.0](https://img.shields.io/badge/.NET%20Core-netstandard2.0-lightgrey.svg)](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md) [![.NET Standard 1.3](https://img.shields.io/badge/.NET%20Core-netstandard1.3-lightgrey.svg)](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard1.3.md) ![.NET Framework 4.5](https://img.shields.io/badge/.NET-4.5.2-lightgrey.svg)  [![Join the chat at https://gitter.im/DateTimeProvider/Lobby](https://badges.gitter.im/DateTimeProvider/Lobby.svg)](https://gitter.im/DateTimeProvider/Lobby?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
================

Provides an interface IDateTimeProvider and static container.

Targets `netstandard2.0`, `netstandard1.3` and `net45`.

`DateTimeProvider` exists in the `global::` namespace to make usage easier.


#### DateTimeOffset vs DateTime

The library provides `DateTimeOffset`, not `DateTime`. Why? Read this [excellent answer on StackOverflow](http://stackoverflow.com/a/14268167/73025)

`DateTimeOffset` is great, however there are use-cases where you need to use `DateTime` as Local or UTC.

You can use `DateTimeOffset.LocalDateTime` and `DateTimeOffset.UtcDateTime` properties. For convenience, our static `DateTimeProvider` has these properties as `LocalNow` and `UtcNow`.


How to use
=============

Install the [Nuget](https://www.nuget.org/packages/DateTimeProvider) package.

    Install-Package DateTimeProvider

Set the provider

```
DateTimeProvider.Provider = new UtcDateTimeProvider();
```

Or

```
DateTimeProvider.Provider = new LocalDateTimeProvider();
```

**NB** The default is `UtcDateTimeProvider`.

Then use or replace in place of `DateTime` in your code.

```c#
var now = DateTimeProvider.Now;
var local = DateTimeProvider.LocalNow;
var utc = DateTimeProvider.UtcNow;
```

Pinning DateTime
=============

Also provided is a static `IDateTimeProvider` and `IDisposable` Override so that time can be manipulated in a fixed scope.

Why is this useful?

- In a web request, you want to freeze time so that all your modified dates are aligned (i.e. not +/- by a few seconds)
- In unit tests when you need to verify business logic.

### Examples

#### Pinning Time in Logic

```c#
using (var o = new OverrideDateTimeProvider(DateTimeProvider.Now))
{
    // logic
}
```

#### Pinning Time in Unit Tests

In your tests, you need may need to manipulate time to verify your logic. This is easy using the `OverrideDateTimeProvider`.

```c#
Console.WriteLine($"{DateTimeProvider.Now}");

var testingWithDate = new DateTimeOffset(new DateTime(2014, 10, 01), TimeSpan.FromHours(8));
using (var o = new OverrideDateTimeProvider(testingWithDate))
{
    Console.WriteLine($"{DateTimeProvider.Now} (Testing)");
    o.MoveTimeForward(TimeSpan.FromHours(5));
    Console.WriteLine($"{DateTimeProvider.Now} (+ 5 hours)");
}

Console.WriteLine($"{DateTimeProvider.Now} (Restored)");
```

Output

```
6/11/2015 5:08:12 AM +00:00
1/10/2014 12:00:00 AM +08:00 (Testing)
1/10/2014 5:00:00 AM +08:00 (+ 5 hours)
6/11/2015 5:08:12 AM +00:00 (Restored)
```


DateTimeProvider.Analyser [![NuGet Version](http://img.shields.io/nuget/v/DateTimeProvider.Analyser.svg?style=flat)](https://www.nuget.org/packages/DateTimeProvider.Analyser/)
================

Also available is a Roslyn Analyser that will suggest replacements for `DateTime` for `DateTimeProvider`.

Install the [Nuget](https://www.nuget.org/packages/DateTimeProvider.Analyser) package.

    Install-Package DateTimeProvider.Analyser


License
=============

MIT
