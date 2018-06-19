using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronologicalBacktracking
{
    class Program
    {
        static void Main(string[] args)
        {
            Solver.Solve(new ChronologicalBacktracker(), new Sudoku());
        }
    }
}
