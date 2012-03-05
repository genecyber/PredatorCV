using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using tessnet2;

namespace PredatorCV.Detectors
{
    public class Text : IDetector
    {
        private static void InitOcr()
        {
            //create OCR engine
            _ocr = new Tesseract();

            //You can download more language definition data from
            //http://code.google.com/p/tesseract-ocr/downloads/list
            //Languages supported includes:
            //Dutch, Spanish, German, Italian, French and English
            _ocr.Init(null, "eng", false);
        }
        private static Tesseract _ocr;
        public static void CreateAndDisplayText(Image<Bgr, byte> image, TextBox textBox)
        {
            InitOcr();
            Image<Gray, byte> gray = image.Convert<Gray, Byte>();
            Image<Gray, Byte> thresh = gray.ThresholdBinaryInv(new Gray(120), new Gray(255));
            var licenses = new List<List<Word>>();
            using (Image<Gray, Byte> plateMask = new Image<Gray, byte>(gray.Size))
            using (Image<Gray, Byte> plateCanny = gray.Canny(new Gray(100), new Gray(50)))
            using (MemStorage stor = new MemStorage())
            {
                plateMask.SetValue(255.0);
                for (
                   Contour<Point> contours = plateCanny.FindContours(
                      Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE,
                      Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_EXTERNAL,
                      stor);
                   contours != null;
                   contours = contours.HNext)
                {
                    Rectangle rect = contours.BoundingRectangle;
                    if (rect.Height > (gray.Height >> 1))
                    {
                        rect.X -= 1; rect.Y -= 1; rect.Width += 2; rect.Height += 2;
                        rect.Intersect(gray.ROI);

                        plateMask.Draw(rect, new Gray(0.0), -1);
                        image.Draw(rect, new Bgr(Color.Red), -1);

                        List<Word> words;
                        using (Bitmap bmp = plateCanny.Bitmap)
                            words = _ocr.DoOCR(bmp, plateCanny.ROI);
                        
                        licenses.Add(words);
                    }
                }

                thresh.SetValue(0, plateMask);
            }
            foreach (var license in licenses)
            {
                foreach (var foo in license)
                {
                    textBox.Text += foo;
                }
                
            }
            thresh._Erode(1);
            thresh._Dilate(1);


            
        }

        public DetectorResult Process(Image<Bgr, byte> rawFrame, Image<Gray, byte> grayFrame)
        {
            InitOcr();
            List<Word> words;
            using (Bitmap bmp = grayFrame.Bitmap)
                words = _ocr.DoOCR(bmp,new Rectangle(0,0,320,240));
            String label = words.Aggregate("", (current, w) => current + w.ToString());
            return new DetectorResult() { Label = label, ProcessedImage = rawFrame };
        }
    }
}
