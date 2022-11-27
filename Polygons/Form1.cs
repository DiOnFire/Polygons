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

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (buffer.SelectedVertex)
                {
                    case Renderer.Vertex.CIRCLE:
                        buffer.AddShape(new Circle(e.X - Shape.Shape._radius / 2, e.Y - Shape.Shape._radius / 2));
                        break;
                    case Renderer.Vertex.SQUARE:
                        buffer.AddShape(new Square(e.X - Shape.Shape._radius / 2, e.Y - Shape.Shape._radius / 2));
                        break;
                    case Renderer.Vertex.TRIANGLE:
                        buffer.AddShape(new Triangle(e.X, e.Y));
                        break;
                }
            } else if (e.Button == MouseButtons.Right)
            {
                buffer.RemoveShape(e.X, e.Y);
            }
            
            Refresh();
            buffer.ReRender();
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                buffer.TryDrag(e.X, e.Y);
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!buffer.IsDragging) return;
            buffer.Drag(e.X, e.Y);
            Refresh();
            buffer.ReRender();
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            buffer.StopDragging();
            buffer.ReRender();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            buffer.Graphics = CreateGraphics();
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
