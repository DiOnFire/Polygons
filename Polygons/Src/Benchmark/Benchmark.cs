using System.Collections.Generic;

namespace Polygons.Benchmark
{
    public class Benchmark
    {
        private readonly int[] DELAYS;

        public Benchmark()
        {
            DELAYS = new int[] { 10, 20, 50, 100, 200, 300, 400, 500 };
        }

        public Benchmark(int[] delays)
        {
            DELAYS = delays;
        }

        public Dictionary<int, long> RunBenchmark(IBenchmark tool)
        {
            Dictionary<int, long> results = new Dictionary<int, long>();
            for (int i = 0; i < DELAYS.Length; i++)
            {
                long res = tool.CalculateAfterElements(DELAYS[i]);
                results.Add(DELAYS[i], res);
            }
            return results;
        }
    }
}
