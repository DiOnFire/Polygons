using System.Drawing;

namespace Polygons
{
    sealed class ConvexHullDefinition
    {
        public static void Draw(Renderer renderer)
        {
            Pen pen = new Pen(new SolidBrush(Color.Red));
            for (int i = 0; i < renderer.shapes.Count; i++)
            {
                for (int j = i + 1; j < renderer.shapes.Count; j++) // первые два цикла для генерации прямой
                {
                    bool up = true;
                    bool down = true;

                    for (int k = 0; k < renderer.shapes.Count; k++) // далее чекаем все точки относительно прямой между двумя выбранными точками (сверху)
                    {
                        if (!(k != j && k != i && i != j)) continue; // шоб не чекать одну и ту же вершину
                        if (renderer.shapes[j].X != renderer.shapes[i].X) // обработка адекватного случая, если две точки не лежат по одному иксу
                        {
                            // по уравнению прямой чекаем выбранная точка ниже или выше
                            if ((renderer.shapes[k].X - renderer.shapes[i].X) * (renderer.shapes[j].Y - renderer.shapes[i].Y) / (renderer.shapes[j].X - renderer.shapes[i].X) + renderer.shapes[i].Y <= renderer.shapes[k].Y)
                                up = false;
                            else
                                down = false;
                            continue;
                        }
                        // если лежат на одном иксе, то сравниваем относительно точки из третьей итерации и делаем вердикт
                        // лежит ли она ниже или нет (по дефолту up/down являются true, так тупо удобнее)
                        if (renderer.shapes[k].X > renderer.shapes[i].X) down = false;
                        else up = false;
                    }

                    // если точка выше или ниже (что собственно и надо для создания вершины и линий от нее), то рисуем
                    if (up || down)
                    {
                        renderer.graphics.DrawLine(pen, renderer.shapes[i].X + Shape.Shape._radius / 2, renderer.shapes[i].Y + Shape.Shape._radius / 2, renderer.shapes[j].X + Shape.Shape._radius / 2, renderer.shapes[j].Y + Shape.Shape._radius / 2);
                    }
                }
            }
        }
    }
}
