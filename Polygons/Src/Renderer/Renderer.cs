using Polygons.ConvexHull;
using System.Collections.Generic;
using System.Drawing;

namespace Polygons
{
    public sealed class Renderer
    {
        public readonly List<Shape.Shape> _shapes;
        public List<Shape.Shape> _used = new List<Shape.Shape>();
        private readonly IConvexHull _jarvis;
        private readonly IConvexHull _definition;
        private DrawAlgorithm algorithm;
        public Graphics _graphics;
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
            set { _graphics = value; }
        }

        public Renderer(Graphics g)
        {
            _graphics = g;
            _shapes = new List<Shape.Shape>();
            _jarvis = new Jarvis(this);
            _definition = new Definition(this);
        }

        public void ReRender()
        {
            
            foreach (Shape.Shape shape in _shapes)
            {
                shape.Draw(_graphics);
            }
            if (_shapes.Count < 2) return;
            _used = _jarvis.Draw();
            
        }

        public void RenderShape(Shape.Shape shape)
        {
            ReRender();
            shape.Draw(_graphics);
        }

        public void AddShape(Shape.Shape shape)
        {
            _shapes.Add(shape);
            
        }

        public void RemoveShape(int x, int y)
        {
            if (isDragging) return;
            foreach (Shape.Shape shape in _shapes)
            {
                if (shape.IsInside(x, y)) {
                    _shapes.Remove(shape);
                    return;
                }
            }
        }

        public void StopDragging()
        {
            foreach (Shape.Shape shape in _shapes)
            {
                shape.IsDragging = false;
            }
            
            ReRender();
            isDragging = false;
        }

        public void Drag(int x, int y)
        {
            foreach (Shape.Shape shape in _shapes)
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
            foreach (Shape.Shape shape in _shapes)
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

        public void ClearGarbage(List<Shape.Shape> used)
        {
            if (_shapes.Count < 3) return;
            List<Shape.Shape> garbage = new List<Shape.Shape>();
            for (int i = 0; i < _shapes.Count; i++)
            {
                if (!used.Contains(_shapes[i]))
                {
                    garbage.Add(_shapes[i]);
                }
            }

            foreach (Shape.Shape item in garbage)
            {
                _shapes.Remove(item);
            }
        }
    }
}
