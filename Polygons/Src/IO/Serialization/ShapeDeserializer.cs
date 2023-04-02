using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Polygons.IO
{
    public class ShapeDeserializer
    {
        public static List<Shape.Shape> Deserialize(string text)
        {
            return JsonSerializer.Deserialize<List<Shape.Shape>>(text);
        }
    }
}
