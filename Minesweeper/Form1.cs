using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form1 : Form
    {

        static Grid grid = new Grid(16, 16);

        // 2D array for Button objects
        public Button[,] btnGrid = new Button[grid.X,grid.Y];



        public Form1()
        {
            InitializeComponent();
            populate();
        }

        private void populate()
        {
            int btnSize = 24;
            panel1.Height = btnSize * grid.Y;
            panel1.Width = btnSize * grid.X;

            //Print buttons to screen and assign values
            for (int x = 0; x < grid.X; x++)
            {
                for (int y = 0; y < grid.Y; y++)
                {
                    btnGrid[x, y] = new Button();
                    btnGrid[x, y].Height = btnSize;
                    btnGrid[x, y].Width = btnSize;
                    //Click Event
                    btnGrid[x, y].Click += Square_Click;

                    panel1.Controls.Add(btnGrid[x, y]);

                    btnGrid[x, y].Location = new Point(x * btnSize, y * btnSize);
                    //TODO remove this text soon
                    //btnGrid[x, y].Text = x + ":" + y;
                    btnGrid[x, y].Tag = new Point(x, y);
                }

            }

            //After creating all the buttons call the grid functiont to merge them with each square object
            grid.Merge(btnGrid);
        }

        private void Square_Click(object sender, EventArgs e)
        {
            //casts as a Button as it will always be a button object
            Button btn = (Button)sender;
            //Casts as a point as it will always be a point
            Point point = (Point)btn.Tag;

            int x = point.X;
            int y = point.Y;

            grid.ClickSquare(x, y);
        }
    }
}
