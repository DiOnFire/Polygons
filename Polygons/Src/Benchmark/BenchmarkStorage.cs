using Polygons.ConvexHull;
using System.Collections.Generic;

namespace Polygons.Benchmark
{
    public class BenchmarkStorage
    {
        private readonly Dictionary<Algorithm, Dictionary<short, long>> _storage;

        public Dictionary<Algorithm, Dictionary<short, long>> Storage
        {
            get { return _storage; }
        }

        public BenchmarkStorage()
        {
            _storage = new Dictionary<Algorithm, Dictionary<short, long>>();
        }

        public void AddBenchmarkResult(Algorithm algorithm, Dictionary<short, long> results)
        {
            _storage.Add(algorithm, results);
        }
    }
}
