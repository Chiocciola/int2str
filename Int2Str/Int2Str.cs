using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Int2Str
{
    public static class Int2Str
    {
        public static readonly char[] twoDigitsTochar1 = new char[100];
        public static readonly char[] twoDigitsTochar2 = new char[100];

        public static readonly string[] _singleDigitStrings = new string[] {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};

        // Internal method that allocates string without initializtion
        // This method probably never be public to discourage mutation of strings
        // https://github.com/dotnet/runtime/issues/36989
        public static readonly Func<int, string> FastAllocateString = typeof(string).GetMethod("FastAllocateString", BindingFlags.Static | BindingFlags.NonPublic).CreateDelegate<Func<int, string>>();

        static Int2Str()
        {
            for(int i = 0; i < 100; i++)
            {
                twoDigitsTochar1[i] = (char)('0' + i / 10);
                twoDigitsTochar2[i] = (char)('0' + i % 10);
            }
        }


        // Extract single digit using division and reminder
        public static string Int2Str_Naive(int x)
        {
            if (x == 0)
            {
                return "0";
            }

            List<char> result = new List<char>();

            while (x != 0)
            {
                result.Add((char)('0' + (x % 10)));

                x /= 10;
            }

            result.Reverse();

            return new string(result.ToArray());
        }

        // Extract single digit using division and multiplication
        public static string Int2Str_NaiveOptimized(int x)
        {
            if (x == 0)
            {
                return "0";
            }

            List<char> result = new List<char>();

            while (x != 0)
            {
                x = Math.DivRem(x, 10, out var reminder);

                result.Add((char)('0' + reminder));
            }

            result.Reverse();

            return new string(result.ToArray());
        }

        // Detect string length using binary search
        // Allocate buffer
        // Extract single digit using division by 10
        public static unsafe string Int2Str_LengthBinSearch_Div10(long x)
        {
            var charCount = FindCharCount(x);

            if (charCount == 1)
            {
                return _singleDigitStrings[x];
            }

            var result = FastAllocateString(charCount);
            
            fixed (char* buffer = result)
            {
                char* p = buffer + charCount;

                do
                {
                    x = Math.DivRem(x, 10, out var remainder);

                    *(--p) = (char)('0' + remainder);
                } while (x != 0);
            };

            return result;
        }

        // Detect string length using binary search
        // Allocate buffer
        // Extract pair of digits using division by 100
        public static unsafe string Int2Str_LengthBinSearch_Div100(long x)
        {
            var charCount = FindCharCount(x);

            if (charCount == 1)
            {
                return _singleDigitStrings[x];
            }

            var result = FastAllocateString(charCount);
            
            fixed (char* buffer = result)
            {
                char* p = buffer + charCount;

                do
                {
                    x = Math.DivRem(x, 100, out var remainder);

                    *(--p) = twoDigitsTochar2[remainder];
                    *(--p) = twoDigitsTochar1[remainder];
                }
                while (x >= 100);

                if (x >= 10)
                {
                    *(--p) = twoDigitsTochar2[x];
                    *(--p) = twoDigitsTochar1[x];
                }
                else if (x > 0)
                {
                    *(--p) = (char)('0' + x);
                }
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FindCharCount(long x)
        {
            if ( x < 10000)
            {
                return (x < 100) 
                    ? x <   10 ? 1 : 2
                    : x < 1000 ? 3 : 4;
            }
           
            if (x < 1_000_000_000_000)
            {
               return  x < 100_000_000
                    ? (x <      1_000_000) ? (x <       100_000 ? 5 :  6) : (x <      10_000_000 ?  7 :  8)
                    : (x < 10_000_000_000) ? (x < 1_000_000_000 ? 9 : 10) : (x < 100_000_000_000 ? 11 : 12);
            }
            
            return  x < 10_000_000_000_000_000
                    ? (x <       100_000_000_000_000) ? (x <      10_000_000_000_000 ? 13 : 14) : (x <      1_000_000_000_000_000 ? 15 : 16)
                    : (x < 1_000_000_000_000_000_000) ? (x < 100_000_000_000_000_000 ? 17 : 18) : 19;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FindCharCount1(ulong x)
        {
            if ( x < 10000)
            {
                 return (x < 100) 
                    ? x <   10 ? 1 : 2
                    : x < 1000 ? 3 : 4;
            }

            return 
            (x < 1_000_000_000_000        
                ? x < 100_000_000
                    ? (x <      1_000_000) ? (x <       100_000 ? 5 :  6) : (x <      10_000_000 ?  7 :  8)
                    : (x < 10_000_000_000) ? (x < 1_000_000_000 ? 9 : 10) : (x < 100_000_000_000 ? 11 : 12)
                : x < 10_000_000_000_000_000
                    ? (x <       100_000_000_000_000) ? (x <      10_000_000_000_000 ? 13 : 14) : (x <      1_000_000_000_000_000 ? 15 : 16)
                    : (x < 1_000_000_000_000_000_000) ? (x < 100_000_000_000_000_000 ? 17 : 18) : (x < 10_000_000_000_000_000_000 ? 19 : 20));
        }
    }
}