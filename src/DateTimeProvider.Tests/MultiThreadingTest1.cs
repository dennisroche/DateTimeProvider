using System;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable once CheckNamespace
namespace DateTimeProviders.Tests
{
    public class MultiThreadingTest1
    {
        public MultiThreadingTest1()
        {
            DateTimeProvider.Provider = new UtcDateTimeProvider();
        }

        [Fact]
        public void MoveTimeForward5HrsFromOffset0()
        {
            var testingWithDate = new DateTimeOffset(new DateTime(2014, 10, 01), TimeSpan.FromHours(0));
            using (var o = new OverrideDateTimeProvider(testingWithDate))
            {
                DateTimeProvider.Now.ToString().ShouldBe("1/10/2014 12:00:00 AM +00:00");
                o.MoveTimeForward(TimeSpan.FromHours(5));
                DateTimeProvider.Now.ToString().ShouldBe("1/10/2014 5:00:00 AM +00:00");
            }
        }
    }
}
