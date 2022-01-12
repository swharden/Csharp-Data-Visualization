using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life
{
    public static class Render
    {
        public static void DrawBoard(Board board, ICanvas canvas, float width, float height)
        {
            Color backgroundColor = Color.FromArgb("#1c395b");
            Color squareColor = Color.FromArgb("#7eabe0");

            canvas.FillColor = backgroundColor;
            canvas.FillRectangle(0, 0, width, height);
            canvas.FillColor = squareColor;

            float cellWidthPx = width / board.Columns;
            float cellHeightPx = height / board.Rows;

            float borderFrac = .1f;
            float xPad = borderFrac * cellWidthPx;
            float yPad = borderFrac * cellHeightPx;

            for (int rowIndex = 0; rowIndex < board.Rows; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < board.Columns; columnIndex++)
                {
                    if (board.Cells[rowIndex, columnIndex] > 0)
                    {
                        RectangleF rect = new(
                            x: columnIndex * cellWidthPx + xPad,
                            y: rowIndex * cellHeightPx + yPad,
                            width: cellWidthPx - xPad * 2,
                            height: cellHeightPx - yPad * 2);

                        canvas.FillRectangle(rect);
                    }
                }
            }
        }
    }
}
