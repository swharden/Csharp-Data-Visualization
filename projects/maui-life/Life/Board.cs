using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life
{
    public class Board
    {
        public readonly byte[,] Cells;
        public readonly int Columns;
        public readonly int Rows;

        public Board(int columns, int rows)
        {
            Cells = new byte[rows, columns];
            Columns = columns;
            Rows = rows;
            AddRandom(0.2);
        }

        public void AddRandom(double density)
        {
            Random rand = new();
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    if (rand.NextDouble() < density)
                        Cells[i, j] = 1;
                }
            }
        }

        public void Clear()
        {
            Random rand = new();
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    Cells[i, j] = 0;
                }
            }
        }

        public void Advance()
        {
            byte[,] Neighbors = new byte[Rows, Columns];

            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    bool isLeftEdge = (x == 0);
                    bool isRightEdge = (x == Columns - 1);
                    bool isTopEdge = (y == 0);
                    bool isBottomEdge = (y == Rows - 1);
                    bool isEdge = isLeftEdge | isRightEdge | isTopEdge | isBottomEdge;

                    if (isEdge)
                        continue;

                    // wrap
                    int xL = isLeftEdge ? Columns - 1 : x - 1;
                    int xR = isRightEdge ? 0 : x + 1;
                    int yT = isTopEdge ? Rows - 1 : y - 1;
                    int yB = isBottomEdge ? 0 : y + 1;
                    int neighborCount =
                        Cells[yT, xL] + Cells[yT, x] + Cells[yT, xR] +
                        Cells[y, xL] + Cells[y, xR] +
                        Cells[yB, xL] + Cells[yB, x] + Cells[yB, xR];

                    Neighbors[y, x] = (byte)neighborCount;
                }
            }

            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    {
                        bool isAlive = Cells[y, x] == 1;
                        int liveNeighbors = Neighbors[y, x];

                        //Any live cell with fewer than two live neighbours dies, as if by underpopulation.
                        if (isAlive && liveNeighbors < 2)
                        {
                            Cells[y, x] = 0;
                            continue;
                        }

                        //Any live cell with more than three live neighbours dies, as if by overpopulation.
                        if (isAlive && liveNeighbors > 3)
                        {
                            Cells[y, x] = 0;
                            continue;
                        }

                        //Any live cell with two or three live neighbours lives on to the next generation.
                        if (isAlive && liveNeighbors >= 2)
                        {
                            continue;
                        }

                        //Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
                        if (!isAlive && liveNeighbors == 3)
                        {
                            Cells[y, x] = 1;
                            continue;
                        }
                    }
                }
            }
        }
    }
}
