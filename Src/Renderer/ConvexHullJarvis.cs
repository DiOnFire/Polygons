using System;
using System.Collections.Generic;
using System.Drawing;

namespace Polygons
{
    sealed class ConvexHullJarvis
    {
        public static double CalculateCos(
            Shape.Shape vectorStart, 
            Shape.Shape vectorEnd,
            Shape.Shape potentialPoint)
        {
            double bc = Math.Sqrt((vectorEnd.X - potentialPoint.X) * (vectorEnd.X - potentialPoint.X)
                +
                (vectorEnd.Y - potentialPoint.Y) * (vectorEnd.Y - potentialPoint.Y));

            double ac = Math.Sqrt((vectorStart.X - potentialPoint.X) * (vectorStart.X - potentialPoint.X)
                +
                (vectorStart.Y - potentialPoint.Y) * (vectorStart.Y - potentialPoint.Y));

            double ab = Math.Sqrt((vectorStart.X - vectorEnd.X) * (vectorStart.X - vectorEnd.X)
                +
                (vectorStart.Y - vectorEnd.Y) * (vectorStart.Y - vectorEnd.Y));

            double cos = (bc * bc - ac * ac + ab * ab) / (2 * bc * ab);
            return cos;
        }

        public static List<Shape.Shape> Draw(Renderer renderer)
        {
            Pen pen = new Pen(new SolidBrush(Color.Blue));

            // Ищем первую точку (индекс в листе точек)
            int firstPoint = FindFirstPoint(renderer.shapes);
            int currentPoint = firstPoint;
            int newPoint = 0, previousPoint;

            List<Shape.Shape> used = new List<Shape.Shape>();
           

            // Для того, чтобы сгенерировать первый вектор, юзаем точку с кордами как у первой, но х будет далеко (-100)
            int vectorX = -100 - renderer.shapes[firstPoint].X;
            int vectorY = 0; // Точки имеют один у

            double cos = double.MaxValue;
            for (int i = 0; i < renderer.shapes.Count; i++)
            {
                if (i == firstPoint) continue;
                int potentialVectorX = renderer.shapes[i].X - renderer.shapes[firstPoint].X;
                int potentialVectorY = renderer.shapes[i].Y - renderer.shapes[firstPoint].Y;

                double potentialCos =
                    (vectorX * potentialVectorX + vectorY * potentialVectorY)
                    /
                    (
                        Math.Sqrt(vectorX * vectorX + vectorY * vectorY)
                        *
                        Math.Sqrt(potentialVectorX * potentialVectorX + potentialVectorY * potentialVectorY)
                    );

                if (potentialCos < cos)
                {
                    cos = potentialCos;
                    newPoint = i;
                }
            }

            renderer.graphics.DrawLine(
                        pen,
                        renderer.shapes[currentPoint].X + Shape.Shape._radius / 2,
                        renderer.shapes[currentPoint].Y + Shape.Shape._radius / 2,
                        renderer.shapes[newPoint].X + Shape.Shape._radius / 2,
                        renderer.shapes[newPoint].Y + Shape.Shape._radius / 2
                    );

            
            currentPoint = newPoint;
            previousPoint = firstPoint;

            used.Add(renderer.shapes[currentPoint]);
            used.Add(renderer.shapes[previousPoint]);


            // ostalnije tochki

            if (renderer.shapes.Count < 3) return used;

            do
            {
                cos = double.MaxValue;
                int potential = 0;
                for (int i = 0; i < renderer.shapes.Count; i++)
                {
                    Shape.Shape previous = renderer.shapes[previousPoint];
                    Shape.Shape current = renderer.shapes[currentPoint];
                    
                    
                    double potentialCos = CalculateCos(previous, current, renderer.shapes[i]);

                    if (potentialCos < cos)
                    {
                        potential = i;
                        cos = potentialCos;
                    }
                }
                renderer.graphics.DrawLine(
                    pen,
                    renderer.shapes[currentPoint].X + Shape.Shape._radius / 2,
                    renderer.shapes[currentPoint].Y + Shape.Shape._radius / 2,
                    renderer.shapes[potential].X + Shape.Shape._radius / 2,
                    renderer.shapes[potential].Y  + Shape.Shape._radius / 2
                );

                previousPoint = currentPoint;
                currentPoint = potential;
                used.Add(renderer.shapes[currentPoint]);

            } while (currentPoint != firstPoint);

            return used;
        }

        // находим самую нижнюю левую точку (возвращаем индекс в листе)
        private static int FindFirstPoint(List<Shape.Shape> shapes)
        {
            int maxY = 0;
            int index = 0;

            for (int i = 0; i < shapes.Count; i++)
            {
                if (shapes[i].Y > maxY)
                {
                    maxY = shapes[i].Y;
                    index = i;
                }
            }
            return index;
        }
    }
}
