using System.Collections.Generic;
using System.Drawing;

namespace Polygons.ConvexHull
{
    sealed class Definition : IConvexHull
    {
        private Renderer _renderer;
        private readonly Pen _pen;

        public Definition(Renderer renderer)
        {
            this._renderer = renderer;
            this._pen = new Pen(new SolidBrush(Color.Blue));
        }

        public List<Shape.Shape> Draw()
        {
            List<Shape.Shape> points = new List<Shape.Shape>();
            for (int i = 0; i < _renderer._shapes.Count; i++)
            {
                for (int j = i + 1; j < _renderer._shapes.Count; j++) // первые два цикла для генерации прямой
                {
                    bool up = true;
                    bool down = true;

                    for (int k = 0; k < _renderer._shapes.Count; k++) // далее чекаем все точки относительно прямой между двумя выбранными точками (сверху)
                    {
                        if (!(k != j && k != i && i != j)) continue; // шоб не чекать одну и ту же вершину
                        if (_renderer._shapes[j].X != _renderer._shapes[i].X) // обработка адекватного случая, если две точки не лежат по одному иксу
                        {
                            // по уравнению прямой чекаем выбранная точка ниже или выше
                            if ((_renderer._shapes[k].X - _renderer._shapes[i].X) * (_renderer._shapes[j].Y - _renderer._shapes[i].Y) / (_renderer._shapes[j].X - _renderer._shapes[i].X) + _renderer._shapes[i].Y <= _renderer._shapes[k].Y)
                                up = false;
                            else
                                down = false;
                            continue;
                        }
                        // если лежат на одном иксе, то сравниваем относительно точки из третьей итерации и делаем вердикт
                        // лежит ли она ниже или нет (по дефолту up/down являются true, так тупо удобнее)
                        if (_renderer._shapes[k].X > _renderer._shapes[i].X) down = false;
                        else up = false;
                    }

                    // если точка выше или ниже (что собственно и надо для создания вершины и линий от нее), то рисуем
                    if (up || down)
                    {
                        points.Add(_renderer._shapes[j]);
                        _renderer._graphics.DrawLine(_pen, _renderer._shapes[i].X + Shape.Shape._radius / 2, _renderer._shapes[i].Y + Shape.Shape._radius / 2, _renderer._shapes[j].X + Shape.Shape._radius / 2, _renderer._shapes[j].Y + Shape.Shape._radius / 2);
                    }
                }
            }
            return points;
        }
    }
}
