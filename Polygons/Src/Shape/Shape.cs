using System;
using System.Drawing;

namespace Polygons.Shape
{
    public abstract class Shape
    {
        private int _x;
        private int _y;
        private int _oldX;
        private int _oldY;
        private bool _dragging;
        public static int _radius = 50;

        protected Shape()
        {
            this._x = 0;
            this._y = 0;
            this._dragging = false;
        }

        protected Shape(int x, int y)
        {
            this._x = x;
            this._y = y;
            this._dragging = false;
        }

        public int OldX
        {
            get { return _oldX; }
        }

        public int OldY
        {
            get { return _oldY; }
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

        public bool IsDragging
        { 
            get { return _dragging; }
            set { _dragging = value; }
        }

        public abstract void Draw(Graphics g);

        public abstract bool IsInside(int x, int y);

        public void MoveRandom(Random random)
        {
            _x += random.Next(-3, 3);
            _y += random.Next(-3, 3);
        }

        public void SaveState()
        {
            _oldX = _x;
            _oldY = _y;
        }

        public void Restore()
        {
            _x = _oldX;
            _y = _oldY;
        }
    }
}
