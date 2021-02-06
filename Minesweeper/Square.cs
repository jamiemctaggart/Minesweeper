using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        //Returns true if you hit a mine
        public bool Click()
        {
            Console.WriteLine("You clicked on me " + X + ":" + Y);
            if (uncovered || flagged) return false;
            uncovered = true;
            if (mine)
            {
                btn.Image = Image.FromFile(Path.Combine(
                    Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                    "Photos\\Mine.png"));
                return true;
            }
            else
            {
                btn.Image = Image.FromFile(Path.Combine(
                    Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                    "Photos\\Empty.png"));
                return false;
            }
        }

        // returns the change in flags place, if removed -1, if added +1 if unchanged 0
        public int Flag()
        {
            // If already uncovered return 0 and change nothing
            if (uncovered) return 0;

            // If it is flagged, remove flag and return -1
            if (flagged)
            {
                flagged = false;
                btn.Image = Image.FromFile(Path.Combine(
                    Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                    "Photos\\Square.png"));
                return -1;
            }

            // Otherwise place flag and return 1 
            else
            {
                flagged = true;
                btn.Image = Image.FromFile(Path.Combine(
                    Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                    "Photos\\Flag.png"));
                return 1;
            }
        }

        // Places a mine 
        // If there is already a mine there it returns flase to avoid duplicates!
        public bool PlaceMine()
        {
            if (mine) return false;
            mine = true;
            return true;
        }
    }
}
