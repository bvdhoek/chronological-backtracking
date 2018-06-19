using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronologicalBacktracking {
    class Sudoku {
        Dictionary<int, Node> puzzle;
        Sudoku() {
            puzzle = new Dictionary<int, Node>();
            for (int y = 0; y < 9; y++) {
                for (int x = 0; x < 9; x++) {
                    puzzle.Add(x + y * 9, new Node(x, y));
                }
            }
            for (int y = 0; y < 9; y++) {
                string line = Console.ReadLine();
                for (int x = 0; x < 9; x++) {
                    int value = line[x] - '0';
                    if (value > 0) {
                        if(!SetNode(x, y, value)) {
                            Console.WriteLine("Unsolvable sudoku");
                        }
                    }
                }
            }
        }

        bool SetNode(int x, int y, int value) {
            puzzle[x + y * 9].SetUnchangable(value);
            for (int i = 0; i < 9; i++) {
                if (i != y) {
                    if (puzzle[x + i * 9].RemovePossibility(value) == 0) {
                        return false;
                    }
                }
                if (i != x) {
                    if (puzzle[i + y * 9].RemovePossibility(value) == 0) {
                        return false;
                    }
                }
            }
            for (int i = y / 3 * 3;  i < (y + 1) / 3 * 3; i++) {
                for (int j = x / 3 * 3; j < (x + 1) / 3 * 3; j++) {
                    if (puzzle[j + i * 9].RemovePossibility(value) == 0) {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
