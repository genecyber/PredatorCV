using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;

namespace PredatorCV.Detectors
{
    class TwoDimensionalTracker : IDetector
    {
        public DetectorResult Process(Image<Bgr, byte> rawFrame, Image<Gray, byte> grayFrame)
        {
            var surfParam = new SurfDetector(900, true);

            var modelImage = new Image<Gray, byte>("iphone\\signin.jpg");
            var modelFeatures = surfParam.DetectFeatures(modelImage, null);
            var tracker = new Features2DTracker(modelFeatures);


            var imageFeatures = surfParam.DetectFeatures(grayFrame, null);
            var homographyMatrix = tracker.Detect(imageFeatures, 100.0);
            
            Image<Bgr, Byte> processedImage = modelImage.Convert<Bgr, Byte>().ConcateVertical(rawFrame);

            if (homographyMatrix != null)
            {  
                var rect = modelImage.ROI;
                var pts = new[]
                              {
                                  new PointF(rect.Left, rect.Bottom),
                                  new PointF(rect.Right, rect.Bottom),
                                  new PointF(rect.Right, rect.Top),
                                  new PointF(rect.Left, rect.Top)
                              };
                homographyMatrix.ProjectPoints(pts);

                for (int i = 0; i < pts.Length; i++)
                    pts[i].Y += modelImage.Height;
                
                processedImage.DrawPolyline(Array.ConvertAll(pts, Point.Round), true, new Bgr(Color.DarkOrange), 1);
            }
            return new DetectorResult(){RawImage = rawFrame, ProcessedImage = processedImage};
        }
    }
}
