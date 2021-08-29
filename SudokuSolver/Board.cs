using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Board
    {
        public readonly Cell[] cells = new Cell[81];

        public Board(int[] initial)
        {
            if (initial.Length == 81)
            {
                for (int i = 0; i < 81; i++)
                {
                    cells[i] = new Cell(i, initial[i]);
                }
            }
            else
            {
                int len = Math.Min(81, initial.Length);
                int rest = 81 - len;
                for (int i = 0; i < len; i++)
                {
                    cells[i] = new Cell(i, initial[i]);
                }
                for (int i = len; i < rest; i++)
                {
                    cells[i] = new Cell(i);
                }

            }
        }

        public Board()
        {
            for (int i = 0; i < 81; i++)
            {
                cells[i] = new Cell(i);
            }
        }
    }
}
