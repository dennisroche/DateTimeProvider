using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

// ReSharper disable once CheckNamespace
namespace DateTimeProviders.Tests
{
    public class DateTimeProviderTest
    {
        [Fact]
        public void GivenNoContext_ThenWeShouldHaveCurrentDateTimeNow()
        {
            Assert.True((DateTimeProvider.Now - DateTime.Now).Milliseconds < 1);
        }

        [Fact]
        public void GivenTwoEmbeddedContexts_ThenEachContextShouldReturnItsSetValue()
        {
            var mockedOuter = new DateTime(2020, 01, 01);
            var mockedInner = new DateTime(2030, 01, 01);

            using (new OverrideDateTimeProvider(mockedOuter))
            {
                Assert.Equal(DateTimeProvider.Now, mockedOuter);

                using (new OverrideDateTimeProvider(mockedInner))
                {
                    Assert.Equal(DateTimeProvider.Now, mockedInner);
                }

                Assert.Equal(DateTimeProvider.Now, mockedOuter);
            }

            Assert.True((DateTimeProvider.Now - DateTime.Now).Milliseconds < 1);
        }

        [Fact]
        public void GivenTwoThreadWithDifferentContext_ThenEachThreadShouldHaveItsSetValue()
        {
            var mockedThread0Date = new DateTime(2020, 01, 01);
            var mockedThread1Date = new DateTime(2020, 01, 01);

            using var entrySynchro = new CountdownEvent(2);
            using var exitSynchro = new CountdownEvent(2);

            var t0 = new Thread(() =>
            {
                using (new OverrideDateTimeProvider(mockedThread0Date))
                {
                    entrySynchro.Signal();
                    entrySynchro.Wait();

                    Assert.Equal(DateTimeProvider.Now, mockedThread0Date);

                    exitSynchro.Signal();
                    exitSynchro.Wait();
                }
            });

            var t1 = new Thread(() =>
            {
                using (new OverrideDateTimeProvider(mockedThread1Date))
                {
                    entrySynchro.Signal();
                    entrySynchro.Wait();

                    Assert.Equal(DateTimeProvider.Now, mockedThread1Date);

                    exitSynchro.Signal();
                    exitSynchro.Wait();
                }
            });

            t0.Start();
            t1.Start();

            t0.Join();
            t1.Join();
        }

        [Fact]
        public async Task GivenTwoTasks_ThenEachTaskShouldHaveItsSetValue()
        {
            var date0 = new DateTime(2020, 01, 01);
            var date1 = new DateTime(2021, 12, 31);

            using var t0Signal = new SemaphoreSlim(0, 1);
            using var t1Signal = new SemaphoreSlim(0, 1);

            var t0 = new Task<Task<int>>(async () =>
            {
                using (new OverrideDateTimeProvider(date0))
                {
                    Assert.Equal(DateTimeProvider.Now, date0);
                    await t1Signal.WaitAsync().ConfigureAwait(continueOnCapturedContext: true);
                    t0Signal.Release();
                    Assert.Equal(DateTimeProvider.Now, date0);
                    await t1Signal.WaitAsync().ConfigureAwait(continueOnCapturedContext: true);
                    t0Signal.Release();
                }

                return 10;
            });

            var t1 = new Task<Task<int>>(async () =>
            {
                using (new OverrideDateTimeProvider(date1))
                {
                    Assert.Equal(DateTimeProvider.Now, date1);
                    t1Signal.Release();
                    await t0Signal.WaitAsync().ConfigureAwait(continueOnCapturedContext: true);
                    Assert.Equal(DateTimeProvider.Now, date1);
                    t1Signal.Release();
                    await t0Signal.WaitAsync().ConfigureAwait(continueOnCapturedContext: true);

                }

                return 10;
            });

            t0.RunSynchronously();
            t1.RunSynchronously();

            await Task.WhenAll(t0.Unwrap(), t1.Unwrap());
        }
    }
}
