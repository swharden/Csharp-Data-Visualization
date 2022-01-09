using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MystifyBlazor.Models
{
    /// <summary>
    /// This class manages shapes (which are historical records of polygons)
    /// </summary>
    public class Field
    {
        private readonly Random rand = new Random();
        private List<Shape> Shapes = new List<Shape>();
        public int ShapeCount = 2;
        public int CornerCount = 4;
        public int HistoryCount = 5;
        public bool Rainbow = false;
        public double Speed = 20;

        public void Advance(double width, double height)
        {
            // delete shapes with incorrect number of corners
            Shapes = Shapes.Where(x => x.Polygons[0].CornerCount == CornerCount).ToList();

            // ensure correct number of shapes
            while (Shapes.Count < ShapeCount)
                Shapes.Add(new Shape(rand, CornerCount, width, height, null));
            while (Shapes.Count > ShapeCount)
                Shapes.RemoveAt(0);

            // advance each shape
            foreach (var shape in Shapes)
                shape.Advance(Speed, width, height, HistoryCount, Rainbow);
        }

        public void RandomizeColors()
        {
            for (int i = 0; i < Shapes.Count; i++)
                Shapes[i].Polygons.Last().Color.Hue = rand.NextDouble();
        }

        public string GetJSON()
        {
            var polyJsonStrings = Shapes.SelectMany(x => x.Polygons).Select(x => x.GetJSON());
            return "[" + string.Join(',', polyJsonStrings) + "]";
        }
    }
}
