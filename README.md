DateTimeProvider [![Build Master](https://ci.appveyor.com/api/projects/status/9rmer97iudefls62/branch/master?svg=true)](https://ci.appveyor.com/project/dennisroche/datetimeprovider) [![Build Release](https://ci.appveyor.com/api/projects/status/9rmer97iudefls62/branch/release?svg=true)](https://ci.appveyor.com/project/dennisroche/datetimeprovider) [![NuGet Version](http://img.shields.io/nuget/v/DateTimeProvider.svg?style=flat)](https://www.nuget.org/packages/DateTimeProvider/)
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

License
=============

MIT