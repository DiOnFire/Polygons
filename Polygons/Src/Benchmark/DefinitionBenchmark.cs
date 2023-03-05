using Polygons.Benchmark;
using Polygons.ConvexHull;
using Polygons.Shape;
using System;
using System.Collections.Generic;

namespace Polygons.Benchmark
{
    sealed class DefinitionBenchmark : IBenchmark
    {
        public long CalculateAfterElements(int count)
        {
            Console.WriteLine("Started definition benchmark...");
            List<Shape.Shape> shapes = ShapeGenerator.GenerateShapes(count);
            Console.WriteLine("Shapes were generated...");
            long startTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            ShapeManager manager = new ShapeManager(shapes);
            Definition definition = new Definition(null, manager);
            definition.Draw(false);
            Console.WriteLine("Ending definition benchmark...");
            long endTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            Console.WriteLine($"Finished after {endTime - startTime} ms");
            manager.Clear();
            return endTime - startTime;
        }
    }
}
