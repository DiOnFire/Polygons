using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace Polygons.IO
{
    public class ShapeDeserializer
    {
        public static List<Shape.SerializableShape> Deserialize(string text)
        {try
            {
                return JsonSerializer.Deserialize<List<Shape.SerializableShape>>(text);
            } catch (Exception e)
            {
                MessageBox.Show("Файл повреждён или содержит ошибки.", "Ошибка открытия", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }
    }
}
