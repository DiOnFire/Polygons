using Polygons.Renderer;
using Polygons.Shape;
using System;
using System.Windows.Forms;

namespace Polygons
{
    public partial class Form1 : Form
    {
        private Renderer.Renderer buffer;

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            buffer = new Renderer.Renderer(CreateGraphics());
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (buffer.isDragging)
                    {
                        buffer.StopDragging();
                    } else
                    {
                        buffer.AddShape(new Circle(e.X, e.Y));
                    }
                    
                    break;
                case MouseButtons.Right:
                    buffer.TryDrag(e.X, e.Y);
                    break;
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                buffer.Drag(e.X, e.Y);
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (buffer.isDragging)
            {
                buffer.Drag(e.X, e.Y);
                Refresh();
                buffer.ReRender();
            }
            
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            buffer.StopDragging();
        }

        private void OnResize(object sender, EventArgs e)
        {
            
        }
    }
}
