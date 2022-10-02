using System.Drawing;

namespace Polygons.Shape
{
    public class Square : Shape
    {
        public Square(int x, int y) : base(x, y) { }

        public override void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Red), X, Y, 10, 10);
        }

        public override bool IsInside(int x, int y)
        {
            return x >= X && x <= X + _radius && y >= Y && y <= Y + _radius;
        }
    }
}
