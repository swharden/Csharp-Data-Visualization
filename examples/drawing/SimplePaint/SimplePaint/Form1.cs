using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimplePaint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            using (var diag = new ColorDialog())
            {
                if (diag.ShowDialog() == DialogResult.OK)
                {
                    btnColor.BackColor = diag.Color;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            using (var gfx = panel1.CreateGraphics())
                gfx.Clear(Color.Black);
        }

        Point mouseLocA;
        Point mouseLocB;
        private void DrawMouseLine()
        {
            using (var pen = new Pen(btnColor.BackColor, (int)nudSize.Value))
            using (var gfx = panel1.CreateGraphics())
            {
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                gfx.DrawLine(pen, mouseLocA, mouseLocB);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocA = e.Location;
            mouseLocB = e.Location;
            DrawMouseLine();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseLocA = mouseLocB;
                mouseLocB = e.Location;
                DrawMouseLine();
            }
        }
    }
}
