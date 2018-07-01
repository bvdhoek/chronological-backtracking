using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Assignment_2 {
    class Program {
        static void Main(string[] args) {
            Stopwatch stopwatch = new Stopwatch();
            int numPuzzles = 0;
            long[,] totals = new long[4, 2];

            while (Console.ReadLine() != "") {
                numPuzzles++;
                int[,] values = new int[9, 9];
                for (int y = 0; y < 9; y++) {
                    string line = Console.ReadLine();
                    for (int x = 0; x < 9; x++) {
                        values[x, y] = line[x] - '0';
                    }
                }

                stopwatch.Restart();
                Solver cbt = new ChronologicalBacktracker(values);
                stopwatch.Stop();
                Console.WriteLine("CBT:     " + stopwatch.ElapsedMilliseconds + " ms, " + cbt.nodeExpansions + " node expansions.");
                totals[0, 0] += stopwatch.ElapsedMilliseconds;
                totals[0, 1] += cbt.nodeExpansions;

                stopwatch.Restart();
                Solver cbt_mcv = new ChronologicalBacktrackerMCV(values);
                stopwatch.Stop();
                Console.WriteLine("CBT_MCV: " + stopwatch.ElapsedMilliseconds + " ms, " + cbt_mcv.nodeExpansions + " node expansions.");
                totals[1, 0] += stopwatch.ElapsedMilliseconds;
                totals[1, 1] += cbt_mcv.nodeExpansions;

                stopwatch.Restart();
                Solver fc = new ForwardChecker(values);
                stopwatch.Stop();
                Console.WriteLine("FC:      " + stopwatch.ElapsedMilliseconds + " ms, " + fc.nodeExpansions + " node expansions.");
                totals[2, 0] += stopwatch.ElapsedMilliseconds;
                totals[2, 1] += fc.nodeExpansions;

                stopwatch.Restart();
                Solver fc_mcv = new ForwardCheckerMCV(values);
                stopwatch.Stop();
                Console.WriteLine("FC_MCV:  " + stopwatch.ElapsedMilliseconds + " ms, " + fc_mcv.nodeExpansions + " node expansions.");
                totals[3, 0] += stopwatch.ElapsedMilliseconds;
                totals[3, 1] += fc_mcv.nodeExpansions;
            }
            Console.WriteLine("Averages over {0} puzzles:", numPuzzles);
            Console.WriteLine("CBT:     " + (totals[0, 0] / numPuzzles) + " ms, " + (totals[0, 1] / numPuzzles) + " node expansions.");
            Console.WriteLine("CBT_MCV: " + (totals[1, 0] / numPuzzles) + " ms, " + (totals[1, 1] / numPuzzles) + " node expansions.");
            Console.WriteLine("FC:      " + (totals[2, 0] / numPuzzles) + " ms, " + (totals[2, 1] / numPuzzles) + " node expansions.");
            Console.WriteLine("FC_MCV:  " + (totals[3, 0] / numPuzzles) + " ms, " + (totals[3, 1] / numPuzzles) + " node expansions.");
            Console.ReadLine();
        }
    }
}
