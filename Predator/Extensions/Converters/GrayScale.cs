using Emgu.CV;
using Emgu.CV.Structure;

namespace PredatorCV.Extensions.Converters
{
    public static class GrayScale
    {
        public static Image<Gray, byte> ToGray(this Image<Bgr, byte> image)
        {
            return image.Convert<Gray, byte>();
        }
    }
}
