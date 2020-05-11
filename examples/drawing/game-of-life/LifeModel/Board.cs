using System;

namespace LifeModel
{
    public class Board
    {
        public readonly Cell[,] Cells;
        public readonly int CellSize;

        public int Columns { get { return Cells.GetLength(0); } }
        public int Rows { get { return Cells.GetLength(1); } }
        public int Width { get { return Columns * CellSize; } }
        public int Height { get { return Rows * CellSize; } }

        readonly Random rand = new Random();

        public Board(int width, int height, int cellSize, bool wrap = true)
        {
            CellSize = cellSize;

            Cells = new Cell[width / cellSize, height / cellSize];
            for (int x = 0; x < Columns; x++)
                for (int y = 0; y < Rows; y++)
                    Cells[x, y] = new Cell();

            ConnectNeighbors(wrap);
        }

        private void ConnectNeighbors(bool wrap)
        {
            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    bool isLeftEdge = (x == 0);
                    bool isRightEdge = (x == Columns - 1);
                    bool isTopEdge = (y == 0);
                    bool isBottomEdge = (y == Rows - 1);
                    bool isEdge = isLeftEdge | isRightEdge | isTopEdge | isBottomEdge;

                    if ((wrap == false) && isEdge)
                        continue;

                    int xL = isLeftEdge ? Columns - 1 : x - 1;
                    int xR = isRightEdge ? 0 : x + 1;
                    int yT = isTopEdge ? Rows - 1 : y - 1;
                    int yB = isBottomEdge ? 0 : y + 1;

                    Cells[x, y].neighbors.Add(Cells[xL, yT]);
                    Cells[x, y].neighbors.Add(Cells[x, yT]);
                    Cells[x, y].neighbors.Add(Cells[xR, yT]);
                    Cells[x, y].neighbors.Add(Cells[xL, y]);
                    Cells[x, y].neighbors.Add(Cells[xR, y]);
                    Cells[x, y].neighbors.Add(Cells[xL, yB]);
                    Cells[x, y].neighbors.Add(Cells[x, yB]);
                    Cells[x, y].neighbors.Add(Cells[xR, yB]);
                }
            }
        }

        public void Randomize(double liveDensity)
        {
            foreach (var cell in Cells)
                cell.IsAlive = rand.NextDouble() < liveDensity;
        }

        public void Advance()
        {
            foreach (var cell in Cells)
                cell.DetermineNextLiveState();
            foreach (var cell in Cells)
                cell.Advance();
        }
    }
}
