using System.Collections.Generic;
using System.Drawing;

namespace Polygons.Renderer
{
    public class VertexBuffer
    {
        private List<Shape.Shape> shapes;
        private Graphics graphics;
        private Shape.Shape currentDragging;

        public bool IsDragging
        {
            get { return currentDragging != null; }
        }

        public VertexBuffer(Graphics g)
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
            RenderShape(shape);
        }

        public void StartDragging(Shape.Shape shape)
        {
            currentDragging = shape;
            currentDragging.Draw(graphics);
        }

        public void StopDragging()
        {
            currentDragging = null;
        }

        public void Drag(int x, int y)
        {
                currentDragging.X = x;
                currentDragging.Y = y;
                RenderShape(currentDragging);
                ReRender();
        }

        public void TryDrag(int x, int y)
        {
            foreach (Shape.Shape shape in shapes)
            {
                if (shape.IsInside(x, y))
                {
                    StartDragging(shape);
                    break;
                }
            }
        }
    }
}
