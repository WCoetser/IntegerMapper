using System;
using System.Collections.Generic;
using Trs.IntegerMapper.EqualityComparerIntegerMapper;
using Trs.IntegerMapper.Memoization;

namespace Trs.IntegerMapper.SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var intMapper = new EqualityComparerMapper<long>(EqualityComparer<long>.Default);
            var nullableIntMapper = new EqualityComparerMapper<long?>(EqualityComparer<long?>.Default);

            var memoizer = new Memoizer<long, long?>(intMapper, nullableIntMapper);

            long fast_fib(long n)
            {
                var existingSolution = memoizer.GetOutput(n);
                var returnValue = (n, existingSolution.HasValue) switch
                {
                    (0, _) => 0,
                    (1, _) => 1,
                    (_, false) => fast_fib(n - 1) + fast_fib(n - 2),
                    (_, true) => existingSolution.Value
                };
                memoizer.Memoize(n, returnValue);
                return returnValue;
            }

            for (int i = 0; i < 92; i++)
            {
                Console.WriteLine($"Fibonacci number {i + 1} is {fast_fib(i)}");
            }
        }
    }
}
