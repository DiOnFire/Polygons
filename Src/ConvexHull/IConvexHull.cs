using System.Collections.Generic;
using System.Drawing;

namespace Polygons.ConvexHull
{
    public interface IConvexHull
    {
        List<Shape.Shape> Draw(Graphics g, bool shouldRender = true);
    }
}
