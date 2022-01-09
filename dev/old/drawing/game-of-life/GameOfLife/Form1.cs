using LifeModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Form1 : Form
    {
        Board board;

        public Form1()
        {
            InitializeComponent();
            Reset();
        }

        // GUI actions that require a board reset
        private void ResetButton_Click(object sender, EventArgs e) { Reset(); }
        private void pictureBox1_SizeChanged(object sender, EventArgs e) { Reset(); }
        private void SizeNud_ValueChanged(object sender, EventArgs e) { Reset(); }
        private void DensityNud_ValueChanged(object sender, EventArgs e) { Reset(); }

        private void Reset(bool randomize = true)
        {
            board = new Board(pictureBox1.Width, pictureBox1.Height, (int)SizeNud.Value);
            if (randomize)
                board.Randomize((double)DensityNud.Value / 100);
            Render();
        }

        private void Reset(string startingPattern)
        {
            string[] lines = startingPattern.Split('\n');
            int yOffset = (board.Rows - lines.Length) / 2;
            int xOffset = (board.Columns - lines[0].Length) / 2;

            Reset(randomize: false);
            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[y].Length; x++)
                    board.Cells[x + xOffset, y + yOffset].IsAlive = lines[y].Substring(x, 1) == "X";

            Render();
        }

        // adjustments to timer
        private void RunCheckbox_CheckedChanged(object sender, EventArgs e) { timer1.Enabled = RunCheckbox.Checked; }
        private void DelayNud_ValueChanged(object sender, EventArgs e) { timer1.Interval = (int)DelayNud.Value; }
        private void timer1_Tick(object sender, EventArgs e)
        {
            board.Advance();
            Render();
        }

        // drawing the board
        private void Render()
        {
            using (var bmp = new Bitmap(board.Width, board.Height))
            using (var gfx = Graphics.FromImage(bmp))
            using (var brush = new SolidBrush(Color.LightGreen))
            {
                gfx.Clear(ColorTranslator.FromHtml("#2f3539"));

                var cellSize = (GridCheckbox.Checked && board.CellSize > 1) ?
                                new Size(board.CellSize - 1, board.CellSize - 1) :
                                new Size(board.CellSize, board.CellSize);

                for (int col = 0; col < board.Columns; col++)
                {
                    for (int row = 0; row < board.Rows; row++)
                    {
                        var cell = board.Cells[col, row];
                        if (cell.IsAlive)
                        {
                            var cellLocation = new Point(col * board.CellSize, row * board.CellSize);
                            var cellRect = new Rectangle(cellLocation, cellSize);
                            gfx.FillRectangle(brush, cellRect);
                        }
                    }
                }

                pictureBox1.Image?.Dispose();
                pictureBox1.Image = (Bitmap)bmp.Clone();
            }
        }

        private void GliderButton_Click(object sender, EventArgs e)
        {
            string startingPattern = "-X-\n" +
                                     "--X\n" +
                                     "XXX";
            Reset(startingPattern);
        }

        private void RowButton_Click(object sender, EventArgs e)
        {
            string complexRow = 
                "XXXXXXXX-XXXXX---XXX------XXXXXXX-XXXXX";
            Reset(complexRow);
        }

        private void SpaceshipButton_Click(object sender, EventArgs e)
        {
            string spaceship = 
                "--XX-\n" +
                "-XXXX\n" +
                "XX-XX\n" +
                "-XX--";
            Reset(spaceship);
        }

        private void GunButton_Click(object sender, EventArgs e)
        {
            string gliderGun =
                "-------------------------X----------\n" +
                "----------------------XXXX----X-----\n" +
                "-------------X-------XXXX-----X-----\n" +
                "------------X-X------X--X---------XX\n" +
                "-----------X---XX----XXXX---------XX\n" +
                "XX---------X---XX-----XXXX----------\n" +
                "XX---------X---XX--------X----------\n" +
                "------------X-X---------------------\n" +
                "-------------X----------------------";
            Reset(gliderGun);
        }
    }
}
