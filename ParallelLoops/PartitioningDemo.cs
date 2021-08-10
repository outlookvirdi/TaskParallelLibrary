using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace TPL.ParallelLoops
{
    public class PartitioningDemo
    {
        [Benchmark]
        public void SquareVanilla()
        {
            const int count = 100000;
            var values = Enumerable.Range(0, count);
            var results = new int[count];
            for (int i = 0; i < count; i++)
            {
                results[i] = (int) Math.Pow(i, 2);
            }

        }
        [Benchmark]
        public void SquareEachValue()
        {
            const int count = 100000;
            var values = Enumerable.Range(0, count);
            var results = new int[count];
            Parallel.ForEach(values, x => { results[x] = (int)Math.Pow(x, 2); });
        }

        [Benchmark]
        public void SquareEachValueChunked()
        {
            const int count = 100000;
            var values = Enumerable.Range(0, count);
            var results = new int[count];
            var part = Partitioner.Create(0, count, 10000); // rangeSize = size of each subrange

            Parallel.ForEach(part, range =>
            {
                for (var i = range.Item1; i < range.Item2; i++)
                {
                    results[i] = (int)Math.Pow(i, 2);
                }
            });
        }


        public void PartitioningDemoFn()
        {
            var summary = BenchmarkRunner.Run<PartitioningDemo>();
            Console.WriteLine(summary);
        }
    }
}