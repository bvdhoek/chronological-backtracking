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
        Sudoko sudoko;

        public Solver(Backtracker backtracker, Sudoko sudoko)
        {
            this.backtracker = backtracker;
            this.sudoko = sudoko;
        }
    }
}
