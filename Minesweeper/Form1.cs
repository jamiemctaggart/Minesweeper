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
        public Button[,] btnGrid = new Button[grid.Rows,grid.Columns];



        public Form1()
        {
            InitializeComponent();
            populate();
        }

        private void populate()
        {
            int btnSize = 36;
            panel1.Height = btnSize * grid.Columns;
            panel1.Width = btnSize * grid.Rows;

            //Print buttons to screen and assign values
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    btnGrid[r, c] = new Button();
                    btnGrid[r, c].Height = btnSize;
                    btnGrid[r, c].Width = btnSize;
                    //Click Event
                    btnGrid[r, c].Click += Square_Click;

                    panel1.Controls.Add(btnGrid[r, c]);

                    btnGrid[r, c].Location = new Point(r * btnSize, c * btnSize);
                    //TODO remove this text soon
                    btnGrid[r, c].Text = r + ":" + c;
                }

            }
        }

        private void Square_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
