using System;
using System.Resources;

namespace Minesweeper
{
    class Save
    {
        public static int[] Load()
        {
            int[] highscores = new int[] { 0, 0, 0 };
            ResXResourceSet resxSet = new ResXResourceSet(@".\Resources.resx");
            highscores[0] = Int32.Parse(Properties.Settings.Default.Easy);
            highscores[1] = Int32.Parse(Properties.Settings.Default.Intermediate);
            highscores[2] = Int32.Parse(Properties.Settings.Default.Expert);

            return highscores;
        }

        public static void SaveScore(int[] highscores)
        {
            Properties.Settings.Default.Easy = highscores[0].ToString();
            Properties.Settings.Default.Intermediate = highscores[1].ToString();
            Properties.Settings.Default.Expert = highscores[2].ToString();
            Properties.Settings.Default.Save();
        }
    }
}
