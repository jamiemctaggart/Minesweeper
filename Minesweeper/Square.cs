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
        public int X { get; set; }
        public int Y { get; set; }
        public bool mine { get; set; }
        public bool uncovered { get; set; }
        public bool flagged { get; set; }

        public Button btn { get; set; }



        // Constructor to set square properties
        public Square(int x, int y)
        {
            X = x;
            Y = y;
            mine = false;
            uncovered = false;
            flagged = false;
            btn = new Button();
        }

        public void Click()
        {
            Console.WriteLine("You clicked on me " + X + ":" + Y);
        }
    }
}
