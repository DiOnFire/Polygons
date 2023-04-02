using Polygons.Benchmark;
using Polygons.Shape;
using Polygons.ConvexHull;
using System;
using System.Windows.Forms;
using Polygons.Util;
using System.IO;
using System.Collections.Generic;
using Polygons.IO;

namespace Polygons
{
    public partial class Form1 : Form
    {
        private Renderer buffer;
        private TimerUtil timer;
        private string filePath = "";

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            UpdateStyles();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            buffer = new Renderer(CreateGraphics());
            timer = new TimerUtil(buffer, this);
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
                case VertexType.CIRCLE:
                    buffer.AddShape(new Circle(x - Shape.Shape._radius / 2, y - Shape.Shape._radius / 2));
                    break;
                case VertexType.SQUARE:
                    buffer.AddShape(new Square(x - Shape.Shape._radius / 2, y - Shape.Shape._radius / 2));
                    break;
                case VertexType.TRIANGLE:
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
            buffer.ClearGarbage(buffer._used);
            ReRender();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            buffer.ReRender();
        }

        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buffer.SelectedVertex = VertexType.CIRCLE;
        }

        private void squareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buffer.SelectedVertex = VertexType.SQUARE;
        }

        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buffer.SelectedVertex = VertexType.TRIANGLE;
        }

        private void runTestBenchmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DefinitionBenchmark definitionBenchmark = new DefinitionBenchmark();
            JarvisBenchmark jarvisBenchmark = new JarvisBenchmark();

            Benchmark.Benchmark benchmark = new Benchmark.Benchmark();
            Benchmark.Benchmark jBenchmark = new Benchmark.Benchmark(new int[] { 10000, 20000, 50000, 100000, 200000 });
            BenchmarkStorage storage = new BenchmarkStorage();

            storage.AddBenchmarkResult(Algorithm.DEFINITION, benchmark.RunBenchmark(definitionBenchmark));
            storage.AddBenchmarkResult(Algorithm.JARVIS, jBenchmark.RunBenchmark(jarvisBenchmark));

            BenchmarkForm form = new BenchmarkForm(storage);
            form.Show();
        }

        private void dynamicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dynamicToolStripMenuItem.Checked)
            {
            
                timer.Setup();
            } else
            {
                timer.Stop();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    filePath = file;
                    string text = File.ReadAllText(file);
                    List<Shape.Shape> shapes = ShapeDeserializer.Deserialize(text);

                }
                catch (IOException)
                {
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = saveFileDialog.FileName;
                File.WriteAllText(file, ShapeSerializer.Serialize(buffer.ShapeManager.Shapes));
            }
            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filePath == "")
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog.FileName;
                    return;
                }
                    

            }
            File.WriteAllText(filePath, ShapeSerializer.Serialize(buffer.ShapeManager.Shapes));
        }
    }
}
