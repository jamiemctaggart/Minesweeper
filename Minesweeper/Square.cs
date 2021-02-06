using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Square
    {
        public int row { get; set; }
        public int col { get; set; }
        public bool mine { get; set; }
        public bool uncovered { get; set; }
        public bool flagged { get; set; }


        // Constructor to set square properties
        public Square(int r, int c)
        {
            row = r;
            col = c;
            mine = false;
            uncovered = false;
            flagged = false;
        }
    }
}
