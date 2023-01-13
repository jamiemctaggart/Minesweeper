using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Minesweeper
{
    public class Grid
    {
        public int x { get; set; }
        public int y { get; set; }
        public int flagsPlaced { get; set; }
        public int mines { get; set; }
        private int minesLeft { get; set; }
        private bool firstClick { get; set; }
        private bool gameOver { get; set; }
        private Square[,] gameGrid { get; set; }
        private Label time { get; set; }
        private Timer timer { get; set; }
        private Stopwatch stopwatch { get; set; }




        // Constructor used to set the lenght and width of the grid then calls the superconstructor
        public Grid(int xParam, int yParam, int minesParam, Label timeParam)
        {
            x = xParam;
            y = yParam;
            flagsPlaced = 0;
            mines = minesParam;
            minesLeft = minesParam;
            firstClick = true;
            gameOver = false;
            gameGrid = new Square[x, y];
            time = timeParam;
            PopulateGrid();
            time.Text = @"0";
        }

        private void PopulateGrid()
        {
            // Fills array with Squares
            for (int x = 0; x < this.x; x++)
            {
                for (int y = 0; y < this.y; y++)
                {
                    gameGrid[x, y] = new Square(x, y);
                }
            }
        }

        private void PopulateMines(int xPos, int yPos)
        {
            Random rnd = new Random();
            int minesToPlace = mines;
            while (minesToPlace > 0)
            {
                int xNew = rnd.Next(x);
                int yNew = rnd.Next(y);
                // Make sure it isnt the first click position
                // and isn't adjacent to the first click
                bool safe = true;
                for (int x = xNew - 1; x <= xNew + 1; x++)
                {
                    if (x < 0 || x >= this.x) continue;
                    for (int y = yNew - 1; y <= yNew + 1; y++)
                    {
                        if (y < 0 || y >= this.y) continue;
                        if (x == xPos && y == yPos) safe = false;
                    }
                }
                if (!safe) continue;
                if (gameGrid[xNew, yNew].PlaceMine())
                    minesToPlace--;
            }
        }

        internal void ClickSquare(int x, int y)
        {
            if (gameOver) return;

            
            if (firstClick)
            {
                PopulateMines(x, y);
                firstClick = false;
                StartTimer();
            }

            //Make sure its not flagged
            if (gameGrid[x, y].flagged) return;
            
            // If mine lose the game
            if (gameGrid[x, y].Click())
            {
                gameOver = true;
                StopTimer();
                MessageBox.Show("You Lost!");
                return;
            }

                        
            int adjacent = CheckAdjacentMines(x, y);
            if (adjacent != 0)
            {
                gameGrid[x, y].btn.Text = adjacent.ToString();

                //This is to change the colour depending on number
                switch (adjacent)
                {
                    case 1:
                        gameGrid[x, y].btn.ForeColor = Color.Blue;
                        break;
                    case 2:
                        gameGrid[x, y].btn.ForeColor = Color.Green;
                        break;
                    case 3:
                        gameGrid[x, y].btn.ForeColor = Color.Red;
                        break;
                    case 4:
                        gameGrid[x, y].btn.ForeColor = Color.Purple;
                        break;
                    case 5:
                        gameGrid[x, y].btn.ForeColor = Color.Maroon;
                        break;
                    case 6:
                        gameGrid[x, y].btn.ForeColor = Color.Turquoise;
                        break;
                    case 7:
                        gameGrid[x, y].btn.ForeColor = Color.Black;
                        break;
                    case 8:
                        gameGrid[x, y].btn.ForeColor = Color.Gray;
                        break;
                }
                //TODO add font and number specific colour
            }
            else
            {
                RevealSurroundings(x, y);
            }
        }

        public void StartTimer() { 
            Console.WriteLine(@"Start timer");
            timer = new Timer();
            timer.Interval = (1000);
            //Add event every second
            timer.Tick += Timer_Tick;
            stopwatch = new Stopwatch();
            timer.Start();
            stopwatch.Start();
        }


        public void StopTimer()
        {
            timer.Stop();
            stopwatch.Stop();
            time.Text = stopwatch.Elapsed.Seconds.ToString();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            time.Text = (stopwatch.Elapsed.Seconds + stopwatch.Elapsed.Minutes * 60).ToString();
        }

        internal void FlagSquare(int x, int y)
        {
            if (gameOver) return;

            int change = gameGrid[x, y].Flag();
            flagsPlaced += change;

            if (gameGrid[x,y].mine)
            {
                // This looks more confusing than it is, all it does is if you flag a location that has a mine,
                // take away one mine, but RE ADD if you remove the flag, when the mines variable reaches 
                // zero you have won the game and flagged all the mines.
                minesLeft -= change;
                if (minesLeft == 0)
                {
                    WinGame();
                    return;
                }
            }
        }

        private void WinGame()
        {
            StopTimer();
            MessageBox.Show("You Won in " + time.Text + "!");
            // Todo: fix the time being wrong
            int score = int.Parse(time.Text);
            gameOver = true;
            int highscoreI = 0;
            int[] highscores = Save.Load();
            /*mines switch
            {
                10 => highscoreI = 0,
                40 => highscoreI = 1
                99 => highscoreI = 2,
            };*/
            switch(mines)
            {
                case 10:
                    highscoreI = 0;
                    break;
                case 40:
                    highscoreI = 1;
                    break;
                case 99:
                    highscoreI = 2;
                    break;
            }
            if (highscores[highscoreI] < score || highscores[highscoreI] == 0)
            {
                highscores[highscoreI] = score;
                Save.SaveScore(highscores);
                MessageBox.Show($"You won in {score} seconds!\nThis is a new high score");
            }
            else
            {
                MessageBox.Show($"You won in {score} seconds!\n");
            }
            Save.SaveScore(highscores);
        }

        //This adds the button object to each square
        internal void Merge(Button[,] btnGrid)
        {
            for (int x = 0; x < this.x; x++)
            {
                for (int y = 0; y < this.y; y++)
                {
                    gameGrid[x, y].btn = btnGrid[x, y];
                }
            }
        }

        private int CheckAdjacentMines(int xPos, int yPos)
        {
            int count = 0;
            for (int x = xPos - 1; x <= xPos + 1; x++)
            {
                if (x < 0 || x >= this.x) continue;
                for (int y = yPos - 1; y <= yPos + 1; y++)
                {
                    if (y < 0 || y >= this.y) continue;
                    if (gameGrid[x, y].mine) count++;
                }
            }
            return count;
        }

        private int CheckAdjacentFlags(int xPos, int yPos)
        {
            int count = 0;
            for (int x = xPos - 1; x <= xPos + 1; x++)
            {
                if (x < 0 || x >= this.x) continue;
                for (int y = yPos - 1; y <= yPos + 1; y++)
                {
                    if (y < 0 || y >= this.y) continue;
                    if (gameGrid[x, y].flagged) count++;
                }
            }
            return count;
        }

        public void RevealSurroundings(int xPos, int yPos)
        {
            if (gameOver) return;
            if (CheckAdjacentMines(xPos, yPos) != CheckAdjacentFlags(xPos, yPos))
                return;
            // Checks it isnt flagged or still covered
            if (gameGrid[xPos, yPos].flagged || !gameGrid[xPos, yPos].uncovered) return;

           
            for (int x = xPos - 1; x <= xPos + 1; x++)
            {
                if (x < 0 || x >= this.x) continue;
                for (int y = yPos - 1; y <= yPos + 1; y++)
                {
                    if (y == yPos && x == xPos) continue;
                    if (y < 0 || y >= this.y) continue;
                    if (gameGrid[x, y].flagged) continue;
                    if (gameGrid[x, y].uncovered) continue;
                    ClickSquare(x, y);
                }
            }
        }
    }
}
