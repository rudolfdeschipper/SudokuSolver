using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Backtrack
    {

        private Cell[] CellsToSolve { get; set; }
        private Board BoardToSolve { get; set; }

        public Backtrack(Board _board)
        {
            BoardToSolve = _board;
            CellsToSolve = _board.cells.Where(c => c.FixedValue == 0)
                .OrderBy(c => c.PossibleValues.Count(p => p == true))
                .ToArray();
        }
        public bool Solve(int position)
        {

            if (position == CellsToSolve.Length)
            {
                return true;
            }

            Cell currentCell = CellsToSolve[position];

            for (int i = 1; i < 10; i++)
            {
                if (currentCell.PossibleValues[i])
                {
                    currentCell.SolveForBacktrack(i);
                    if (IsValidConfiguration(currentCell))
                    {
                        if (Solve(position + 1))
                        {
                            return true;
                        }
                    }
                    currentCell.UnsolveForBacktrack();
                }
            }
            return false;
        }

        public bool IsSolved()
        {
            for (int i = 0; i < 81; i++)
            {
                if (!BoardToSolve.cells[i].IsSolved()) return false;
            }
            return true;
        }

        private bool IsValidConfiguration(Cell currentCell)
        {
            var isRowValid = BoardToSolve.cells.Count(c => c.Row == currentCell.Row && c.FixedValue == currentCell.FixedValue) == 1;
            var isColumnValid = BoardToSolve.cells.Count(c => c.Column == currentCell.Column && c.FixedValue == currentCell.FixedValue) == 1;
            var isAreaValid = BoardToSolve.cells.Count(c => c.Area == currentCell.Area && c.FixedValue == currentCell.FixedValue) == 1;

            return isRowValid && isColumnValid && isAreaValid;
        }
    }
}
