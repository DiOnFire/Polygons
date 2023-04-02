using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Polygons.IO
{
    public class ShapeSerializer
    {
        public static string Serialize(List<Shape.Shape> shapes)
        {
            return JsonSerializer.Serialize(shapes);
        }
    }
}
