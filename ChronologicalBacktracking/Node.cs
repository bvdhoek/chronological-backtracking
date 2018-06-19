using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronologicalBacktracking {
    struct Node {
        public int x, y;
        public List<int> possibleValues;
        public bool changable;

        public Node(int x, int y, List<int> possibleValues) {
            this.x = x;
            this.y = y;
            this.possibleValues = possibleValues;
            changable = true;
        }

        public Node(int x, int y) {
            this.x = x;
            this.y = y;
            possibleValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            changable = true;
        }

        public int RemovePossibility(int value) {
            if (possibleValues.Contains(value)) {
                possibleValues.Remove(value);
            }
            return possibleValues.Count;
        }

        public void SetUnchangable(int value) {
            possibleValues = new List<int>() { value };
            changable = false;
        }
    }
}
