using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygons.Shape
{
    public class Circle : Shape
    {
        public Circle(int x, int y) : base(x - _radius / 16, y - _radius / 16)
        {
            
        }

        public override void Draw(Graphics g)
        {
                g.FillEllipse(new SolidBrush(Color.Red), X, Y, _radius, _radius);
        }

        public override bool IsInside(int x, int y)
        {
            return (x - X) * (x - X) + (y - Y) * (y - Y) <= _radius * _radius;
        }
    }
}
