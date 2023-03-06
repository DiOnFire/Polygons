using System.Collections.Generic;
using System.Windows.Forms;
using Polygons.Benchmark;
using Polygons.ConvexHull;

namespace Polygons
{
    public partial class BenchmarkForm : Form
    {
        private readonly BenchmarkStorage _storage;

        private BenchmarkStorage Storage
        {
            get { return _storage; }
        }

        public BenchmarkForm(BenchmarkStorage storage)
        {
            InitializeComponent();
            this._storage = storage;
            RenderChart();
        }

        private void RenderChart()
        {
            foreach (KeyValuePair<Algorithm, Dictionary<int, long>> entry in Storage.Storage)
            {
                foreach (KeyValuePair<int, long> test in entry.Value)
                {
                    benchmarkResultsChart.Series[entry.Key.ToString()].Points.AddXY(test.Key, test.Value);
                }
               
            }
        }
    }
}
