using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public class Grid
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        
        public Square[,] GameGrid { get; set; }




        // Constructor used to set the lenght and width of the grid then calls the superconstructor
        public Grid(int rows, int columns)
        {
            Columns = rows;
            Rows = columns;
            Console.WriteLine("It made it to the base constructor");
            GameGrid = new Square[Rows, Columns];

            // Fills array with Squares
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    GameGrid[r, c] = new Square(r, c);
                }
            }

        }

        internal void ClickSquare(int r, int c)
        {
            GameGrid[r, c].Click();
        }

        //This adds the button object to each square
        internal void Merge(Button[,] btnGrid)
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    GameGrid[r, c].btn = btnGrid[r, c];
                }
            }
        }
    }
}
