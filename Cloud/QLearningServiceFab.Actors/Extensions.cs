using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Generator;

namespace QLearningServiceFab.Actors
{
    public static class Extensions
    {
        public static Task ForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> body)
        {
            return Task.WhenAll(
                from item in source
                select body(item));
        }

        public static bool IsOdd(this int n)
        {
            return n % 2 != 0;
        }
        public static bool IsEven(this int n)
        {
            return n % 2 == 0;
        }


        
    }

    public static class TaskUtils
    {
        public async static Task ExecuteWithRetry(Func<Task> func, int numberOfRetries, TimeSpan waitBetweenRetries)
        {
            var tries = 0;
            var eXs = new List<Exception>();
            do
            {
                try
                {
                    await func();

                    return;
                }
                catch (AggregateException ex)
                {
                    ActorEventSource.Current.Message("Retriable error message: {0} retries:{1}",ex.InnerException.Message,tries);
                    eXs.Add(ex);
                }
                await Task.Delay(waitBetweenRetries);
                tries++;
            } while (tries < numberOfRetries);

            ActorEventSource.Current.Message("Too many retries");
            throw new AggregateException("Too many retries.", eXs);
        }

    }
}
