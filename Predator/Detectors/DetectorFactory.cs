using System;
using PredatorCV.Collection;

namespace PredatorCV.Detectors
{
    public class DetectorFactory
    {
        public static ResultOf<IDetector> GetDetector(String source)
        {
            switch (source)
            {
                case "Circle":
                    return new ResultOf<IDetector>(GetCircle().Value);
                case "Triangle":
                    return new ResultOf<IDetector>(new Triangle());
                case "Face":
                    return new ResultOf<IDetector>(new Face());
                case "Contour":
                    return new ResultOf<IDetector>(new Contour());
                case "Eigen":
                    return new ResultOf<IDetector>(new Eigen("training\\", 1));
                case "Surf":
                    return new ResultOf<IDetector>(new Surf());
                case "Text":
                    return new ResultOf<IDetector>(new Text());
                case "2D":
                    return new ResultOf<IDetector>(new TwoDimensionalTracker());

            }
            return new ResultOf<IDetector>(new Passthrough());
        }

        private static ResultOf<Circle> GetCircle()
        {
            var result = new ResultOf<Circle>();
            try
            {
                result.Value = new Circle();

            }
            catch (NullReferenceException e)
            {
                result.AddMessage(e.Message);
            }
            return result;
        }
    }
}
