using Polygons.ConvexHull;
using Polygons.Shape;
using System.Collections.Generic;
using System.Drawing;

namespace Polygons
{
    public sealed class Renderer
    {
        #region fields
        public List<Shape.Shape> _used = new List<Shape.Shape>();
        private readonly IConvexHull _jarvis;
        private readonly IConvexHull _definition;
        private Algorithm algorithm;
        public Graphics _graphics;
        private bool isDragging;
        private VertexType selected;
        private readonly ShapeManager _manager;
        #endregion

        public VertexType SelectedVertex
        {
            get { return selected; }
            set { selected = value; }
        }

        public bool IsDragging { get { return isDragging; } }

        public Graphics Graphics
        {
            set { _graphics = value; }
            get { return _graphics; }
        }

        public ShapeManager ShapeManager
        {
            get { return _manager; }
        }

        public IConvexHull JarvisRenderer
        {
            get { return _jarvis; }
        }

        public IConvexHull DefinitionRenderer
        {
            get { return _definition; }
        }

        public Renderer(Graphics g)
        {
            _manager = new ShapeManager();
            _graphics = g;
            _jarvis = new Jarvis(this, _manager);
            _definition = new Definition(this, _manager);
        }

        public void ReRender()
        {
            
            foreach (Shape.Shape shape in ShapeManager.Shapes)
            {
                shape.Draw(Graphics);
            }
            if (ShapeManager.Shapes.Count < 2) return;
            _used = JarvisRenderer.Draw();
            
        }

        public void RenderShape(Shape.Shape shape)
        {
            ReRender();
            shape.Draw(Graphics);
        }

        public void AddShape(Shape.Shape shape)
        {
            ShapeManager.AddShape(shape);
        }

        public void RemoveShape(int x, int y)
        {
            if (isDragging) return;
            foreach (Shape.Shape shape in ShapeManager.Shapes)
            {
                if (shape.IsInside(x, y)) {
                    ShapeManager.RemoveShape(shape);
                    return;
                }
            }
        }

        public void StopDragging()
        {
            foreach (Shape.Shape shape in ShapeManager.Shapes)
            {
                shape.IsDragging = false;
            }
            
            ReRender();
            isDragging = false;
        }

        public void Drag(int x, int y)
        {
            foreach (Shape.Shape shape in ShapeManager.Shapes)
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
            foreach (Shape.Shape shape in ShapeManager.Shapes)
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
            if (ShapeManager.Shapes.Count < 3) return;
            List<Shape.Shape> garbage = new List<Shape.Shape>();
            for (int i = 0; i < ShapeManager.Shapes.Count; i++)
            {
                if (!used.Contains(ShapeManager.Shapes[i]))
                {
                    garbage.Add(ShapeManager.Shapes[i]);
                }
            }

            foreach (Shape.Shape item in garbage)
            {
                ShapeManager.RemoveShape(item);
            }
        }
    }
}
