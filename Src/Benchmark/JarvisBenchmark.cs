using Polygons.ConvexHull;
using Polygons.Shape;
using System;
using System.Collections.Generic;

namespace Polygons.Benchmark
{
    sealed class JarvisBenchmark : IBenchmark 
    {
        public long CalculateAfterElements(int count)
        {
            Console.WriteLine("Started jarvis benchmark...");
            List<Shape.Shape> shapes = ShapeGenerator.GenerateShapes(count);
            Console.WriteLine("Shapes were generated...");
            long startTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            ShapeManager manager = new ShapeManager(shapes);
            Jarvis jarvis = new Jarvis(null, manager);
            jarvis.Draw(null, false);
            long endTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            Console.WriteLine($"Finished after {endTime - startTime} ms");
            manager.Clear();
            return endTime - startTime;
        }
    }
}
