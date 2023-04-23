using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Polygons.Form1;

namespace Polygons
{
    public partial class ChangeRadiusForm : Form
    {
        public ChangeRadiusForm()
        {
            InitializeComponent();
            trackBar1.Value = Shape.Shape._radius;
        }

        public event RadiusChangedDelegate RadiusChanged;

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (RadiusChanged == null)
            {
                return;
            }
            RadiusChanged(this, new Polygons.Events.RadiusChangedArgs(trackBar1.Value));
        }
    }
}
