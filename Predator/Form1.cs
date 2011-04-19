using System.Collections.Generic;
using System.Windows.Forms;
using PredatorCV;

namespace _ExperimentCV
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            var tracks = new List<TrackBar>(){};
            var cap = new Sandbox(capturedImageBox, textBox1, tracks, desktop);
        }
    }
}
