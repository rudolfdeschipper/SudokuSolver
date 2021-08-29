using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Backtrack
    {

        public Cell[] CellsToSolve { get; set; }
        public Board board { get; set; }

        public Backtrack(Board _board)
        {
            board = _board;
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
                if (!board.cells[i].IsSolved()) return false;
            }
            return true;
        }

        private bool IsValidConfiguration(Cell currentCell)
        {
            var isRowValid = board.cells.Count(c => c.Row == currentCell.Row && c.FixedValue == currentCell.FixedValue) == 1;
            var isColumnValid = board.cells.Count(c => c.Column == currentCell.Column && c.FixedValue == currentCell.FixedValue) == 1;
            var isAreaValid = board.cells.Count(c => c.Area == currentCell.Area && c.FixedValue == currentCell.FixedValue) == 1;

            return isRowValid && isColumnValid && isAreaValid;
        }
    }
}
