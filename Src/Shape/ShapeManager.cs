using System.Collections.Generic;

namespace Polygons.Shape
{
    public class ShapeManager
    {
        private readonly List<Shape> _shapes;

        public List<Shape> Shapes { get { return _shapes; } }

        public ShapeManager()
        {
            _shapes = new List<Shape>();
        }

        public ShapeManager(List<Shape> shapes)
        {
            _shapes = shapes;
        }

        public void AddShape(Shape shape)
        {
            _shapes.Add(shape);
        }

        public void RemoveShape(Shape shape)
        {
            _shapes.Remove(shape);
        }

        public void Clear()
        {
            _shapes.Clear();
        }

        public void SaveShapesState()
        {
            foreach (Shape shape in Shapes)
            {
                shape.SaveState();
            }
        }

        public void RestoreState()
        {
            foreach (Shape shape in Shapes)
            {
                shape.Restore();
            }
        }
    }
}
