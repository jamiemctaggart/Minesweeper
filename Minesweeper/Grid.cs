using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Grid
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        
        public Square[,] GameGrid { get; set; }



        // Constructor to make the grid out of squares
        public Grid()
        {
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

        //TODO check to make sure the base() works properly at some point
        // Constructor used to set the lenght and width of the grid then calls the superconstructor
        public Grid(int length, int width) : base()
        {
            Columns = length;
            Rows = width;
        }
    }
}
