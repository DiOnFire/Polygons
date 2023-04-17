using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Polygons.IO
{
    public class ShapeSerializer
    {
        public static string Serialize(List<Shape.Shape> shapes)
        {
            List<Shape.SerializableShape> ser = new List<Shape.SerializableShape>();
            foreach (Shape.Shape shape in shapes)
            {
                ser.Add(new Shape.SerializableShape(shape.X, shape.Y, 50, shape.Type));
            }
            return JsonSerializer.Serialize(ser);
        }
    }
}
