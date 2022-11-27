using System.Drawing;

namespace Polygons
{
    class ConvexHullDefinition
    {
        public static void Draw(Renderer renderer)
        {
            Pen pen = new Pen(new SolidBrush(Color.Red));
            for (int i = 0; i < renderer.shapes.Count; i++)
            {
                for (int j = i + 1; j < renderer.shapes.Count; j++)
                {
                    bool up = true;
                    bool down = true;

					for (int k = 0; k < renderer.shapes.Count; k++)
					{
						if (k != j && k != i && i != j)
						{
							if ((renderer.shapes[j].X - renderer.shapes[i].X) != 0)
								if ((renderer.shapes[k].X - renderer.shapes[i].X) * (renderer.shapes[j].Y - renderer.shapes[i].Y) / (renderer.shapes[j].X - renderer.shapes[i].X) + renderer.shapes[i].Y <= renderer.shapes[k].Y)
									up = false;
								else
									down = false;
							else
								if (renderer.shapes[k].X > renderer.shapes[i].X)
								down = false;
							else
								up = false;
						}
					}
					if (up || down)
					{
						renderer.graphics.DrawLine(pen, renderer.shapes[i].X + Shape.Shape._radius / 2, renderer.shapes[i].Y + Shape.Shape._radius / 2, renderer.shapes[j].X + Shape.Shape._radius / 2, renderer.shapes[j].Y + Shape.Shape._radius / 2);
					}
					
                }
            }
        }
    }
}
