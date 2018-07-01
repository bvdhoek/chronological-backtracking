﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2 {
    public class Domain {
        public int Count { get; set; }
        int domain;

        public Domain(int value) {
            if (value == 0) {
                domain = 511; // set last 9 bits
            } else {
                domain = 1 << (value - 1);
            }
            Count = CountSetBits();
        }

        public bool Contains(int n) {
            return (((domain >> (n - 1)) & 1) == 1);
        }

        public bool Remove(int n) {
            if (n != 0 && Contains(n)) {
                domain = (domain ^ (1 << (n - 1)));
                Count--;
                return true;
            }
            return false;
        }

        public bool Add(int n) {
            if (n != 0 && !Contains(n)) {
                domain = (domain | (1 << (n - 1)));
                Count++;
                return true;
            }
            return false;
        }

        private int CountSetBits() {
            int count = 0;
            for (int i = 1; i <= 9; i++) {
                if (Contains(i))
                    count++;
            }
            return count;
        }

        public override string ToString() {
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
