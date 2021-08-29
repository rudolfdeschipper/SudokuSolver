using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class SudokuPrinter
    {
        private readonly TextWriter Output = null;
        public SudokuPrinter(TextWriter output)
        {
            Output = output;
        }
        public void Print(Board board)
        {
            if (Output == null)
            {
                return;
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var s = board.cells[(i * 9) + j].FixedValue;
                    Output.Write(' ');
                    if (s == 0)
                    {
                        Output.Write('_');
                    }
                    else
                    {
                        Output.Write(s);
                    }
                    Output.Write(' ');
                }
                Output.WriteLine();
                Output.WriteLine();
            }
        }
    }
}
