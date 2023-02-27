using System;
using System.Collections.Generic;

namespace Polygons.Benchmark
{
    sealed class JarvisBenchmark : IBenchmark 
    {
        public long CalculateAfterElements(int count)
        {
            List<Shape.Shape> shapes = ShapeGenerator.GenerateShapes(count);
            long startTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            return 0;
        }

        
    }
}
