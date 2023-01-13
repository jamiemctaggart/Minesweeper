using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public class Square
    {
        public int x { get; set; }
        public int y { get; set; }
        public bool mine { get; set; }
        public bool uncovered { get; set; }
        public bool flagged { get; set; }

        public Button btn { get; set; }


        // Constructor to set square properties
        public Square(int x, int y)
        {
            this.x = x;
            this.y = y;
            mine = false;
            uncovered = false;
            flagged = false;
            btn = new Button();
        }

        //Returns true if you hit a mine
        public bool Click()
        {
            Console.WriteLine("You clicked on me " + x + ":" + y);
            if (uncovered || flagged) return false;
            uncovered = true;
            if (mine)
            {
                btn.Image = (Image) Properties.Resources.Mine;
                return true;
            }
            else
            {
                btn.Image = (Image) Properties.Resources.Empty;
                btn.DoubleClick += Btn_DoubleClick;
                return false;
            }
        }

        private void Btn_DoubleClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
                btn.Image = (Image) Properties.Resources.Square;
                return -1;
            }

            // Otherwise place flag and return 1 
            else
            {
                flagged = true;
                btn.Image = (Image) Properties.Resources.Flag;
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
