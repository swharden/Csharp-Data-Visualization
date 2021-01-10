using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MystifyBlazor.Models
{
    /// <summary>
    /// A shape stores a collection of polygons.
    /// Polygons early in the list are historical polygons.
    /// The last polygon in the list is the most recent.
    /// </summary>
    public class Shape
    {
        public List<Polygon> Polygons = new List<Polygon>();

        public Shape(Random rand, int cornerCount, double width, double height, Color color) =>
            Polygons.Add(Polygon.RandomPolygon(rand, cornerCount, width, height, color));

        public void Advance(double delta, double width, double height, int historyCount, bool rainbow)
        {
            Polygon nextPolygon = Polygons.Last().NextPolygon(delta, width, height, rainbow);
            Polygons.Add(nextPolygon);
            while (Polygons.Count > historyCount)
                Polygons.RemoveAt(0);
        }
    }
}
