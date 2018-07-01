using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2 {
    class ForwardCheckerMCV : Solver {
        public ForwardCheckerMCV(int[,] values) : base(values) {
            List<Cell> toSolve = new List<Cell>();
            for (int x = 0; x < 9; x++) {
                for (int y = 0; y < 9; y++) {
                    toSolve.Add(cells[x, y]);
                }
            }
            if (Solve(SortCells(toSolve))) {
                PrintCells();
            } else {
                Console.WriteLine("Sudoku could not be solved");
            }
        }

        public bool Solve(List<Cell> toSolve) {
            if (toSolve.Count == 0)
                return true;
            Cell cell = toSolve[0];
            toSolve.RemoveAt(0);
            if (cell.value != 0) {
                CheckForward(cell, cell.value);
                return Solve(SortCells(toSolve));
            }
            for (int value = 1; value <= 9; value++) {
                if (cell.domain.Contains(value)) {
                    cell.value = value;
                    List<int> changedCells = CheckForward(cell, value);
                    if (Solve(SortCells(toSolve))) {
                        return true;
                    }
                    RevertChanges(changedCells, value);
                    cell.value = 0;
                }
            }
            return false;
        }

        public List<Cell> SortCells(List<Cell> toSolve) {
            return toSolve.OrderBy(cell => cell.domain.Count).ToList();
        }

        public List<int> CheckForward(Cell cell, int value) {
            List<int> changedCells = new List<int>();
            for (int i = 0; i < 9; i++) {
                if (cells[i, cell.y].domain.Remove(value)) {
                    changedCells.Add((cell.y << 4) + i);
                }
                if (cells[cell.x, i].domain.Remove(value)) {
                    changedCells.Add((i << 4) + cell.x);
                }
            }

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
            int changedCell;
            for (int i = 0; i < changedCells.Count; ++i) {
                changedCell = changedCells[i];
                cells[changedCell & 15, changedCell >> 4].domain.Add(value);
            }
        }
    }
}
