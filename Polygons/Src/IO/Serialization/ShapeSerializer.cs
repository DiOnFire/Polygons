using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Polygons.IO
{
    public class ShapeSerializer
    {
        public static void Serialize(FileStream stream, List<Shape.Shape> shapes)
        {
            JsonSerializer.Serialize(stream, shapes);
        }
    }
}
