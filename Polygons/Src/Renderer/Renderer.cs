using System.Collections.Generic;
using System.Drawing;

namespace Polygons
{
    public class Renderer
    {
        public List<Shape.Shape> shapes;
        public Graphics graphics;
        private bool isDragging;
        private Vertex selected;

        public enum Vertex
        {
            CIRCLE,
            TRIANGLE,
            SQUARE
        }

        public enum DrawAlgorithm
        {
            JARVIS,
            DEFENITION
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
            if (shapes.Count < 2) return;
            ConvexHullDefinition.Draw(this);
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
                    shape.X = x - Shape.Shape._radius / 2;
                    shape.Y = y - Shape.Shape._radius / 2;
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
