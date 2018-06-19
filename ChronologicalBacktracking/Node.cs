using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronologicalBacktracking {
    struct Node {
        int x, y;
        List<int> possibleValues;

        Node(int x, int y, List<int> possibleValues) {
            this.x = x;
            this.y = y;
            this.possibleValues = possibleValues;
        }
    }
}
