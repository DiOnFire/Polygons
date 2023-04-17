using System.Drawing;

namespace Polygons.Shape
{
    public class Triangle : Shape
    {
        public Triangle(int x, int y) : base(x, y, VertexType.TRIANGLE) { }

        public override void Draw(Graphics g)
        {
            SolidBrush br = new SolidBrush(Color.Red);
            Point point1 = new Point(X - _radius / 2, Y + _radius / 2);
            Point point2 = new Point(X, Y - _radius / 2);
            Point point3 = new Point(X + _radius / 2, Y + _radius / 2);
            Point[] curvePoints = { point1, point2, point3 };
            g.FillPolygon(br, curvePoints);
        }

        public override bool IsInside(int x, int y)
        {
            Point point1 = new Point(X - _radius / 2, Y + _radius / 2);
            Point point2 = new Point(X, Y - _radius / 2);
            Point point3 = new Point(X + _radius / 2, Y + _radius / 2);
            return (point1.X - point3.X) * (y - point3.Y) > (point1.Y - point3.Y) * (x - point3.X) &&
                (point2.X - point3.X) * (y - point3.Y) < (point2.Y - point3.Y) * (x - point3.X) &&
                y < point1.Y;
        }
    }
}
