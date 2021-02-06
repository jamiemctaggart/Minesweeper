using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public class Square
    {
        public int row { get; set; }
        public int col { get; set; }
        public bool mine { get; set; }
        public bool uncovered { get; set; }
        public bool flagged { get; set; }

        public Button btn { get; set; }



        // Constructor to set square properties
        public Square(int r, int c)
        {
            row = r;
            col = c;
            mine = false;
            uncovered = false;
            flagged = false;
            btn = new Button();
        }

        public void Click()
        {
            Console.WriteLine("You clicked on me " + row + ":" + col);
        }
    }
}
