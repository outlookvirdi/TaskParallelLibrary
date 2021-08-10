using System;
using System.Threading;
using System.Threading.Tasks;

namespace TPL.ParallelLoops
{
    public class ThreadLocalStorageDemo
    {
        private Random random = new Random();
        public void ThreadLocalStorageDemoFn()
        {
            // add numbers from 1 to 1000

            var sum = 0;
            Parallel.For(1, 1001, x =>
            {
                Console.Write($"[{x}] t={Task.CurrentId}\t");
                // sum += x;
                Interlocked.Add(ref sum, x); // concurrent access to sum from all these threads is inefficient
                // all tasks can add up their respective values, then add sum to total sum
            });
            Console.WriteLine($"\nSum of 1..100 = {sum}");

            sum = 0;
            Parallel.For(1, 1001,
                () => 0, // initialize local state, show code completion for next arg
                (x, state, tls) =>
                {
                    //if (x > 500)
                    //    state.Break();
                    //Console.WriteLine($"{Task.CurrentId}");
                    tls += x;
                    Console.WriteLine($"Task {Task.CurrentId} has sum {tls}");
                    return tls;
                },
                partialSum =>
                {
                    Console.WriteLine($"Partial value of task {Task.CurrentId} is {partialSum}");
                    Interlocked.Add(ref sum, partialSum);
                }
            );
            Console.WriteLine($"Sum of 1..100 = {sum}");
        }
    }
}