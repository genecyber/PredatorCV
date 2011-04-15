using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace _ExperimentCV.Detectors
{
    public class Contour : IDetector
    {

        public DetectorResult Process(Image<Bgr, byte> rawFrame, Image<Gray, byte> grayFrame)
        {
            Image<Bgr, byte> contourImage = null;
            if (rawFrame != null)
            {
                List<Point[]> polygon = new List<Point[]>();      // to draw the perimeter

                Image<Gray, byte> gray = rawFrame.Convert<Gray, byte>();               // convert source to gray
                Image<Gray, byte> thresh = gray.PyrDown().PyrUp();                  // attempt to make edges more distinct?

                using (Image<Gray, Byte> mask = new Image<Gray, byte>(thresh.Size))
                using (Image<Gray, byte> cannyImg = thresh.Canny(new Gray(10), new Gray(50)))
                using (Image<Gray, byte> dilateImg = cannyImg.Dilate(1))

                using (MemStorage stor = new MemStorage())
                {
                    mask.SetValue(255.0);
                    for (
                       Contour<Point> contours = dilateImg.FindContours(
                          Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE,
                          Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_EXTERNAL,
                          stor);
                       contours != null; contours = contours.HNext)
                    {
                        Rectangle rect = contours.BoundingRectangle;
                        int area = rect.Height * rect.Width;
                        if (area > 30000)
                        {
                            rect.X -= 1; rect.Y -= 1; rect.Width += 2; rect.Height += 2;
                            rect.Intersect(gray.ROI);
                            mask.Draw(rect, new Gray(0.0), -1);

                            polygon.Add(contours.ToArray());
                        }
                    }

                    thresh.SetValue(0, mask);
                }

                contourImage = new Image<Bgr, byte>(gray.Bitmap);
                contourImage.CopyBlank();

                foreach (Point[] points in polygon)
                    contourImage.DrawPolyline(points, true, new Bgr(Color.Red), 2);
            }
            var result = new DetectorResult()
                             {
                                 Confidence = 100,
                                 GrayImage = grayFrame,
                                 ProcessedImage = contourImage,
                                 RawImage = rawFrame
                             };
            return result;
        }
    }
}
