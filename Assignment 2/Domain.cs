using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2 {
    public class Domain { // A domain keeps track of which values a cell can still have
        public int Count { get; set; }
        int domain; // The domain stores the information as 9 bits

        public Domain(int value) {
            if (value == 0) {
                domain = 511; // Set last 9 bits
            } else {
                domain = 1 << (value - 1); // Sets only the bit corresponding to the value
            }
            Count = CountSetBits();
        }

        public bool Contains(int value) {
            // Check if the domain contains the value
            return (((domain >> (value - 1)) & 1) == 1);
        }

        public bool Remove(int value) {
            // Removes a value from the domain, returns true if successfull
            if (value != 0 && Contains(value)) {
                domain = (domain ^ (1 << (value - 1)));
                Count--;
                return true;
            }
            return false;
        }

        public void Add(int value) {
            // Adds a value to the domain
            if (value != 0 && !Contains(value)) {
                domain = (domain | (1 << (value - 1)));
                Count++;
            }
        }

        private int CountSetBits() {
            // Counts the size of the domain
            int count = 0;
            for (int i = 1; i <= 9; i++) {
                if (Contains(i))
                    count++;
            }
            return count;
        }

        public override string ToString() {
            // Lists the domain as a string
            string result = "";
            for (int i = 9; i >= 1; i--) {
                if (Contains(i)) {
                    result += i;
                } else {
                    result += "0";
                }
            }
            return result;
        }
    }
}
