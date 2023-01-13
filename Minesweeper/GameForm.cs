using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class GameForm : Form
    {

        private Grid _grid;

        // 2D array for Button objects
        private Button[,] _btnGrid;

        public GameForm()
        {
            InitializeComponent();
             Save.Load();
            MenuUi();
        }

        private void MenuUi()
        {
            panel1.Controls.Clear();
            panel1.Height = 500;
            panel1.Width = 500;
            this.Size = new Size(260, 200);
            Button easy = new Button();
            easy.MouseUp += EasyClick;
            easy.Text = @"Easy";
            easy.Location = new Point(70, 0);
            panel1.Controls.Add(easy);

            Button intermediate = new Button();
            intermediate.MouseUp += IntermediateClick;
            intermediate.Text = @"Intermediate";
            intermediate.Location = new Point(70, 30);
            panel1.Controls.Add(intermediate);

            Button expert = new Button();
            expert.MouseUp += ExpertClick;
            expert.Text = @"Expert";
            expert.Location = new Point(70, 60);
            panel1.Controls.Add(expert);

        }

        private void Populate(int xWidth, int yHeight, int mine)
        {
            //check if xWidth is 0 as this means to use previous Populate list
                        // If you dont need to uset the previous difficulty, then save the current one
            _grid = new Grid(xWidth, yHeight, mine, label1);
            _btnGrid = new Button[_grid.x,_grid.y];

            //Set panel size
            int btnSize = 24;
            panel1.Height = btnSize * _grid.y;
            panel1.Width = btnSize * _grid.x;

            //Set form size
            this.Size = new Size(btnSize * _grid.x + 50, btnSize * _grid.y + 100);

            //Print buttons to screen and assign values
            for (int x = 0; x < _grid.x; x++)
            {
                for (int y = 0; y < _grid.y; y++)
                {
                    _btnGrid[x, y] = new Button();
                    _btnGrid[x, y].Height = btnSize;
                    _btnGrid[x, y].Width = btnSize;

                    
                    //Click Event
                    //For some unknown reason it needs to mouseup otherwise right click wont work
                    //This took me about half an hour to figure out lmao
                    _btnGrid[x, y].MouseUp += Square_Click;

                                        

                    //Add button to panel
                    panel1.Controls.Add(_btnGrid[x, y]);

                    _btnGrid[x, y].Location = new Point(x * btnSize, y * btnSize);
                    //TODO remove this text soon
                    //_btnGrid[x, y].Text = x + ":" + y;
                    _btnGrid[x, y].Tag = new Point(x, y);

                    //Format button nicely
                    _btnGrid[x, y].TabStop = false;
                    _btnGrid[x, y].FlatStyle = FlatStyle.Flat;
                    _btnGrid[x, y].FlatAppearance.BorderSize = 0;
                    _btnGrid[x, y].Image = Properties.Resources.Square;
                }

            }
            UpdateMenu();
            //After creating all the buttons call the _grid functiont to merge them with each square object
            _grid.Merge(_btnGrid);
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
                    _grid.ClickSquare(x, y);

                    // it is important to remove the handler to uncover a square 
                    // after it has been left clicked, this allows us to make
                    // a new handler for revealing all adjacent squares
                    _btnGrid[x, y].MouseUp -= Square_Click;

                    // Adds new handler for reveal all function
                    _btnGrid[x, y].MouseClick += Form1_MouseClick;

                    break;
                case MouseButtons.Right:
                    _grid.FlagSquare(x, y);
                    break;
            }
            UpdateMenu();
        }

        // This is specifically for revealing nearby squares
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine(@"Test");
            Button btn = (Button)sender;
            //Casts as a point as it will always be a point
            Point point = (Point)btn.Tag;

            int x = point.X;
            int y = point.Y;

            _grid.RevealSurroundings(x, y);

            UpdateMenu();
        }

        private void UpdateMenu()
        {
            label2.Text = (_grid.mines-_grid.flagsPlaced).ToString();
        }

        
        private void EasyClick(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Populate(9, 9, 10);
        }

        private void IntermediateClick(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Populate(16, 16, 40);
        }

        private void ExpertClick(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Populate(30, 16, 99);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuUi();
        }

        private void rulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Rules:" +
                "\n1. Click on a square" +
                "\n2. The number on a square is the number of adjacent mines (Blank = 0)" +
                "\n3. Right click a square if you know it is a mine" +
                "\n4. When you have cleared all the mines you have won the game!" +
                "\n\nOptional:" +
                "\n1. double clicking on an uncovered square will reveal all adjacent squares");
        }

        private void highscoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] highscores = Save.Load();
            MessageBox.Show(
                $@"Highscores:
Easy: {highscores[0]}
Intermediate: {highscores[1]}
Expert: {highscores[2]}");
        }
    }
}
