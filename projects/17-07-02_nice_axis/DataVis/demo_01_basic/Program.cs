namespace demo_01_basic
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ScottPlot2.ScottPlot SP = new ScottPlot2.ScottPlot();
            ScottPlot2.Generate SPgen = new ScottPlot2.Generate();

            SP.SetSize(1500, 400);
            SP.AddLine(SPgen.Sequence(5000), SPgen.Sine(5000));

            SP.Render();
            SP.SaveFig("test.jpg");
        }
    }
}