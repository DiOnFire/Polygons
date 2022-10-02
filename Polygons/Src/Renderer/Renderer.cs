using System.Collections.Generic;
using System.Drawing;

namespace Polygons.Renderer
{
    public class Renderer
    {
        private List<Shape.Shape> shapes;
        private Graphics graphics;
        private int prevX, prevY;
        public bool isDragging;

        public Renderer(Graphics g)
        {
            graphics = g;
            shapes = new List<Shape.Shape>();
        }

        public void ReRender()
        {
            foreach (Shape.Shape shape in shapes)
            {
                shape.Draw(graphics);
            }
        }

        public void RenderShape(Shape.Shape shape)
        {
            ReRender();
            shape.Draw(graphics);
        }

        public void AddShape(Shape.Shape shape)
        {
            shapes.Add(shape);
            ReRender();
        }

        public void StopDragging()
        {
            foreach (Shape.Shape shape in shapes)
            {
                shape.IsDragging = false;
            }
            isDragging = false;
            ReRender();
        }

        public void Drag(int x, int y)
        {
            if (!isDragging) return;
            foreach (Shape.Shape shape in shapes)
            {
                if (shape.IsDragging)
                {
                    if (!isDragging)
                    {
                        shape.X = x;
                        shape.Y = y;
                    } 
                    else
                    {
                        shape.X += x - prevX;
                        shape.Y += y - prevY;
                    }
                    ReRender();
                }
            }
            prevX = x;
            prevY = y;
            
        }

        public void TryDrag(int x, int y)
        {
            foreach (Shape.Shape shape in shapes)
            {
                if (shape.IsInside(x, y))
                {
                    shape.IsDragging = true;
                
                    ReRender();
                    break;
                }
            }
        }
    }
}
