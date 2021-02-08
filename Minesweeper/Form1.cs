using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form1 : Form
    {

        Grid grid;

        // 2D array for Button objects
        public Button[,] btnGrid;

        public Form1()
        {
            InitializeComponent();
            menu();
        }

        private void menu()
        {
            panel1.Controls.Clear();
            panel1.Height = 500;
            panel1.Width = 500;
            this.Size = new Size(200, 200);
            Button easy = new Button();
            easy.MouseUp += EasyClick;
            easy.Text = "Easy";
            easy.Location = new Point(40, 0);
            panel1.Controls.Add(easy);

            Button intermediate = new Button();
            intermediate.MouseUp += IntermediateClick;
            intermediate.Text = "Intermediate";
            intermediate.Location = new Point(40, 30);
            panel1.Controls.Add(intermediate);

            Button expert = new Button();
            expert.MouseUp += ExpertClick;
            expert.Text = "Expert";
            expert.Location = new Point(40, 60);
            panel1.Controls.Add(expert);

        }

        private void populate(int X, int Y, int mine)
        {
            //check if X is 0 as this means to use previous populate list
                        // If you dont need to uset the previous difficulty, then save the current one
            grid = new Grid(X, Y, mine, label1);
            btnGrid = new Button[grid.X,grid.Y];

            //Set panel size
            int btnSize = 24;
            panel1.Height = btnSize * grid.Y;
            panel1.Width = btnSize * grid.X;

            //Set form size
            this.Size = new Size(btnSize * grid.X + 50, btnSize * grid.Y + 100);

            //Print buttons to screen and assign values
            for (int x = 0; x < grid.X; x++)
            {
                for (int y = 0; y < grid.Y; y++)
                {
                    btnGrid[x, y] = new Button();
                    btnGrid[x, y].Height = btnSize;
                    btnGrid[x, y].Width = btnSize;

                    
                    //Click Event
                    //For some unknown reason it needs to mouseup otherwise right click wont work
                    //This took me about half an hour to figure out lmao
                    btnGrid[x, y].MouseUp += new MouseEventHandler(Square_Click);

                                        

                    //Add button to panel
                    panel1.Controls.Add(btnGrid[x, y]);

                    btnGrid[x, y].Location = new Point(x * btnSize, y * btnSize);
                    //TODO remove this text soon
                    //btnGrid[x, y].Text = x + ":" + y;
                    btnGrid[x, y].Tag = new Point(x, y);

                    //Format button nicely
                    btnGrid[x, y].TabStop = false;
                    btnGrid[x, y].FlatStyle = FlatStyle.Flat;
                    btnGrid[x, y].FlatAppearance.BorderSize = 0;

                    //TODO add image to button
                    btnGrid[x, y].Image = Image.FromFile(
                        Path.Combine(
                            Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                            "Photos\\Square.png")
                        );
                }

            }
            UpdateMenu();
            //After creating all the buttons call the grid functiont to merge them with each square object
            grid.Merge(btnGrid);
        }

        // This will click the square, it also removes the mouse event
        // and replaces it with a new one for revealing all adjacent squares
        private void Square_Click(object sender, MouseEventArgs e)
        {
            //casts as a Button
            Button btn = (Button)sender;
                            //Casts as a point as it will always be a point
            Point point = (Point)btn.Tag;

            // Extract x and y values from point object
            int x = point.X;
            int y = point.Y;

            
            // Either places flag if right click or reveals if left click
            switch(e.Button)
            {
                case MouseButtons.Left:
                    grid.ClickSquare(x, y);

                    // it is important to remove the handler to uncover a square 
                    // after it has been left clicked, this allows us to make
                    // a new handler for revealing all adjacent squares
                    btnGrid[x, y].MouseUp -= new MouseEventHandler(Square_Click);

                    // Adds new handler for reveal all function
                    btnGrid[x, y].MouseClick += Form1_MouseClick;

                    break;
                case MouseButtons.Right:
                    grid.FlagSquare(x, y);
                    break;
            }
            UpdateMenu();
        }

        // This is specifically for revealing nearby squares
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Test");
            Button btn = (Button)sender;
            //Casts as a point as it will always be a point
            Point point = (Point)btn.Tag;

            int x = point.X;
            int y = point.Y;

            grid.RevealSurroundings(x, y);

            UpdateMenu();
        }

        private void UpdateMenu()
        {
            label2.Text = (grid.mines-grid.flagsPlaced).ToString();
        }

        
        private void EasyClick(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            populate(9, 9, 10);
        }

        private void IntermediateClick(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            populate(16, 16, 40);
        }

        private void ExpertClick(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            populate(30, 16, 99);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menu();
        }
    }
}
