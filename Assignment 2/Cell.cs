using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2 {
    public class Cell { // A cell contains it's position, assigned value and the domain from which values can still be taken.
        public int x, y, value;
        public Domain domain;

        public Cell(int x, int y, int value) {
            this.x = x;
            this.y = y;
            this.value = value;
            this.domain = new Domain(value);
        }
    }
}
