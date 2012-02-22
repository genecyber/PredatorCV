using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace PredatorCV.Detectors
{

    public class Haar : IDetector
    {
        /// <summary>
        /// Stub
        /// </summary>
        /// <param name="rawFrame">The raw frame.</param>
        /// <param name="grayFrame">The gray frame.</param>
        /// <returns></returns>
        public DetectorResult Process(Image<Bgr, byte> rawFrame, Image<Gray, byte> grayFrame)
        {
            return new DetectorResult() { RawImage = rawFrame, Confidence = 100, GrayImage = grayFrame, ProcessedImage = rawFrame };
        }
    }
}
