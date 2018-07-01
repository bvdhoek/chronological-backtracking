using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2 {
    class ForwardChecker : Solver {
        public ForwardChecker(int[,] values) : base(values) {
            for (int y = 0; y < 9; y++) {
                for (int x = 0; x < 9; x++) {
                    if (cells[x, y].value != 0) {
                        CheckForward(cells[x, y], cells[x, y].value);
                    }
                }
            }
            if (Solve(cells[0, 0])) {
                PrintCells();
            } else {
                Console.WriteLine("Sudoku could not be solved");
            }
        }

        public override bool Solve(Cell cell) {
            if (cell == null)
                return true;
            if (cell.value != 0) {
                //Console.WriteLine(cell.x + ", " + cell.y + ": " + cell.value);
                return Solve(NextCell(cell));
            }
            //Console.WriteLine(cell.x + ", " + cell.y + ": " + cell.domain.ToString());
            for (int value = 1; value <= 9; value++) {
                if (cell.domain.Contains(value)) {
                    cell.value = value;
                    List<int> changedCells = CheckForward(cell, value);
                    if (Solve(NextCell(cell))) {
                        return true;
                    }
                    RevertChanges(changedCells, value);
                    cell.value = 0;
                }
            }

            return false;

        }

        public override Cell NextCell(Cell cell) {
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
