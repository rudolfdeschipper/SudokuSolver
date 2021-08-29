using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Solver
    {
        private readonly Board board;
        public Solver(Board _board)
        {
            board = _board;
        }
        public void Solve()
        {
            bool didSomething;
            do
            {
                didSomething = false;
                if (ReducePossibleValues())
                {
                    didSomething = SolveCells();
                }
            } while (didSomething);
        }

        public bool IsSolved()
        {
            for (int i = 0; i < 81; i++)
            {
                if(!board.cells[i].IsSolved()) return false;
            }
            return true;
        }

        private bool ReducePossibleValues()
        {
            bool didSomething = false;

            foreach (var item in board.cells)
            {
                if (!item.IsSolved())
                {
                    // check row
                    didSomething |= CheckValues(item, board.cells.Where(c => c.Row == item.Row && c.ItemNr != item.ItemNr));
                    // check col
                    didSomething |= CheckValues(item, board.cells.Where(c => c.Column == item.Column && c.ItemNr != item.ItemNr));
                    // check area
                    didSomething |= CheckValues(item, board.cells.Where(c => c.Area == item.Area && c.ItemNr != item.ItemNr));
                }
            }

            return didSomething;
        }

        private static bool CheckValues(Cell cell, IEnumerable<Cell> otherCells)
        {
            bool changed = false;
            for (int i = 1; i < 10; i++)
            {
                if (cell.PossibleValues[i])
                {
                    if (otherCells.Any(c => c.FixedValue == i))
                    {
                        cell.PossibleValues[i] = false;
                        changed = true;
                    }
                }
            }
            return changed;
        }

        private bool SolveCells()
        {
            bool didSomething = false;

            foreach (var item in board.cells)
            {
                if (!item.IsSolved())
                {
                    for (int i = 1; i < 10; i++)
                    {
                        if (item.PossibleValues[i])
                        {
                            // check if this value exists elsewhere
                            var row = board.cells.Where(c => c.Row == item.Row && c.ItemNr != item.ItemNr);
                            var existInRow = row.Any(c => c.PossibleValues[i] || c.FixedValue == i) == true;
                            var column = board.cells.Where(c => c.Column == item.Column && c.ItemNr != item.ItemNr);
                            var existInColumn = column.Any(c => c.PossibleValues[i] || c.FixedValue == i) == true;
                            var area = board.cells.Where(c => c.Area == item.Area && c.ItemNr != item.ItemNr);
                            var existInArea = area.Any(c => c.PossibleValues[i] || c.FixedValue == i) == true;
                            if (!(existInRow && existInColumn && existInArea))
                            {
                                didSomething = item.Solve(i);
                                break;
                            }
                            if (item.PossibleValues.Count(c => c == true) == 1 )
                            {
                                   
                                didSomething = item.Solve(i);
                                break;
                            }
                        }
                    }
                }
            }

            return didSomething;
        }

        // outline of backtracking solution
        // TryCell(cell)
        //      1: take cell possible value
        //      put cell on stack, fix for possible value
        //      setup possible values for this fixed value
        //      take nextcell - if no more, return
        //      TryCell(nextcell)
        //      take value of stack and put board back in state before trycell (recalc possible values)
        //      if !board.IsSolved) goto 1
        //      if no more possible values: return from TryCell
        //
        //
    }
}
