namespace Polygons.Shape
{
    public class SerializableShape
    {
        public int x;
        public int y;
        public int radius;
        public VertexType type;

        public int X
        {
            get { return x; }
        }
        public int Y
        {
            get { return y; }
        }
        public int Radius
        {
            get { return radius; }
        }
        public VertexType Type
        {
            get { return type; }
        }

        public SerializableShape(int x, int y, int radius, VertexType type)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.type = type;
        }
    }
}
