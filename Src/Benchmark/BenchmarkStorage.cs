using Polygons.ConvexHull;
using System.Collections.Generic;

namespace Polygons.Benchmark
{
    public class BenchmarkStorage
    {
        private readonly Dictionary<Algorithm, Dictionary<int, long>> _storage;

        public Dictionary<Algorithm, Dictionary<int, long>> Storage
        {
            get { return _storage; }
        }

        public BenchmarkStorage()
        {
            _storage = new Dictionary<Algorithm, Dictionary<int, long>>();
        }

        public void AddBenchmarkResult(Algorithm algorithm, Dictionary<int, long> results)
        {
            _storage.Add(algorithm, results);
        }
    }
}
