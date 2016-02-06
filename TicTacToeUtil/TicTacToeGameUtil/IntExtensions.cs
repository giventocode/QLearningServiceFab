using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToeGameUtil
{
    public static class IntExtensions
    {
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
