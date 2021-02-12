using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    class fileIO
    {
        public static int[] readSave()
        {
            int[] highscores = new int[] { 0, 0, 0 };
            ResXResourceSet resxSet = new ResXResourceSet(@".\Resources.resx");
            highscores[0] = Int32.Parse(Properties.Settings.Default.Easy);
            highscores[1] = Int32.Parse(Properties.Settings.Default.Intermediate);
            highscores[2] = Int32.Parse(Properties.Settings.Default.Expert);

            return highscores;
        }

        public static void setSave(int[] highscores)
        {
            Properties.Settings.Default.Easy = highscores[0].ToString();
            Properties.Settings.Default.Intermediate = highscores[1].ToString();
            Properties.Settings.Default.Expert = highscores[2].ToString();
            Properties.Settings.Default.Save();
        }
    }
}
