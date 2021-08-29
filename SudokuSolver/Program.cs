using System;
using Microsoft.Extensions.Configuration;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Sudoku!");

            var cmd = new ConfigurationBuilder();
            cmd.AddCommandLine(args);
            var config = cmd.Build();

            InputReader reader; ;

            if (config["i"] != null)
            {
                System.IO.StreamReader sr = new(config["i"]);
                reader = new(sr);
            }
            else
            {
                Console.WriteLine("No input supplied, reading from Console:");
                Console.WriteLine("Type board values (0=empty cell):");

                reader = new(Console.In);
            }

            var initial = reader.ReadInput();

            Board board = new(initial);

            SudokuPrinter printer = new(Console.Out);

            Console.WriteLine("Initial Sodoku board");
            printer.Print(board);

            Solver solver = new(board);

            DateTime tStart = DateTime.Now;
            solver.Solve();
            DateTime tEnd = DateTime.Now;
            var t = tEnd - tStart;

            if (solver.IsSolved())
            {
                Console.WriteLine("Solved Sodoku board in {0} mSec", t.TotalMilliseconds);
            }
            else
            {
                Console.WriteLine("Could not solve Sodoku board");

                printer.Print(board);

                Console.WriteLine("Try with backtrack on result achieved so far");

                Backtrack bt = new(board);

                tStart = DateTime.Now;

                bt.Solve(0);
                tEnd = DateTime.Now;
                t = tEnd - tStart;
                if (bt.IsSolved())
                {
                    Console.WriteLine("Solved Sodoku board with backtrack in {0} mSec", t.TotalMilliseconds);
                }
                else
                {
                    Console.WriteLine("Could not solve Sodoku board with backtrack either");
                }
            }

            printer.Print(board);
        }

    }
}
