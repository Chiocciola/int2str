using BenchmarkDotNet.Running;

namespace Int2Str.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Int2StrBenchmarks>();
        }
    }
}