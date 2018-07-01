using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2 {
    class ChronologicalBacktrackerMCV : Solver {
        public List<Cell> orderedCells;

        public ChronologicalBacktrackerMCV(int[,] values) : base(values) {
            // Put all cells into a list
            orderedCells = new List<Cell>();
            for (int x = 0; x < 9; x++) {
                for (int y = 0; y < 9; y++) {
                    orderedCells.Add(cells[x, y]);
                }
            }
            // Calculate the domains
            CalculateDomains();
            // Order the cells by domain size
            orderedCells = orderedCells.OrderBy(cell => cell.domain.Count).ToList();
            // Start with the first cell in the oredered list
            if (Solve(orderedCells[0])) {
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
            // Get the next cell in the list
            int idx = orderedCells.IndexOf(cell) + 1;
            if (idx >= 81)
                return null;
            Cell c = orderedCells[idx];
            return c;
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

        private void CalculateDomains() {
            // Calculate the domain dor each cell
            for (int x = 0; x < 9; x++) {
                for (int y = 0; y < 9; y++) {
                    CalculateDomain(cells[x, y]);
                }
            }
        }

        private void CalculateDomain(Cell cell) {
            for (int i = 0; i < 9; i++) {
                cell.domain.Remove(cells[i, cell.y].value); // Removes values in the same row
                cell.domain.Remove(cells[cell.x, i].value); // Romoves values in the same column
            }

            // Removes values in the same block
            int x1 = 3 * (cell.x / 3);
            int y1 = 3 * (cell.y / 3);
            int x2 = x1 + 2;
            int y2 = y1 + 2;
            for (int x = x1; x <= x2; x++) {
                for (int y = y1; y <= y2; y++) {
                    cell.domain.Remove(cells[x, y].value);
                }
            }
        }
    }
}
