using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 

namespace Minesweeper
{
    public partial class Form1 : Form
    {

        Grid grid = new Grid(20);

        public Form1()
        {
            InitializeComponent();
        }
    }
}
