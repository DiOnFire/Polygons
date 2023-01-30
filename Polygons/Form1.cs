using Polygons.Shape;
using System;
using System.Windows.Forms;

namespace Polygons
{
    public partial class Form1 : Form
    {
        private Renderer buffer;

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            buffer = new Renderer(CreateGraphics());
        }

        private void ReRender()
        {
            Refresh();
            buffer.ReRender();
        }

        private void AddShape(int x, int y)
        {
            switch (buffer.SelectedVertex)
            {
                case Renderer.Vertex.CIRCLE:
                    buffer.AddShape(new Circle(x - Shape.Shape._radius / 2, y - Shape.Shape._radius / 2));
                    break;
                case Renderer.Vertex.SQUARE:
                    buffer.AddShape(new Square(x - Shape.Shape._radius / 2, y - Shape.Shape._radius / 2));
                    break;
                case Renderer.Vertex.TRIANGLE:
                    buffer.AddShape(new Triangle(x, y));
                    break;
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                buffer.TryDrag(e.X, e.Y);
                if (!buffer.IsDragging)
                {
                    AddShape(e.X, e.Y);
                }
            }
            else
            {
                buffer.RemoveShape(e.X, e.Y);
            }
            ReRender();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!buffer.IsDragging) return;
            buffer.Drag(e.X, e.Y);
            ReRender();
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            buffer.StopDragging();
            buffer.ClearGarbage(buffer.used);
            ReRender();
           
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            buffer.ReRender();
        }

        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buffer.SelectedVertex = Renderer.Vertex.CIRCLE;
        }

        private void squareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buffer.SelectedVertex = Renderer.Vertex.SQUARE;
        }

        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buffer.SelectedVertex = Renderer.Vertex.TRIANGLE;
        }
    }
}
