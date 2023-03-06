using System.Collections.Generic;
using System.IO;

namespace Polygons.IO
{
    public class FileReader
    {
        public static List<Shape.Shape> ReadFile(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                List<Shape.Shape> shapes = ShapeDeserializer.Deserialize(stream);
                return shapes;
            }
        }
    }
}
