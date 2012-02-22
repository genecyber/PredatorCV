using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Features2D;
//using Emgu.CV.UI;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace PredatorCV.Detectors
{
    public class Surf : IDetector
    {
        public DetectorResult Process(Image<Bgr, byte> rawFrame, Image<Gray, byte> grayFrame)
        {
            //MCvSURFParams surfParam = new MCvSURFParams(500, false);
            SurfDetector surfParam = new SurfDetector(500, false);

            Image<Gray, Byte> modelImage = new Image<Gray, byte>("box.png");
            //extract features from the object image
            SURFFeature[] modelFeatures = modelImage.ExtractSURF(ref surfParam);

            //Image<Gray, Byte> observedImage = new Image<Gray, byte>("box_in_scene.png");
            // extract features from the observed image
            SURFFeature[] imageFeatures = grayFrame.ExtractSURF(ref surfParam);

            //Create a SURF Tracker using k-d Tree
            SURFTracker tracker = new SURFTracker(modelFeatures);
            //Comment out above and uncomment below if you wish to use spill-tree instead
            //SURFTracker tracker = new SURFTracker(modelFeatures, 50, .7, .1);

            SURFTracker.MatchedSURFFeature[] matchedFeatures = tracker.MatchFeature(imageFeatures, 2, 20);
            matchedFeatures = SURFTracker.VoteForUniqueness(matchedFeatures, 0.8);
            matchedFeatures = SURFTracker.VoteForSizeAndOrientation(matchedFeatures, 1.5, 20);
            HomographyMatrix homography = SURFTracker.GetHomographyMatrixFromMatchedFeatures(matchedFeatures);

            //Merge the object image and the observed image into one image for display
            Image<Gray, Byte> res = modelImage.ConcateVertical(grayFrame);

            #region draw lines between the matched features
            foreach (SURFTracker.MatchedSURFFeature matchedFeature in matchedFeatures)
            {
                PointF p = matchedFeature.ObservedFeature.Point.pt;
                p.Y += modelImage.Height;
                res.Draw(new LineSegment2DF(matchedFeature.ObservedFeature.Point.pt, p), new Gray(0), 1);
            }
            #endregion

            #region draw the project region on the image
            if (homography != null)
            {  //draw a rectangle along the projected model
                Rectangle rect = modelImage.ROI;
                PointF[] pts = new PointF[] { 
               new PointF(rect.Left, rect.Bottom),
               new PointF(rect.Right, rect.Bottom),
               new PointF(rect.Right, rect.Top),
               new PointF(rect.Left, rect.Top)};
                homography.ProjectPoints(pts);

                for (int i = 0; i < pts.Length; i++)
                    pts[i].Y += modelImage.Height;

                res.DrawPolyline(Array.ConvertAll<PointF, Point>(pts, Point.Round), true, new Gray(255.0), 5);
            }
            #endregion

            return new DetectorResult()
                       {RawImage = rawFrame, Confidence = 100, GrayImage = grayFrame, ProcessedImage = res};
        }
    }
}
