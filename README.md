DateTimeProvider [![Build Master](https://ci.appveyor.com/api/projects/status/9rmer97iudefls62/branch/master?svg=true)](https://ci.appveyor.com/project/dennisroche/datetimeprovider) [![NuGet Version](http://img.shields.io/nuget/v/DateTimeProvider.svg?style=flat)](https://www.nuget.org/packages/DateTimeProvider/)
================

Provides an interface IDataTimeProvider and static container. Implemented as a Portal Class Library (PCL) targeting **Profile111** (portable-net45+netcore45+wpa81).

Inspired by blog post https://robdmoore.id.au/blog/2015/01/26/review-of-jimmy-bogard-holistic-testing/

`DateTimeProvider` exists in the `global::` namespace to make usage easier.


#### DateTimeOffset vs DateTime

The library provides `DateTimeOffset`, not `DateTime`. Why? Read this [excellent answer on StackOverflow](http://stackoverflow.com/a/14268167/73025)

`DateTimeOffset` is great, however there are use-cases where you need to use `DateTime` as Local or UTC.

You can use `DateTimeOffset.LocalDateTime` and `DateTimeOffset.UtcDateTime` properties. For convenience, our static `DateTimeProvider` has these properties as `LocalNow` and `UtcNow`.


How to use
=============

Install the [Nuget](https://www.nuget.org/packages/DateTimeProvider) package.

    Install-Package DateTimeProvider

Then use or replace in place of `DateTime` in your code.

```c#
var now = DateTimeProvider.Now;
var local = DateTimeProvider.LocalNow;
var utc = DateTimeProvider.UtcNow;
```

License
=============

MIT