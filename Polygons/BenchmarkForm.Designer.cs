namespace Polygons
{
    partial class BenchmarkForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.benchmarkResultsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.benchmarkResultsChart)).BeginInit();
            this.SuspendLayout();
            // 
            // benchmarkResultsChart
            // 
            chartArea1.Name = "ChartArea1";
            this.benchmarkResultsChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.benchmarkResultsChart.Legends.Add(legend1);
            this.benchmarkResultsChart.Location = new System.Drawing.Point(-1, -3);
            this.benchmarkResultsChart.Name = "benchmarkResultsChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "JARVIS";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "DEFINITION";
            this.benchmarkResultsChart.Series.Add(series1);
            this.benchmarkResultsChart.Series.Add(series2);
            this.benchmarkResultsChart.Size = new System.Drawing.Size(653, 531);
            this.benchmarkResultsChart.TabIndex = 0;
            this.benchmarkResultsChart.Text = "chart1";
            // 
            // BenchmarkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 527);
            this.Controls.Add(this.benchmarkResultsChart);
            this.Name = "BenchmarkForm";
            this.Text = "BenchmarkForm";
            ((System.ComponentModel.ISupportInitialize)(this.benchmarkResultsChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart benchmarkResultsChart;
    }
}