using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace PredatorCV.Detectors
{
    /// <summary>
    /// Interface for detectors.
    /// </summary>
    public interface IDetector
    {
        /// <summary>
        /// Processes the specified raw frame.
        /// </summary>
        /// <param name="rawFrame">The raw frame.</param>
        /// <param name="grayFrame">The gray frame.</param>
        /// <returns></returns>
        DetectorResult Process(Image<Bgr, byte> rawFrame, Image<Gray, byte> grayFrame);
    }

    /// <summary>
    /// An object representation of the detector.
    /// </summary>
    public class DetectorResult
    {
        public IImage RawImage { get; set; }
        public IImage GrayImage { get; set; }
        public IImage ProcessedImage { get; set; }
        public Rectangle ROI { get; set; }
        public string Label { get; set; }
        public int Confidence { get; set; }
    }
}
