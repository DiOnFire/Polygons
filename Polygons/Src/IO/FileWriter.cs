using System.Collections.Generic;
using System.IO;

namespace Polygons.IO
{
    public class FileWriter
    {
        public static void WriteFile(string path, List<Shape.Shape> shapes)
        {
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                ShapeSerializer.Serialize(stream, shapes);
            }
        }
    }
}
