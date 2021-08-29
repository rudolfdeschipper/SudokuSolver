using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Cell
    {
        public int ItemNr { get; private set; }
        public int Row { get; private set; }
        public int Column { get; private set; }
        public int Area { get; private set; }
        public int FixedValue { get; set; }
        public bool[] PossibleValues { get; private set; } = new bool[10];
        public int InitialValue { get; private set; }
        public Cell(int item)
        {
            ItemNr = item;
            Row = item / 9;
            Column = item % 9;
            Area = 3 * (Row / 3) + (Column / 3);
            for (int i = 0; i < 10; i++)
            {
                PossibleValues[i] = true;
            }
            PossibleValues[0] = false;
        }

        public Cell(int item, int initial) : this(item)
        {
            if (initial > 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    PossibleValues[i] = false;
                }
                PossibleValues[initial] = true;
            }
            InitialValue = initial;
            FixedValue = initial;
        }

        public bool IsSolved()
        {
            return FixedValue > 0;
        }

        public bool Solve(int value)
        {
            if (IsSolved() ||! IsPossible(value))
            {
                return false;
            }
            FixedValue = value;
            for (int i = 1; i < 10; i++)
            {
                PossibleValues[i] = false;
            }
            PossibleValues[value] = true;
            return true;
        }

        public void SolveForBacktrack(int value)
        {
            FixedValue = value;
        }

        public void UnsolveForBacktrack()
        {
            FixedValue = 0;
        }

        public bool IsPossible(int value)
        {
            return PossibleValues[value];
        }
    }
}
