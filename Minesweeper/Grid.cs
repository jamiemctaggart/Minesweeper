﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// Difficulties
///           Dimention:Mines
/// Easy        :   9x9:10
/// Intermediate: 16x16:40
/// Expert      : 16x30:99
/// </summary>

namespace Minesweeper
{
    public class Grid
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int flagsPlaced { get; set; }
        public int mines { get; set; }
        public bool firstClick { get; set; }
        public Square[,] GameGrid { get; set; }




        // Constructor used to set the lenght and width of the grid then calls the superconstructor
        public Grid(int xParam, int yParam, int minesParam)
        {
            X = xParam;
            Y = yParam;
            flagsPlaced = 0;
            mines = minesParam;
            firstClick = true;
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

        public void populateMines(int xPos, int yPos)
        {
            Random rnd = new Random();
            int minesToPlace = mines;
            while (minesToPlace > 0)
            {
                int xNew = rnd.Next(X);
                int yNew = rnd.Next(Y);
                // Make sure it isnt the first click position
                // and isn't adjacent to the first click
                bool safe = true;
                for (int x = xNew - 1; x <= xNew + 1; x++)
                {
                    if (x < 0 || x >= X) continue;
                    for (int y = yNew - 1; y <= yNew + 1; y++)
                    {
                        if (y < 0 || y >= Y) continue;
                        if (x == xPos && y == yPos) safe = false;
                    }
                }
                if (!safe) continue;
                GameGrid[xNew, yNew].PlaceMine();
                minesToPlace--;
            }
        }

        internal void ClickSquare(int x, int y)
        {
            if (firstClick)
            {
                populateMines(x, y);
                firstClick = false;
            }

            //TODO instead of return make this end the game
            if (GameGrid[x, y].Click()) return;

            int adjacent = CheckAdjacentMines(x, y);
            if (adjacent != 0)
            {
                GameGrid[x, y].btn.Text = adjacent.ToString();
                //TODO add font and number specific colour
            }
            else
            {
                RevealSurroundings(x, y);
            }
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

        public int CheckAdjacentMines(int xPos, int yPos)
        {
            int count = 0;
            for (int x = xPos - 1; x <= xPos + 1; x++)
            {
                if (x < 0 || x >= X) continue;
                for (int y = yPos - 1; y <= yPos + 1; y++)
                {
                    if (y < 0 || y >= Y) continue;
                    if (GameGrid[x, y].mine) count++;
                }
            }
            return count;
        }

        public int CheckAdjacentFlags(int xPos, int yPos)
        {
            int count = 0;
            for (int x = xPos - 1; x <= xPos + 1; x++)
            {
                if (x < 0 || x >= X) continue;
                for (int y = yPos - 1; y <= yPos + 1; y++)
                {
                    if (y < 0 || y >= Y) continue;
                    if (GameGrid[x, y].flagged) count++;
                }
            }
            return count;
        }

        public void RevealSurroundings(int xPos, int yPos)
        {
            Console.WriteLine("Testing1-----------------------");
            if (CheckAdjacentMines(xPos, yPos) != CheckAdjacentFlags(xPos, yPos))
                return;
            Console.WriteLine("Testing2");
            // Checks it isnt flagged or still covered
            if (GameGrid[xPos, yPos].flagged || !GameGrid[xPos, yPos].uncovered) return;

           
            for (int x = xPos - 1; x <= xPos + 1; x++)
            {
                if (x < 0 || x >= X) continue;
                for (int y = yPos - 1; y <= yPos + 1; y++)
                {
                    if (y == yPos && x == xPos) continue;
                    if (y < 0 || y >= Y) continue;
                    if (GameGrid[x, y].flagged) continue;
                    if (GameGrid[x, y].uncovered) continue;
                    ClickSquare(x, y);
                    Console.WriteLine("Testing3");
                }
            }
        }
    }
}
