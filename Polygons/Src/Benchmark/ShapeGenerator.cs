using System;
using Polygons.Shape;
using System.Collections.Generic;

namespace Polygons.Benchmark
{
    sealed class ShapeGenerator
    {
        public static List<Shape.Shape> GenerateShapes(int count)
        {
            List<Shape.Shape> shapes = new List<Shape.Shape>();
            for (int i = 0; i < count; i++)
            {
                Random random = new Random();
                int x = random.Next(0, 1000);
                int y = random.Next(0, 1000);
                shapes.Add(new Circle(x, y));
            }
            return shapes;
        }
    }
}
