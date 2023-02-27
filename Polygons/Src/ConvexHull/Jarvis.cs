using System;
using System.Collections.Generic;
using System.Drawing;

namespace Polygons.ConvexHull
{
    sealed class Jarvis : IConvexHull
    {
        private readonly Pen _pen;
        private Renderer _renderer;

        public Jarvis(Renderer renderer)
        {
            this._pen = new Pen(new SolidBrush(Color.Blue));
            this._renderer = renderer;
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

        public List<Shape.Shape> Draw()
        {
            // Ищем первую точку (индекс в листе точек)
            int firstPoint = FindFirstPoint(_renderer._shapes);
            int currentPoint = firstPoint;
            int newPoint = 0, previousPoint;

            List<Shape.Shape> used = new List<Shape.Shape>();
           

            // Для того, чтобы сгенерировать первый вектор, юзаем точку с кордами как у первой, но х будет далеко (-100)
            int vectorX = -100 - _renderer._shapes[firstPoint].X;
            int vectorY = 0; // Точки имеют один у

            double cos = double.MaxValue;
            for (int i = 0; i < _renderer._shapes.Count; i++)
            {
                if (i == firstPoint) continue;
                int potentialVectorX = _renderer._shapes[i].X - _renderer._shapes[firstPoint].X;
                int potentialVectorY = _renderer._shapes[i].Y - _renderer._shapes[firstPoint].Y;

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

            _renderer._graphics.DrawLine(
                _pen,
                _renderer._shapes[currentPoint].X + Shape.Shape._radius / 2,
                _renderer._shapes[currentPoint].Y + Shape.Shape._radius / 2,
                _renderer._shapes[newPoint].X + Shape.Shape._radius / 2,
                _renderer._shapes[newPoint].Y + Shape.Shape._radius / 2
            );

            
            currentPoint = newPoint;
            previousPoint = firstPoint;

            used.Add(_renderer._shapes[currentPoint]);
            used.Add(_renderer._shapes[previousPoint]);


            // ostalnije tochki

            if (_renderer._shapes.Count < 3)
            {
                return used;
            }

            do
            {
                cos = double.MaxValue;
                int potential = 0;
                for (int i = 0; i < _renderer._shapes.Count; i++)
                {
                    Shape.Shape previous = _renderer._shapes[previousPoint];
                    Shape.Shape current = _renderer._shapes[currentPoint];
                    
                    
                    double potentialCos = CalculateCos(previous, current, _renderer._shapes[i]);

                    if (potentialCos < cos)
                    {
                        potential = i;
                        cos = potentialCos;
                    }
                }
                _renderer._graphics.DrawLine(
                    _pen,
                    _renderer._shapes[currentPoint].X + Shape.Shape._radius / 2,
                    _renderer._shapes[currentPoint].Y + Shape.Shape._radius / 2,
                    _renderer._shapes[potential].X + Shape.Shape._radius / 2,
                    _renderer._shapes[potential].Y  + Shape.Shape._radius / 2
                );

                previousPoint = currentPoint;
                currentPoint = potential;
                used.Add(_renderer._shapes[currentPoint]);

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
