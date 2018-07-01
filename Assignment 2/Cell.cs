using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2 {
    public class Cell {
        public int x, y, value;
        public Domain domain;

        public Cell(int x, int y, int value) {
            this.domain = new Domain();
            this.value = value;
            this.x = x;
            this.y = y;
        }
    }
}
