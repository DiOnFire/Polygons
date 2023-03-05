using System.Collections.Generic;

namespace Polygons.Benchmark
{
    public class Benchmark
    {
        private readonly short[] DELAYS;

        public Benchmark()
        {
            DELAYS = new short[] { 10, 20, 50, 100, 200, 300, 400, 500 };
        }

        public Dictionary<short, long> RunBenchmark(IBenchmark tool)
        {
            Dictionary<short, long> results = new Dictionary<short, long>();
            for (int i = 0; i < DELAYS.Length; i++)
            {
                long res = tool.CalculateAfterElements(DELAYS[i]);
                results.Add(DELAYS[i], res);
            }
            return results;
        }
    }
}
