using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronologicalBacktracking
{
    class Solver
    {
        Backtracker backtracker;
        Sudoku sudoko;

        public Solver(Backtracker backtracker, Sudoku sudoko)
        {
            this.backtracker = backtracker;
            this.sudoko = sudoko;
        }

        public static Sudoku Solve(Backtracker backtracker, Sudoku sudoku)
        {
            return new Solver(backtracker, sudoku).SolvePuzzle();
        }

        private Sudoku SolvePuzzle()
        {
            return new Sudoku();
        }
    }
}
