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

            //int index = FindFirstPoint(renderer.shapes);
            //int newPoint = 0;
            //int potentialPoint = 0;
            //double cos = 0;

            //// находим первую линию
            //for (int i = 0; i < renderer.shapes.Count; i++)
            //{
            //    if (i == index) continue;

            //    int x = renderer.shapes[index].X - renderer.shapes[i].X;
            //    int y = renderer.shapes[index].Y - renderer.shapes[i].Y;

            //    double newCos = x * 2 / Math.Sqrt(x * x + y * y) * 2;

            //    if (newCos < cos)
            //    {
            //        cos = newCos;
            //        newPoint = i;
            //    }
            //}

            //renderer.graphics.DrawRectangle(pen, renderer.shapes[index].X, renderer.shapes[index].Y, 10, 10);

            //// рендерим первую линию
            //renderer.graphics.DrawLine(
            //    pen, 
            //    renderer.shapes[index].X + Shape.Shape._radius / 2, 
            //    renderer.shapes[index].Y + Shape.Shape._radius / 2, 
            //    renderer.shapes[newPoint].X + Shape.Shape._radius / 2, 
            //    renderer.shapes[newPoint].Y + Shape.Shape._radius / 2
            //);

            //int currentPoint = index;
            //for (int j = 0; j < renderer.shapes.Count; j++)
            //{
            //    cos = 100;

            //    // ищем минимальный косинус
            //    for (int i = 0; i < renderer.shapes.Count; i++)
            //    {
            //        if (i == currentPoint || i == newPoint) continue;

            //        // ищемс вектор
            //        int x = renderer.shapes[currentPoint].X - renderer.shapes[newPoint].X;
            //        int y = renderer.shapes[currentPoint].Y - renderer.shapes[newPoint].Y;
            //        int x1 = renderer.shapes[newPoint].X - renderer.shapes[i].X;
            //        int y1 = renderer.shapes[newPoint].Y - renderer.shapes[i].Y;

            //        double newCos = x * x1 + y * y1 / Math.Sqrt(x * x + y * y) * Math.Sqrt(x1 * x1 + y1 * y1);

            //        if (newCos < cos)
            //        {
            //            cos = newCos;
            //            potentialPoint = i;
            //        }    
            //    }



            //    renderer.graphics.DrawLine(
            //        pen, 
            //        renderer.shapes[currentPoint].X + Shape.Shape._radius / 2, 
            //        renderer.shapes[currentPoint].Y + Shape.Shape._radius / 2, 
            //        renderer.shapes[potentialPoint].X + Shape.Shape._radius / 2, 
            //        renderer.shapes[potentialPoint].Y + Shape.Shape._radius / 2
            //    );

            //    currentPoint = newPoint;
            //    newPoint = potentialPoint;
            //}

            // Ищем первую точку (индекс в листе точек)
            int currentPoint = FindFirstPoint(renderer.shapes);
            int newPoint = 0;
            renderer.graphics.DrawRectangle(pen, renderer.shapes[currentPoint].X, renderer.shapes[currentPoint].Y, 10, 10);

            // Для того, чтобы сгенерировать первый вектор, юзаем точку с кордами как у первой, но х будет далеко (-100)
            int vectorX = -100 - renderer.shapes[currentPoint].X;
            int vectorY = 0; // Точки имеют один у

            double cos = double.MaxValue;
            for (int i = 0; i < renderer.shapes.Count; i++)
            {
                if (i == currentPoint) continue;
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
                    vectorX = potentialVectorX;
                    vectorY = potentialVectorY;

                    newPoint = i;
                    renderer.graphics.DrawRectangle(pen, renderer.shapes[newPoint].X, renderer.shapes[newPoint].Y, 20, 20);
                }
            }
            renderer.graphics.DrawLine(
                    pen,
                    renderer.shapes[currentPoint].X + Shape.Shape._radius / 2,
                    renderer.shapes[currentPoint].Y + Shape.Shape._radius / 2,
                    renderer.shapes[newPoint].X + Shape.Shape._radius / 2,
                    renderer.shapes[newPoint].Y + Shape.Shape._radius / 2
                );

            // Итерируемся по каждому набору точек по нужному кол-ву раз
            //for (int _ = 0; _ < renderer.shapes.Count; _++)
            //{
            //    double cos = double.MaxValue;
                
            //    for (int i = 0; i < renderer.shapes.Count; i++)
            //    {
            //        if (i == currentPoint) continue;
            //        int potentialVectorX = renderer.shapes[i].X - renderer.shapes[currentPoint].X;
            //        int potentialVectorY = renderer.shapes[i].Y - renderer.shapes[currentPoint].Y;

            //        double potentialCos =
            //            (vectorX * potentialVectorX + vectorY * potentialVectorY)
            //            /
            //            (
            //                Math.Sqrt(vectorX * vectorX + vectorY * vectorY)
            //                * 
            //                Math.Sqrt(potentialVectorX * potentialVectorX + potentialVectorY * potentialVectorY)
            //            );

            //        if (potentialCos < cos)
            //        {
            //            cos = potentialCos;
            //            vectorX = potentialVectorX;
            //            vectorY = potentialVectorY;

            //            newPoint = i;
            //            renderer.graphics.DrawRectangle(pen, renderer.shapes[newPoint].X, renderer.shapes[newPoint].Y, 20, 20);
            //        }
            //    }
            //    renderer.graphics.DrawLine(
            //        pen,
            //        renderer.shapes[currentPoint].X + Shape.Shape._radius / 2,
            //        renderer.shapes[currentPoint].Y + Shape.Shape._radius / 2,
            //        renderer.shapes[newPoint].X + Shape.Shape._radius / 2,
            //        renderer.shapes[newPoint].Y  + Shape.Shape._radius / 2
            //    );
            //    currentPoint = newPoint;
            //} 
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
