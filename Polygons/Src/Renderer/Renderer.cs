using Polygons.ConvexHull;
using Polygons.Shape;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Polygons
{
    public sealed class Renderer
    {
        #region fields
        public List<Shape.Shape> _used = new List<Shape.Shape>();
        private IConvexHull _jarvis;
        private IConvexHull _definition;
        private Algorithm algorithm;
        public Graphics _graphics;
        private bool isDragging;
        private VertexType selected;
        private ShapeManager _manager;
        #endregion

        public VertexType SelectedVertex
        {
            get { return selected; }
            set { selected = value; }
        }

        public bool IsDragging { get { return isDragging; } }

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

        public Renderer()
        {
            _manager = new ShapeManager();
            _jarvis = new Jarvis(this, _manager);
            _definition = new Definition(this, _manager);
        }

        public void ReSetup(List<Shape.Shape> shapes)
        {
            _manager = new ShapeManager(shapes);
            _jarvis = new Jarvis(this, _manager);
            _definition = new Definition(this, _manager);
        }

        public void ReRender(Graphics g)
        {   
            foreach (Shape.Shape shape in ShapeManager.Shapes)
            {
                shape.Draw(g);
            }
            if (ShapeManager.Shapes.Count <= 2) return;
            _used = JarvisRenderer.Draw(g);
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
           
            isDragging = false;
        }

        public void Drag(int x, int y)
        {
            foreach (Shape.Shape shape in ShapeManager.Shapes)
            {
                if (shape.IsDragging)
                {
                    if (shape.Type != VertexType.TRIANGLE)
                    {
                        shape.X = x - Shape.Shape._radius / 2;
                        shape.Y = y - Shape.Shape._radius / 2;
                    } else
                    {
                        shape.X = x;
                        shape.Y = y;
                    }
                   
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
                    break;
                }
            }
        }

        public bool IsInside(Shape.Shape shape)
        {
            List<Shape.Shape> list = GetGarbage(_used);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == shape)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Shape.Shape> GetGarbage(List<Shape.Shape> used)
        {
           
            List<Shape.Shape> garbage = new List<Shape.Shape>();
            if (ShapeManager.Shapes.Count < 3) return garbage;
            for (int i = 0; i < ShapeManager.Shapes.Count; i++)
            {
                if (!used.Contains(ShapeManager.Shapes[i]))
                {
                    garbage.Add(ShapeManager.Shapes[i]);
                }
            }
            return garbage;
        }

        public void ClearGarbage(List<Shape.Shape> used)
        {
            

            foreach (Shape.Shape item in GetGarbage(used))
            {
                ShapeManager.RemoveShape(item);
            }
        }
    }
}
