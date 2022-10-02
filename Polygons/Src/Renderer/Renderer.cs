using System.Collections.Generic;
using System.Drawing;

namespace Polygons.Renderer
{
    public class Renderer
    {
        private List<Shape.Shape> shapes;
        private Graphics graphics;
        private bool isDragging;
        private Vertex selected;

        public enum Vertex
        {
            CIRCLE,
            TRIANGLE,
            SQUARE
        }

        public Vertex SelectedVertex
        {
            get { return selected; }
            set { selected = value; }
        }

        public bool IsDragging { get { return isDragging; } }

        public Graphics Graphics
        {
            set { graphics = value; }
        }

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
            
            ReRender();
            isDragging = false;
        }

        public void Drag(int x, int y)
        {
            foreach (Shape.Shape shape in shapes)
            {
                if (shape.IsDragging)
                {
                    shape.X = x;
                    shape.Y = y;
                    ReRender();
                    break;
                }
            }

        }

        public void TryDrag(int x, int y)
        {
            foreach (Shape.Shape shape in shapes)
            {
                if (shape.IsInside(x, y))
                {
                    shape.IsDragging = true;
                    isDragging = true;
                    ReRender();
                    break;
                }
            }
        }
    }
}
