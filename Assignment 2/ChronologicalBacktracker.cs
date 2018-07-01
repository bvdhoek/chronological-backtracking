using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    class ChronologicalBacktracker : Solver
    {
        public ChronologicalBacktracker(int[,] values) : base(values) { }

        public override bool Solve(Cell cell)
        {
            if (cell == null) return true;
            if (cell.value != 0) return Solve(NextCell(cell));

            for (int value = 1; value <= 9; value++)
            {
                if (!SatisfiesConstraints(cell, value)) continue;

                cell.value = value;
                if (Solve(NextCell(cell)))
                {
                    return true;
                } else
                {
                    cell.value = 0;
                }
            }

            return false;
        }

        public override Cell NextCell(Cell cell)
        {
            int x = cell.x;
            int y = cell.y;
            
            if (cell.x == 8)
            {
                x = 0;
                y++;
            } else
            {
                x++;
            }

            if (y > 8) return null;
            return cells[x, y];
        }

        public override bool SatisfiesConstraints(Cell cell, int value)
        {
            for (int i = 0; i < 9; i++)
            {
                if (cells[i, cell.y].value == value || cells[cell.x, i].value == value) return false;
            }

            int x1 = 3 * (cell.x / 3);
            int y1 = 3 * (cell.y / 3);
            int x2 = x1 + 2;
            int y2 = y1 + 2;

            for (int x = x1; x <= x2; x++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    if (cells[x, y].value == value) return false;
                }
            }
            return true;
        }
    }
}
