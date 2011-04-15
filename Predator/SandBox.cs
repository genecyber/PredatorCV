using System;
using System.Collections.Generic;
using System.Windows.Forms;
using _ExperimentCV.Extensions;
using _ExperimentCV.Converters;
using _ExperimentCV.Detectors;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;


namespace _ExperimentCV
{
    public class Sandbox
    {
        private readonly Capture _capture;
        private readonly ImageBox _imageBox;
        private static TextBox _textBox;
        private static List<Eigen> _eigenDetectors;
        const string cannyDirectory = "training\\canny";
        const string directory = "training";
        private static List<TrackBar> _tracks;

        public Sandbox(ImageBox imageBox, TextBox textBox, List<TrackBar> tracks)
        {
            _imageBox = imageBox;
            _textBox = textBox;
            _tracks = tracks;
            cannyDirectory.CleanImageFileNames();
            
            directory.CleanImageFileNames();
            _eigenDetectors = new List<Eigen>
                          {
                              new Eigen(cannyDirectory,18),
                              new Eigen(directory,5)
                          };

            if (_capture == null)
            {
                try
                {
                    _capture = new Capture();
                    Application.Idle += ProcessFrame;

                }
                catch (NullReferenceException excpt)
                {   
                    MessageBox.Show(excpt.Message);
                }
            }
        }
        private void ProcessFrame(object sender, EventArgs e)
        {
            using (Image<Bgr, Byte> image = _capture.QueryFrame())
            {
                IImage editedImage = image.Copy();
                editedImage = FindEigens(image);
                ((Image<Bgr, byte>) editedImage)._Flip(FLIP.HORIZONTAL);
                //editedImage = FindCircles(((Image<Bgr, byte>)editedImage));
                _imageBox.Image = editedImage;
            }
        }

        private static IImage FindEigens(Image<Bgr, byte> image)
        {
            var result1 = _eigenDetectors[0].Process(image, image.ToScaledSmoothedCanny());
            var result2 = _eigenDetectors[1].Process(image, image.ToGray());
            if (result2.Label == result1.Label && result2.Label != "")
            {
                _textBox.Text = result1.Label;
                image.ToScaledSmoothedCanny().SaveImageAtIndex(result1.Label,
                                                             cannyDirectory.TotalImagesWithLabel(result1.Label),
                                                             cannyDirectory);
                image.SaveImageAtIndex(result1.Label, directory.TotalImagesWithLabel(result1.Label),
                                                      directory);
            }
            else
                _textBox.Text = "";
            return image;
        }

        private static IImage FindCircles(Image<Bgr, byte> image)
        {
            var detector = new Circle();
            return detector.Process(image, image.ToGray()).ProcessedImage;
        }
    }
}
