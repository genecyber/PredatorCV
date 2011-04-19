using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using PredatorCV.Converters;
using PredatorCV.Detectors;
using PredatorCV.Extensions;
using PredatorCV.Extensions.Converters;
using PredatorCV.Sources;

namespace PredatorCV
{
    public class Sandbox
    {
        private readonly IVideo _capture;
        private readonly IVideo _desktopCapture;
        private readonly ImageBox _imageBox;
        private static TextBox _textBox;
        private static List<Eigen> _eigenDetectors;
        private static PictureBox _desktop;
        const string cannyDirectory = "training\\canny";
        const string directory = "training";
        private static List<TrackBar> _tracks;
        

        public Sandbox(ImageBox imageBox, TextBox textBox, List<TrackBar> tracks, PictureBox desktop)
        {
            _imageBox = imageBox;
            _textBox = textBox;
            _tracks = tracks;
            _desktop = desktop;
            cannyDirectory.CleanImageFileNames();
            
            directory.CleanImageFileNames();
            _eigenDetectors = new List<Eigen>
                          {
                              new Eigen(cannyDirectory,18),
                              new Eigen(directory,5)
                          };
            _capture = VideoFactory.GetCaptureDevice(VideoFactory.VideoSource.Camera, ProcessCameraFrame).Value;
            _desktopCapture = VideoFactory.GetCaptureDevice(VideoFactory.VideoSource.Desktop, ProcessDesktopFrame).Value;
        }
        private void ProcessCameraFrame(object sender, EventArgs e)
        {
            using (Image<Bgr, Byte> image = _capture.QueryFrame())
            {
                IImage editedImage = image.Copy();
                _imageBox.Image = editedImage;
            }
        }
        private void ProcessDesktopFrame(object sender, EventArgs e)
        {
            using (var image = _desktopCapture.QueryFrame())
            {
                IImage editedImage = image.Copy();
                _desktop.Image = editedImage.Bitmap;
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
    }
}
