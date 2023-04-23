using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygons.Events
{
    public class RadiusChangedArgs : EventArgs
    {
        public int Radius { get; set; }

        public RadiusChangedArgs(int radius)
        {
            this.Radius = radius;
        }
    }
}
