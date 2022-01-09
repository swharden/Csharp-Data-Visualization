using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace WpfPlotConcept
{
    public class DrawVis : DrawingVisual
    {
        Random rand = new Random();

        public DrawVis()
        {
            Render();
        }

        public void Render(int pointCount = 1_000, int width = 200, int height = 200)
        {
            Pen pen = new Pen(Brushes.Black, 1);
            using DrawingContext dc = RenderOpen();
            for (int i = 0; i < 1_000; i++)
            {
                Point pt1 = new Point(rand.NextDouble() * width, rand.NextDouble() * height);
                Point pt2 = new Point(rand.NextDouble() * width, rand.NextDouble() * height);
                dc.DrawLine(pen, pt1, pt2);
            }
        }
    }
}
