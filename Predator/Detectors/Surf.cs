using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using PredatorCV.Collection;

namespace PredatorCV.Detectors
{
    public class Surf : IDetector
    {
        private static SurfDetector _surfParam = new SurfDetector(900, true);
        private static readonly List<Image<Gray, byte>> ModelImages = GetModelImages();

        public DetectorResult Process(Image<Bgr, byte> rawFrame, Image<Gray, byte> grayFrame)
        {
            var processedImage = ModelImages.Aggregate(rawFrame, (current, modelImage) => ProcessSurf(modelImage, grayFrame, current));

            return new DetectorResult()
                       {RawImage = rawFrame, Confidence = 100, GrayImage = grayFrame, ProcessedImage = processedImage};
        }

        private static List<Image<Gray, byte>> GetModelImages()
        {
            return new List<Image<Gray,byte>>
                       {
                           //new Image<Gray, byte>("iphone\\signin_e.jpg"),
                           new Image<Gray, byte>("iphone\\signin.jpg")
                       };
        }

        private static Image<Bgr, byte> ProcessSurf(Image<Gray, byte> modelImage, Image<Gray, byte> grayFrame, Image<Bgr, byte> rawFrame)
        {
            SURFFeature[] modelFeatures = modelImage.ExtractSURF(ref _surfParam);
            SURFFeature[] imageFeatures = grayFrame.ExtractSURF(ref _surfParam);

            SURFTracker tracker = new SURFTracker(modelFeatures);


            SURFTracker.MatchedSURFFeature[] matchedFeatures = tracker.MatchFeature(imageFeatures, 2, 20);
            matchedFeatures = SURFTracker.VoteForUniqueness(matchedFeatures, 0.8);
            matchedFeatures = SURFTracker.VoteForSizeAndOrientation(matchedFeatures, 1.5, 20);
            HomographyMatrix homography = SURFTracker.GetHomographyMatrixFromMatchedFeatures(matchedFeatures);

            //Image<Bgr, Byte> processedImage = modelImage.Convert<Bgr, Byte>().ConcateVertical(rawFrame);
            var processedImage = rawFrame;
            //DrawLines(modelImage, matchedFeatures, processedImage);

            if (homography != null)
            {
                //draw a rectangle along the projected model
                var rect = modelImage.ROI;
                var pts = new PointF[]
                                   {
                                       new PointF(rect.Left, rect.Bottom),
                                       new PointF(rect.Right, rect.Bottom),
                                       new PointF(rect.Right, rect.Top),
                                       new PointF(rect.Left, rect.Top)
                                   };
                homography.ProjectPoints(pts);

                //for (int i = 0; i < pts.Length; i++)
                //    pts[i].Y += modelImage.Height;

                processedImage.DrawPolyline(Array.ConvertAll(pts, Point.Round), true, new Bgr(Color.DarkOrange), 1);
            }
            return processedImage;
        }

        private void DrawLines(Image<Gray, byte> modelImage, SURFTracker.MatchedSURFFeature[] matchedFeatures, Image<Bgr, byte> processedImage)
        {
            foreach (SURFTracker.MatchedSURFFeature matchedFeature in matchedFeatures)
            {
                PointF p = matchedFeature.ObservedFeature.Point.pt;
                p.Y += modelImage.Height;
                processedImage.Draw(new LineSegment2DF(matchedFeature.ObservedFeature.Point.pt, p), new Bgr(Color.DarkOrange), 1);
            }
        }
    }
}
