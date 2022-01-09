using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MystifyBlazor.Models
{
    /// <summary>
    /// A Polygon describes a shape at one instance in time.
    /// It has a list of corners and a color.
    /// </summary>
    public class Polygon
    {
        public int CornerCount => Corners.Count;
        private readonly List<Corner> Corners = new List<Corner>();
        public readonly Color Color;

        public Polygon(Color color, List<Corner> corners) => (Color, Corners) = (color, corners);

        public static Polygon RandomPolygon(Random rand, int cornerCount, double width, double height, Color color) =>
            new Polygon(
                color: color ?? new Color(rand.NextDouble()),
                corners: Enumerable.Range(0, cornerCount).Select(x => Corner.RandomPoint(rand, width, height)).ToList()
            );

        public Polygon NextPolygon(double delta, double width, double height, bool rainbow)
        {
            var newCorners = Corners.Select(x => x.NextPoint(delta, width, height)).ToList();
            var newColor = rainbow ? new Color(Color.Hue + .01) : Color;
            return new Polygon(newColor, newCorners);
        }

        public string GetJSON()
        {
            StringBuilder sb = new StringBuilder($"[\"{Color}\"");
            foreach (Corner point in Corners)
                sb.Append($",[{point.X:0.00},{point.Y:0.00}]");
            sb.Append(']');
            return sb.ToString();
        }
    }
}
