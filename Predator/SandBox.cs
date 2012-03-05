using System;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using PredatorCV.Detectors;
using PredatorCV.Sources;

namespace PredatorCV
{
    public class Sandbox
    {
        private ISource _capture;
        private IDetector _detector;
        private readonly ImageBox _imageBox;
        private TextBox _textBox;


        public Sandbox(ImageBox imageBox, String source, String detector, TextBox textBox)
        {
            _imageBox = imageBox;
            _textBox = textBox;

            AssignCaptureSource(source, detector);
        }

        public void AssignCaptureSource(string source, string detector)
        {
            _detector = DetectorFactory.GetDetector(detector).Value;
            _capture = SourceFactory.GetCaptureDevice(source, FrameProcessor).Value;
        }

        public void AssignDetector(string detector)
        {
            _detector = DetectorFactory.GetDetector(detector).Value;
        }

        private void FrameProcessor(object sender, EventArgs e)
        {
            DetectorResult imageObject = _detector.Process(_capture.QueryFrame(),
                                                           _capture.QueryFrame().Convert<Gray, Byte>());

            IImage editedImage = imageObject.ProcessedImage;
            _imageBox.Image = editedImage;
            if (imageObject.Label != null)
                _textBox.Text = imageObject.Label;

        }
    }
}
