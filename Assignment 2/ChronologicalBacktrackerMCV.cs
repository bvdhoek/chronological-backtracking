using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2 {
    class ChronologicalBacktrackerMCV : Solver {
        public List<Cell> orderedCells;
        int current = 0;

        public ChronologicalBacktrackerMCV(int[,] values) : base(values) {
            orderedCells = new List<Cell>();
            for (int x = 0; x < 9; x++) {
                for (int y = 0; y < 9; y++) {
                    orderedCells.Add(cells[x, y]);
                }
            }
            CalculateDomains();
            orderedCells = orderedCells.OrderBy(cell => cell.domain.Count).ToList();
            if (Solve(orderedCells[current])) {
                PrintCells();
            } else {
                Console.WriteLine("Sudoku could not be solved");
            }
        }

        public override bool Solve(Cell cell) {
            if (cell == null)
                return true;
            if (cell.value != 0)
                return Solve(NextCell(cell));

            for (int value = 1; value <= 9; value++) {
                if (!SatisfiesConstraints(cell, value) || !cell.domain.Contains(value))
                    continue;

                cell.value = value;
                if (Solve(NextCell(cell))) {
                    return true;
                }
            }
            cell.value = 0;
            return false;
        }

        public override Cell NextCell(Cell cell) {
            current = orderedCells.IndexOf(cell) + 1;
            if (current >= 81)
                return null;
            Cell c = orderedCells[current];
            return c;
        }

        public override bool SatisfiesConstraints(Cell cell, int value) {
            for (int i = 0; i < 9; i++) {
                if (cells[i, cell.y].value == value || cells[cell.x, i].value == value)
                    return false;
            }

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
            for (int x = 0; x < 9; x++) {
                for (int y = 0; y < 9; y++) {
                    CalculateDomain(cells[x, y]);
                }
            }
        }

        private void CalculateDomain(Cell cell) {
            for (int i = 0; i < 9; i++) {
                cell.domain.Remove(cells[i, cell.y].value);
                cell.domain.Remove(cells[cell.x, i].value);
            }

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
