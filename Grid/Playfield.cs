using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TetrisTutorial.Assets.Tetrimino;

namespace TetrisTutorial.Grid
{
    internal class Playfield
    {
        //grid is structured[rows][columns]
        private const int COLUMNS = 10;
        private const int ROWS = 20;

        private List<int> _completedLines = new();
        private Cell[][] _cells;
        private Vector3 _position;

        public Playfield(Vector3 position)
        {
            _cells = new Cell[ROWS][];
            _position = position;

            for (int i = 0; i < ROWS; i++)
            {
                _cells[i] = new Cell[COLUMNS];

                for (int j = 0; j < COLUMNS; j++)
                    _cells[i][j] = new Cell() { Occupied = false, Color = Color.Black };
                // Color doesn't really matter as we're not going to draw unoccupied spaces.
            }
        }

        public void Draw()
        {
            for (int y = 0; y < ROWS; y++)
            {
                //left border
                Assets.Models.DrawCube(
                    Matrix.CreateTranslation(_position) * Matrix.CreateTranslation(-1 * 0.2f, -y * 0.2f, 0),
                    Color.Gray);

                //right border
                Assets.Models.DrawCube(
                    Matrix.CreateTranslation(_position) * Matrix.CreateTranslation(COLUMNS * 0.2f, -y * 0.2f, 0),
                    Color.Gray);

                for (int x = 0; x < COLUMNS; x++)
                {
                    if (!_cells[y][x].Occupied)
                        continue;

                    Assets.Models.DrawCube(
                       Matrix.CreateTranslation(_position) * Matrix.CreateTranslation(x * 0.2f, -y * 0.2f, 0),
                       _cells[y][x].Color);
                }
            }

            //Draw bottom of the grid
            for (int x = -1; x < COLUMNS + 1; x++)
            {
                Assets.Models.DrawCube(
                    Matrix.CreateTranslation(_position) * Matrix.CreateTranslation(x * 0.2f, -20 * 0.2f, 0),
                    Color.Gray);
            }
        }

        public bool LockInPlace(Tetrimino tetrimino, int leftColumn, int topLine)
        {
            if (topLine < 0)
                return false;

            for (int y = 0; y < tetrimino.CurrentShape.ShapeBits.Length; y++)
            {
                for (int x = 0; x < tetrimino.CurrentShape.ShapeBits[y].Length; x++)
                {
                    if (tetrimino.CurrentShape.ShapeBits[y][x])
                    {
                        _cells[topLine + y][leftColumn + x].Occupied = true;
                        _cells[topLine + y][leftColumn + x].Color = tetrimino.Color;
                    }
                }
            }

            return true;
        }

        public bool DoesShapeFitHere(Tetrimino shape, int leftcolumn, int topline)
        {
            //loop over the bits in our shape:
            for (int y = 0; y < shape.CurrentShape.ShapeBits.Length; y++)
            {
                for (int x = 0; x < shape.CurrentShape.ShapeBits[y].Length; x++)
                {
                    //We only need to check bits that are set to true
                    if (shape.CurrentShape.ShapeBits[y][x])
                    {
                        //so this is a filled bit of the shape!

                        //we return false if the shape tries to fit in the border:
                        //check for bottom:
                        if (topline + y >= ROWS)
                            return false;

                        //check for left wall:
                        if (leftcolumn + x < 0)
                            return false;

                        //check for right wall:
                        if (leftcolumn + x >= COLUMNS)
                            return false;

                        //We're not checking the top- a piece spawns above the playfield!
                        //bonus, we prevent the array out of bounds
                        if (topline + y < 0)
                            continue;

                        //now for the grid:
                        //if both the bit in the shape is true
                        //and the cell is occupied, 
                        //the shape can not fit!
                        if (_cells[topline + y][leftcolumn + x].Occupied)
                            return false;
                    }
                }
            }

            //all checks came out clear, so it fits!
            return true;
        }

        public int ValidateField()
        {
            _completedLines.Clear();

            for (int y = 0; y < ROWS; y++)
            {
                bool lineClear = true;
                for (int x = 0; x < COLUMNS; x++)
                {
                    if (!_cells[y][x].Occupied)
                    {
                        //unoccupied space in this line, no need to check further.
                        lineClear = false;
                        break;
                    }
                }

                if (lineClear)
                {
                    _completedLines.Add(y);
                }
            }

            return _completedLines.Count;
        }

        public void ClearLines()
        {
            foreach (int row in _completedLines)
                ClearLine(row);
        }

        private void ClearLine(int y)
        {
            for (int row = y; row > 0; row--)
            {
                _cells[row] = CopyLine(row - 1);
            }

            for (int column = 0; column < COLUMNS; column++)
            {
                Cell c = new() { Occupied = false, Color = Color.Transparent };
                _cells[0][column] = c;
            }
        }

        private Cell[] CopyLine(int line)
        {
            Cell[] cells = new Cell[COLUMNS];
            for (int column = 0; column < COLUMNS; column++)
            {
                Cell c = new() { Occupied = _cells[line][column].Occupied, Color = _cells[line][column].Color };
                cells[column] = c;
            }
            return cells;
        }
    }
}
