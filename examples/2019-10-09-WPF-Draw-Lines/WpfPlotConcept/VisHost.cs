using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace WpfPlotConcept
{
    public class VisHost : FrameworkElement
    {
        public DrawVis drawVis = new DrawVis();

        public VisHost()
        {
            this.AddVisualChild(drawVis);
            this.AddLogicalChild(drawVis);
        }

        // EllipseAndRectangle instance is our only visual child
        protected override Visual GetVisualChild(int index)
        {
            return drawVis;
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return 1;
            }
        }
    }
}
