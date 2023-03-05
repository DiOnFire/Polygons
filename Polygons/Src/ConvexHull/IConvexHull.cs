using System.Collections.Generic;

namespace Polygons.ConvexHull
{
    public interface IConvexHull
    {
        List<Shape.Shape> Draw(bool shouldRender = true);
    }
}
