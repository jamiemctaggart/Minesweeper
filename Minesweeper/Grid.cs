﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public class Grid
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int flagsPlaced { get; set; }
        
        public Square[,] GameGrid { get; set; }




        // Constructor used to set the lenght and width of the grid then calls the superconstructor
        public Grid(int xParam, int yParam)
        {
            X = xParam;
            Y = yParam;
            flagsPlaced = 0;
            GameGrid = new Square[X, Y];
            populateGrid();
           

        }

        public void populateGrid()
        {
            // Fills array with Squares
            for (int x = 0; x < X; x++)
            {
                for (int y = 0; y < Y; y++)
                {
                    GameGrid[x, y] = new Square(x, y);
                }
            }
        }

        internal void ClickSquare(int x, int y)
        {
            GameGrid[x, y].Click();
        }

        internal void FlagSquare(int x, int y)
        {
            flagsPlaced += GameGrid[x, y].Flag();
        }

        //This adds the button object to each square
        internal void Merge(Button[,] btnGrid)
        {
            for (int x = 0; x < X; x++)
            {
                for (int y = 0; y < Y; y++)
                {
                    GameGrid[x, y].btn = btnGrid[x, y];
                }
            }
        }
    }
}
