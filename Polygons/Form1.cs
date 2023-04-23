using Polygons.Benchmark;
using Polygons.Shape;
using Polygons.ConvexHull;
using System;
using System.Windows.Forms;
using Polygons.Util;
using System.IO;
using System.Collections.Generic;
using Polygons.IO;
using Polygons.Events;

namespace Polygons
{
    public partial class Form1 : Form
    {
        private Renderer buffer;
        private TimerUtil timer;
        private string filePath = "";
        private bool isChanged = false;
        private Shape.Shape lastShape;

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        public delegate void RadiusChangedDelegate(object sender, RadiusChangedArgs e);

        public void OnRadiusChange(object sender, RadiusChangedArgs args)
        {
            Shape.Shape._radius = args.Radius;
            Refresh();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            buffer = new Renderer();
            timer = new TimerUtil(buffer, this);
        }

        private void ReRender()
        {
            isChanged = true;
            Refresh();
        }

        private void AddShape(int x, int y)
        {
            switch (buffer.SelectedVertex)
            {
                case VertexType.CIRCLE:
                    lastShape = new Circle(x - Shape.Shape._radius / 2, y - Shape.Shape._radius / 2);
                    break;
                case VertexType.SQUARE:
                    lastShape = new Square(x - Shape.Shape._radius / 2, y - Shape.Shape._radius / 2);
                    break;
                case VertexType.TRIANGLE:
                    lastShape = new Triangle(x, y);
                    break;
            }
            buffer.AddShape(lastShape);
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
            buffer.ReRender(e.Graphics);
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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((filePath == "" && buffer.ShapeManager.Shapes.Count > 0) || isChanged)
            {
                DialogResult res = MessageBox.Show("Есть не сохранённые изменения. Сохранить?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.OK)
                {
                    Save();
                    isChanged = false;
                }
            }
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    filePath = file;
                    string text = File.ReadAllText(file);
                    List<Shape.SerializableShape> shapes = ShapeDeserializer.Deserialize(text);
                    if (shapes == null) {
                        filePath = "";
                        return; 
                    }
                    List<Shape.Shape> newShapes = new List<Shape.Shape>();
                    foreach (Shape.SerializableShape s in shapes)
                    {
                        switch (s.type)
                        {
                            case VertexType.CIRCLE:
                                {
                                    newShapes.Add(new Circle(s.x, s.y));
                                    break;
                                }
                            case VertexType.SQUARE:
                                {
                                    newShapes.Add(new Square(s.x, s.y));
                                    break;
                                }
                            case VertexType.TRIANGLE:
                                {
                                    newShapes.Add(new Triangle(s.x - Shape.Shape._radius / 2, s.y - Shape.Shape._radius / 2));
                                    break;
                                }
                        }
                       
                    }
                    buffer.ReSetup(newShapes);
                    ReRender();
                }
                catch (IOException)
                {
                    MessageBox.Show("Файл повреждён или содержит ошибки.", "Ошибка открытия", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                isChanged = false;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON (*.json)|*.json";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = saveFileDialog.FileName;
                File.WriteAllText(filePath, ShapeSerializer.Serialize(buffer.ShapeManager.Shapes));
            }
        }

        public void Save()
        {
            if (filePath == "")
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                Stream myStream;
                saveFileDialog.Filter = "JSON (*.json)|*.json";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog.FileName;
                    File.WriteAllText(filePath, ShapeSerializer.Serialize(buffer.ShapeManager.Shapes));
                    isChanged = false;
                }
            } else
            {
                File.WriteAllText(filePath, ShapeSerializer.Serialize(buffer.ShapeManager.Shapes));
                isChanged = false;
            }
          
            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        public void Check()
        {
            if ((filePath == "" && buffer.ShapeManager.Shapes.Count > 0) || isChanged)
            {
                DialogResult res = MessageBox.Show("Есть не сохранённые изменения. Сохранить?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.OK)
                {
                    Save();
                    isChanged = false;
                }
                if (res == DialogResult.Cancel)
                {
                    buffer = new Renderer();
                    filePath = "";
                    ReRender();
                    isChanged = false;
                }
            }
            else
            {
                buffer = new Renderer();
                filePath = "";
                ReRender();
                isChanged = false;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Check();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Check();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {             
            timer.Setup();
            startToolStripMenuItem.Enabled = false;
            continueToolStripMenuItem.Enabled = false;
            resetToolStripMenuItem.Enabled = true;
            pauseToolStripMenuItem.Enabled = true;
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Pause();
            pauseToolStripMenuItem.Enabled = false;
            continueToolStripMenuItem.Enabled = true;
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Stop();
            startToolStripMenuItem.Enabled = true;
            continueToolStripMenuItem.Enabled = false;
            resetToolStripMenuItem.Enabled = false;
            pauseToolStripMenuItem.Enabled = false;
        }

        private void continueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Continue();
            pauseToolStripMenuItem.Enabled = true;
            continueToolStripMenuItem.Enabled = false;
        }

        private void changeRadiusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeRadiusForm form = new ChangeRadiusForm();
            form.RadiusChanged += new RadiusChangedDelegate(OnRadiusChange);
            form.Show();
        }
    }
}
