using System;
using System.Collections.Generic;
using System.Drawing;

namespace Polygons
{
    sealed class ConvexHullJarvis
    {
        public static void Draw(Renderer renderer)
        {
            Pen pen = new Pen(new SolidBrush(Color.Blue));

            // Ищем первую точку (индекс в листе точек)
            int firstPoint = FindFirstPoint(renderer.shapes);
            int currentPoint = firstPoint;
            int newPoint = 0;
           

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

            vectorX = renderer.shapes[newPoint].X - renderer.shapes[firstPoint].X;
            vectorY = renderer.shapes[newPoint].Y - renderer.shapes[firstPoint].Y;

            renderer.graphics.DrawLine(
                        pen,
                        renderer.shapes[currentPoint].X + Shape.Shape._radius / 2,
                        renderer.shapes[currentPoint].Y + Shape.Shape._radius / 2,
                        renderer.shapes[newPoint].X + Shape.Shape._radius / 2,
                        renderer.shapes[newPoint].Y + Shape.Shape._radius / 2
                    );

            
            currentPoint = newPoint;

            

            // ostalnije tochki

            if (renderer.shapes.Count < 3) return;

            do
            {
                cos = double.MaxValue;
                for (int i = 0; i < renderer.shapes.Count; i++)
                {  
                    int potentialVectorX = renderer.shapes[i].X - renderer.shapes[currentPoint].X;
                    int potentialVectorY = renderer.shapes[i].Y - renderer.shapes[currentPoint].Y;

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
                        renderer.graphics.DrawRectangle(pen, renderer.shapes[i].X, renderer.shapes[i].Y, 10, 10);
                    }
                }

                // в чем баг: такое ощущение, что этот цикл не видит дальше первых двух точек и рендерит только их, почему - чёрт его знает

                renderer.graphics.DrawLine(
                        pen,
                        renderer.shapes[currentPoint].X + Shape.Shape._radius / 2,
                        renderer.shapes[currentPoint].Y + Shape.Shape._radius / 2,
                        renderer.shapes[newPoint].X + Shape.Shape._radius / 2,
                        renderer.shapes[newPoint].Y + Shape.Shape._radius / 2
                    );

                

                vectorX = renderer.shapes[newPoint].X - renderer.shapes[currentPoint].X;
                vectorY = renderer.shapes[newPoint].Y - renderer.shapes[currentPoint].Y;
               
                currentPoint = newPoint;

               

                if (currentPoint == firstPoint)
                {
                    Console.WriteLine("operation end");
                } else
                {
                    Console.WriteLine("operation not end");
                }

            } while (currentPoint != firstPoint);
            
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
