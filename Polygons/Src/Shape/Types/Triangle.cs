using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygons.Shape
{
    public class Triangle : Shape
    {
        public Triangle(int x, int y) : base(x, y) { }

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
            return y - Y + _radius / 2 >= 2 * X - 2 * x && y - Y + _radius / 2 >= 2 * x - 2 * X && y <= Y + _radius / 2;
        }
    }
}
