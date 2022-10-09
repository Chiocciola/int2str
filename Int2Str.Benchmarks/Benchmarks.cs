using System;
using System.Collections.Generic;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;

namespace Int2Str.Benchmarks
{

    [SimpleJob(launchCount: 1, warmupCount: 5, targetCount: 20)]   
    public class Int2StrBenchmarks
    {
        //[Params(111_222, 111_222_333_444, 111_222_333_444_555_666)]
        [ParamsSource(nameof(RandomValues))]
        public long N;

        public static IEnumerable<long> RandomValues
        {
            get
            {
                var rand = new Random();

                var vals = new long[10];

                for (int i = 0; i < vals.Length; i++)
                {
                    vals[i] = rand.NextInt64(long.MaxValue);
                }

                return vals;
            }
        }

        [Benchmark(Baseline = true)]
        public void Long_ToString()
        {
            N.ToString();
        }

        // [Benchmark]
        // public void Int2Str_Naive()
        // {
        //     Int2Str.Int2Str_Naive(N);
        // }

        // [Benchmark]
        // public void Int2Str_NaiveOptimized()
        // {
        //     Int2Str.Int2Str_NaiveOptimized(N);
        // }

        // [Benchmark]
        // public void BinSearch_Div10()
        // {
        //     Int2Str.Int2Str_LengthBinSearch_Div10(N);
        // }

        [Benchmark]
        public void BinSearch_Div100()
        {
            Int2Str.Int2Str_LengthBinSearch_Div100(N);
        }
    }
}
