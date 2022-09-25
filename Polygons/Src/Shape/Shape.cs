using System.Drawing;

namespace Polygons.Shape
{
    public abstract class Shape
    {
        private int _x;
        private int _y;
        protected static int _radius = 10;

        protected Shape()
        {
            this._x = 0;
            this._y = 0;
        }

        protected Shape(int x, int y)
        {
            this._x = x;
            this._y = y;
        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public abstract void Draw(Graphics g);

        public abstract bool IsInside(int x, int y);
    }
}
