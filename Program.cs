using TPL.ParallelLinq;
using TPL.ParallelLoops;

namespace TPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // PARALLEL LOOPS
            new ParallelLoopsDemo().ParallelLoopsDemoFn();

            new BreakingAndStoppingDemo().BreakingAndStoppingDemoFn();

            new ThreadLocalStorageDemo().ThreadLocalStorageDemoFn();

            new PartitioningDemo().PartitioningDemoFn();

            //// PARALLEL LINQ

            //new AsParallelExample().AsParallelExampleFn();

            //new CancellationAndExceptions().CancellationAndExceptionsFn();

            //new MergeOptions().MergeOptionsFn();

            //new CustomAggregationDemo().CustomAggregationDemoFn();
        }
    }
}
