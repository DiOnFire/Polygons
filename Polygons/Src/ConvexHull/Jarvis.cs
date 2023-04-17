using Polygons.Shape;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Polygons.ConvexHull
{
    public sealed class Jarvis : IConvexHull
    {
        private readonly Pen _pen;
        private Renderer _renderer;
        private ShapeManager _manager;

        private ShapeManager ShapeManager
        {
            get { return _manager; }
        }

        private Renderer Renderer
        {
            get { return _renderer; }
        }

        private Pen Pen
        {
            get { return _pen; }
        }

        public Jarvis(Renderer renderer, ShapeManager manager)
        {
            this._pen = new Pen(new SolidBrush(Color.Blue));
            this._renderer = renderer;
            this._manager = manager;
        }

        private double CalculateCos(
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

        public List<Shape.Shape> Draw(Graphics g = null, bool shouldRender = true)
        {
            // Ищем первую точку (индекс в листе точек)
            int firstPoint = FindFirstPoint(ShapeManager.Shapes);
            int currentPoint = firstPoint;
            int newPoint = 0, previousPoint;

            List<Shape.Shape> used = new List<Shape.Shape>();


            // Для того, чтобы сгенерировать первый вектор, юзаем точку с кордами как у первой, но х будет далеко (-100)
            int vectorX = -100 - ShapeManager.Shapes[firstPoint].X;
            int vectorY = 0; // Точки имеют один у

            double cos = double.MaxValue;
            for (int i = 0; i < ShapeManager.Shapes.Count; i++)
            {
                if (i == firstPoint) continue;
                int potentialVectorX = ShapeManager.Shapes[i].X - ShapeManager.Shapes[firstPoint].X;
                int potentialVectorY = ShapeManager.Shapes[i].Y - ShapeManager.Shapes[firstPoint].Y;

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

            if (shouldRender)
            {
                g.DrawLine(
                Pen,
                ShapeManager.Shapes[currentPoint].X + (ShapeManager.Shapes[currentPoint].Type == VertexType.TRIANGLE ? 0 : Shape.Shape._radius / 2),
                ShapeManager.Shapes[currentPoint].Y + (ShapeManager.Shapes[currentPoint].Type == VertexType.TRIANGLE ? 3 : Shape.Shape._radius / 2),
                ShapeManager.Shapes[newPoint].X + (ShapeManager.Shapes[newPoint].Type == VertexType.TRIANGLE ? 0 : Shape.Shape._radius / 2),
                ShapeManager.Shapes[newPoint].Y + (ShapeManager.Shapes[newPoint].Type == VertexType.TRIANGLE ? 3 : Shape.Shape._radius / 2)
            );
            }



            currentPoint = newPoint;
            previousPoint = firstPoint;

            used.Add(ShapeManager.Shapes[currentPoint]);
            used.Add(ShapeManager.Shapes[previousPoint]);


            // ostalnije tochki

            if (ShapeManager.Shapes.Count < 3)
            {
                return used;
            }

            do
            {
                cos = double.MaxValue;
                int potential = 0;
                for (int i = 0; i < ShapeManager.Shapes.Count; i++)
                {
                    Shape.Shape previous = ShapeManager.Shapes[previousPoint];
                    Shape.Shape current = ShapeManager.Shapes[currentPoint];


                    double potentialCos = CalculateCos(previous, current, ShapeManager.Shapes[i]);

                    if (potentialCos < cos)
                    {
                        potential = i;
                        cos = potentialCos;
                    }
                }

                if (shouldRender)
                {
                    g.DrawLine(
                    Pen,
                    ShapeManager.Shapes[currentPoint].X + (ShapeManager.Shapes[currentPoint].Type == VertexType.TRIANGLE ? 0 : Shape.Shape._radius / 2),
                ShapeManager.Shapes[currentPoint].Y + (ShapeManager.Shapes[currentPoint].Type == VertexType.TRIANGLE ? 3 : Shape.Shape._radius / 2),
                    ShapeManager.Shapes[potential].X + (ShapeManager.Shapes[potential].Type == VertexType.TRIANGLE ? 0 : Shape.Shape._radius / 2),
                ShapeManager.Shapes[potential].Y + (ShapeManager.Shapes[potential].Type == VertexType.TRIANGLE ? 3 : Shape.Shape._radius / 2)
                );

                }


                previousPoint = currentPoint;
                currentPoint = potential;
                used.Add(ShapeManager.Shapes[currentPoint]);

            } while (currentPoint != firstPoint);

            return used;
        }

        // находим самую нижнюю левую точку (возвращаем индекс в листе)
        private int FindFirstPoint(List<Shape.Shape> shapes)
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
