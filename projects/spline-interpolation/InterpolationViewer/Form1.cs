namespace SplineInterpolationViewer
{
    public partial class Form1 : Form
    {
        readonly List<double> Xs = new();
        readonly List<double> Ys = new();
        readonly ScottPlot.Plot Plot = new();

        public Form1()
        {
            InitializeComponent();
        }

        private void Reset()
        {
            Xs.Clear();
            Ys.Clear();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}