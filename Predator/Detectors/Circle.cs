using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace PredatorCV.Detectors
{
    public class Circle : IDetector
    {
        public DetectorResult Process(Image<Bgr, byte> rawFrame, Image<Gray, byte> grayFrame)
        {
            grayFrame.PyrDown().PyrUp();
            var processedFrame = rawFrame.Copy();
            Gray cannyThreshold = new Gray(180);
            Gray circleAccumulatorThreshold = new Gray(500);

            CircleF[] circles = grayFrame.HoughCircles(
                cannyThreshold,
                circleAccumulatorThreshold,
                4.0, //Resolution of the accumulator used to detect centers of the circles
                15.0, //min distance 
                5, //min radius
                0 //max radius
                )[0]; //Get the circles from the first channel

            foreach (CircleF circle in circles)
            {
                processedFrame.Draw(circle, new Bgr(Color.DarkRed), 1);
            }
            return new DetectorResult() {RawImage = rawFrame, GrayImage = grayFrame, ProcessedImage = processedFrame};
        }
    }
}
