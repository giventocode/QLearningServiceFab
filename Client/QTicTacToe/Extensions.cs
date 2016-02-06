using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTicTacToe
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
}
