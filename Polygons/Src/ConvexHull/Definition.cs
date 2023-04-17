using Polygons.Shape;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Polygons.ConvexHull
{
    public sealed class Definition : IConvexHull
    {
        private readonly Renderer _renderer;
        private readonly Pen _pen;
        private readonly ShapeManager _manager;

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

        public Definition(Renderer renderer, ShapeManager manager)
        {
            this._renderer = renderer;
            this._pen = new Pen(new SolidBrush(Color.Blue));
            this._manager = manager;
        }

        public List<Shape.Shape> Draw(Graphics g = null, bool shouldRender = true)
        {
            List<Shape.Shape> points = new List<Shape.Shape>();
            for (int i = 0; i < ShapeManager.Shapes.Count; i++)
            {
                for (int j = i + 1; j < ShapeManager.Shapes.Count; j++) // первые два цикла для генерации прямой
                {


                    bool up = true;
                    bool down = true;

                    for (int k = 0; k < ShapeManager.Shapes.Count; k++) // далее чекаем все точки относительно прямой между двумя выбранными точками (сверху)
                    {
                        if (!(k != j && k != i && i != j)) continue; // шоб не чекать одну и ту же вершину
                        if (ShapeManager.Shapes[j].X != ShapeManager.Shapes[i].X) // обработка адекватного случая, если две точки не лежат по одному иксу
                        {
                            // по уравнению прямой чекаем выбранная точка ниже или выше
                            if ((ShapeManager.Shapes[k].X - ShapeManager.Shapes[i].X) * (ShapeManager.Shapes[j].Y - ShapeManager.Shapes[i].Y) / (ShapeManager.Shapes[j].X - ShapeManager.Shapes[i].X) + ShapeManager.Shapes[i].Y <= ShapeManager.Shapes[k].Y)
                                up = false;
                            else
                                down = false;
                            continue;
                        }
                        // если лежат на одном иксе, то сравниваем относительно точки из третьей итерации и делаем вердикт
                        // лежит ли она ниже или нет (по дефолту up/down являются true, так тупо удобнее)
                        if (ShapeManager.Shapes[k].X > ShapeManager.Shapes[i].X) down = false;
                        else up = false;
                    }

                    // если точка выше или ниже (что собственно и надо для создания вершины и линий от нее), то рисуем
                    if (up || down)
                    {
                        if (!points.Contains(ShapeManager.Shapes[j]))
                        {
                            points.Add(ShapeManager.Shapes[j]);
                        }

                        
                        
                        if (shouldRender)
                        {
                            g.DrawLine(Pen, ShapeManager.Shapes[i].X + Shape.Shape._radius / 2, ShapeManager.Shapes[i].Y + Shape.Shape._radius / 2, ShapeManager.Shapes[j].X + Shape.Shape._radius / 2, ShapeManager.Shapes[j].Y + Shape.Shape._radius / 2);
                        }
                    }
                }
            }
            return points;
        }
    }
}
