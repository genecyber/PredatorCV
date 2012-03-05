using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace PredatorCV.Detectors
{
    class Passthrough : IDetector
    {
        public DetectorResult Process(Image<Bgr, byte> rawFrame, Image<Gray, byte> grayFrame)
        {
            return new DetectorResult(){ProcessedImage = rawFrame};
        }
    }
}
