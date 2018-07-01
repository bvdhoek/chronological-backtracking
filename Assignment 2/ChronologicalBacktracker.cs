using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2 {
    class ChronologicalBacktracker : Solver {
        public ChronologicalBacktracker(int[,] values) : base(values) {
            // Start with the top-left most cell
            if (Solve(cells[0, 0])) {
                // Print the solved sudoku
                PrintCells();
            } else {
                // The sudoku could not be solved
                Console.WriteLine("Sudoku could not be solved");
            }
        }

        public bool Solve(Cell cell) {
            if (cell == null) { return true; } // Return if there's no cell to be solved
            nodeExpansions++;
            if (cell.value != 0) { return Solve(NextCell(cell)); } // Continue with the next cell if the cell already has a value

            // Check each possible value
            for (int value = 1; value <= 9; value++) {
                if (!SatisfiesConstraints(cell, value)) { continue; } // Immediatly check the next value if a constraint is broken

                cell.value = value;
                if (Solve(NextCell(cell))) { // Check the next cell
                    return true;
                }
            }
            // Return false if each possible value breaks a contraint
            cell.value = 0;
            return false;
        }

        public Cell NextCell(Cell cell) {
            // Gets the next cell based on it's position
            int x = cell.x;
            int y = cell.y;

            if (cell.x == 8) {
                x = 0;
                y++;
            } else {
                x++;
            }

            if (y > 8)
                return null;
            return cells[x, y];
        }

        public bool SatisfiesConstraints(Cell cell, int value) {
            for (int i = 0; i < 9; i++) { // Check the rows and columns
                if (cells[i, cell.y].value == value || cells[cell.x, i].value == value)
                    return false;
            }

            // Check the block of the cell
            int x1 = 3 * (cell.x / 3);
            int y1 = 3 * (cell.y / 3);
            int x2 = x1 + 2;
            int y2 = y1 + 2;
            for (int x = x1; x <= x2; x++) {
                for (int y = y1; y <= y2; y++) {
                    if (cells[x, y].value == value)
                        return false;
                }
            }
            return true;
        }
    }
}
