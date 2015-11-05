DateTimeProvider [![Build status](https://ci.appveyor.com/api/projects/status/fx8d5f2apa553gge?svg=true)](https://ci.appveyor.com/project/dennisroche/datatimeprovider) [![NuGet Version](http://img.shields.io/nuget/v/DateTimeProvider.svg?style=flat)](https://www.nuget.org/packages/DateTimeProvider/)
=============

Provides an interface IDataTimeProvider and static container. Implemented as a Portal Class Library (PCL) targeting Profile92 (portable-net4+netcore45+wpa81).

Inspired by blog post https://robdmoore.id.au/blog/2015/01/26/review-of-jimmy-bogard-holistic-testing/

How to use
=============

Install the [Nuget](https://www.nuget.org/packages/DateTimeProvider) package.

    Install-Package DateTimeProvider

Then use or replace in place of `DateTime` in your code.

```c#
var currentTime = DateTimeProvider.Now;
```

DateTimeProvider.Testing [![Build status](https://ci.appveyor.com/api/projects/status/fx8d5f2apa553gge?svg=true)](https://ci.appveyor.com/project/dennisroche/datatimeprovider) [![NuGet Version](http://img.shields.io/nuget/v/DateTimeProvider.Testing.svg?style=flat)](https://www.nuget.org/packages/DateTimeProvider.Testing/)
=============

Provides a static IDateTimeProvider and IDisposable Override so that time can be manipulated in Unit Tests

How to use
=============

Install the [Nuget](https://www.nuget.org/packages/DateTimeProvider.Testing) package into your **Unit Test** project

    Install-Package DateTimeProvider.Testing

In your tests, you need may need to manipulate time to verify your logic. This is easy using the `OverrideDateTimeProvider`.

```c#
using (var time = new OverrideDateTimeProvider())
{
    Console.WriteLine($"{DateTimeProvider.Now}");
    time.MoveTimeForward(TimeSpan.FromHours(5));
    Console.WriteLine($"{DateTimeProvider.Now}");
}
```
