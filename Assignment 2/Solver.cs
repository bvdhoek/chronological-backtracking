using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2 {
    public abstract class Solver {
        public Cell[,] cells;

        public Solver(int[,] values) {
            this.cells = new Cell[9, 9];
            for (int x = 0; x < 9; x++) {
                for (int y = 0; y < 9; y++) {
                    cells[x, y] = new Cell(x, y, values[x, y]);
                }
            }
        }

        public void PrintCells() {
            for (int y = 0; y < 9; y++) {
                for (int x = 0; x < 9; x++) {
                    Console.Write(cells[x, y].value);
                }
                Console.Write("\n");
            }
        }

        public abstract bool Solve(Cell cell);
        public abstract Cell NextCell(Cell cell);
        public abstract bool SatisfiesConstraints(Cell cell, int value);
    }
}
