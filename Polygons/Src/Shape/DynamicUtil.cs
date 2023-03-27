using System;

namespace Polygons.Shape
{
    public sealed class DynamicUtil
    {
        private readonly Random _random;
        private readonly ShapeManager _shapeManager;

        private Random Random
        {
            get { return _random; }
        }

        private ShapeManager ShapeManager
        {
            get { return _shapeManager; }
        }

        public DynamicUtil(ShapeManager shapeManager)
        {
            _random = new Random();
            _shapeManager = shapeManager;
        }

        public void MoveRandom()
        {
            foreach (Shape shape in ShapeManager.Shapes)
            {
                shape.MoveRandom(Random);
            }
        }
    }
}
