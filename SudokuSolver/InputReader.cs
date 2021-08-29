using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class InputReader
    {
        private readonly TextReader input;
        public InputReader(System.IO.TextReader inputStream)
        {
            input = inputStream;
        }

        public int[] ReadInput()
        {
            int[] readValues = new int[81];

            if (input == null)
            {
                return null;
            }
            int i = 0;
            int next;
            while(i != 81 && (next = input.Read()) != -1)
            {
                switch(next)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        readValues[i++] = Convert.ToInt32(next - '0');
                        break;
                }
            }
            if (i != 81)
            {
                for (int j = i; j < 81; j++)
                {
                    readValues[j] = 0;
                }
            }
            return readValues;
        }
    }
}
