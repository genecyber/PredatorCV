using System.Collections.Generic;
using System.Windows.Forms;
using PredatorCV.Extensions;

namespace PredatorCV
{
    public partial class Form1 : Form
    {
        private readonly Sandbox _sandbox;

        public Form1()
        {
            InitializeComponent();
            var tracks = new List<TrackBar>();
            _sandbox = new Sandbox(capturedImageBox, "Image", "Surf", textBox1);
        }


        private void SourceSelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (detector.SelectedItem != null)
                _sandbox.AssignCaptureSource(sender.As<ComboBox>().SelectedItem.ToString(), detector.SelectedItem.ToString());
            else
                _sandbox.AssignCaptureSource(sender.As<ComboBox>().SelectedItem.ToString(), "");
        }

        private void detector_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (detector.SelectedItem != null)
                _sandbox.AssignDetector(sender.As<ComboBox>().SelectedItem.ToString());
            else
                _sandbox.AssignDetector("");
        }

        


    }
}
