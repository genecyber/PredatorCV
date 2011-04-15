using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.VideoSurveillance;

namespace _ExperimentCV
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            var tracks = new List<TrackBar>(){trackBar1, trackBar2, trackBar3, trackBar4, trackBar5, trackBar6, trackBar7};
            var cap = new Sandbox(capturedImageBox, textBox1, tracks);
        }
    }
}
