using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2 {
    class ForwardCheckerMCV : Solver {
        public ForwardCheckerMCV(int[,] values) : base(values) {
            // Put all cells into a list
            List<Cell> toSolve = new List<Cell>();
            for (int x = 0; x < 9; x++) {
                for (int y = 0; y < 9; y++) {
                    toSolve.Add(cells[x, y]);
                }
            }
            // Solve for the list of cells
            if (Solve(SortCells(toSolve))) {
                // Print the solved sudoku
                PrintCells();
            } else {
                // The sudoku could not be solved
                Console.WriteLine("Sudoku could not be solved");
            }
        }

        public bool Solve(List<Cell> toSolve) {
            if (toSolve.Count == 0) { return true; } // Return if there's no cell to be solved
            Cell cell = toSolve[0]; // Solve for the first cell in the list
            toSolve.RemoveAt(0);
            nodeExpansions++;

            // Continue with the next cell if the cell already has a value
            if (cell.value != 0) {
                CheckForward(cell, cell.value);
                return Solve(SortCells(toSolve));
            }

            // Check each possible value
            for (int value = 1; value <= 9; value++) {
                // only check if the value is in the domain
                if (cell.domain.Contains(value)) {
                    cell.value = value;
                    List<int> changedCells = CheckForward(cell, value); // Keep track of the changes due to the forward checking
                    if (Solve(SortCells(toSolve))) { // Check the next cells
                        return true;
                    }
                    RevertChanges(changedCells, value); // Revert the changes of the forward checking
                }
            }
            // Return false if each possible value breaks a contraint
            cell.value = 0;
            return false;
        }

        public List<Cell> SortCells(List<Cell> toSolve) {
            // Sort the list of cell to solve on domain size
            return toSolve.OrderBy(cell => cell.domain.Count).ToList();
        }

        public List<int> CheckForward(Cell cell, int value) {
            // Remove the value from each domain in the same row, cloumn or block as the cell
            List<int> changedCells = new List<int>(); // Keep track of the changes
            for (int i = 0; i < 9; i++) {
                if (cells[i, cell.y].domain.Remove(value)) { // Check each row
                    changedCells.Add((cell.y << 4) + i);
                }
                if (cells[cell.x, i].domain.Remove(value)) { // Check each column
                    changedCells.Add((i << 4) + cell.x);
                }
            }

            // Check each block
            int x1 = 3 * (cell.x / 3);
            int y1 = 3 * (cell.y / 3);
            int x2 = x1 + 2;
            int y2 = y1 + 2;
            for (int x = x1; x <= x2; x++) {
                for (int y = y1; y <= y2; y++) {
                    if (cells[x, y].domain.Remove(value)) {
                        changedCells.Add((y << 4) + x);
                    }
                }
            }
            return changedCells.Distinct().ToList();
        }

        public void RevertChanges(List<int> changedCells, int value) {
            // Revert changes form the forward checking
            int changedCell;
            for (int i = 0; i < changedCells.Count; ++i) {
                changedCell = changedCells[i];
                cells[changedCell & 15, changedCell >> 4].domain.Add(value);
            }
        }
    }
}
