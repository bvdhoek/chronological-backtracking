using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2 {
    class Program {
        static void Main(string[] args) {
            string SukdokuName = Console.ReadLine();
            int[,] values = new int[9, 9];
            for (int y = 0; y < 9; y++) {
                string line = Console.ReadLine();
                for (int x = 0; x < 9; x++) {
                    values[x, y] = line[x] - '0';
                }
            }
            //int[,] values = { { 3, 0, 6, 5, 0, 8, 4, 0, 0 },
            //                { 5, 2, 0, 0, 0, 0, 0, 0, 0 },
            //                { 0, 8, 7, 0, 0, 0, 0, 3, 1 },
            //                { 0, 0, 3, 0, 1, 0, 0, 8, 0 },
            //                { 9, 0, 0, 8, 6, 3, 0, 0, 5 },
            //                { 0, 5, 0, 0, 9, 0, 6, 0, 0 },
            //                { 1, 3, 0, 0, 0, 0, 2, 5, 0 },
            //                { 0, 0, 0, 0, 0, 0, 0, 7, 4 },
            //                { 0, 0, 5, 2, 0, 6, 3, 0, 0 } };
            Console.WriteLine(SukdokuName);
            Solver solver = new ForwardCheckerMCV(values);

            //Cell cell = new Cell(0, 0, 4);
            //Console.WriteLine(cell.domain.ToString());


            Console.ReadLine();
        }
    }
}
