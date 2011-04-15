using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace _ExperimentCV.Converters
{
    public static class Canny
    {
        public static Image<Gray,byte> ToScaledSmoothedCanny(this Image<Bgr,byte> image)
        {
            Image<Gray, Byte> grayFrame = image.Convert<Gray, Byte>();
            Image<Gray, Byte> smallGrayFrame = grayFrame.PyrDown();
            Image<Gray, Byte> smoothedGrayFrame = smallGrayFrame.PyrUp();
            Image<Gray, Byte> cannyFrame = smoothedGrayFrame.Canny(new Gray(100), new Gray(60));
            return cannyFrame;
        }

        public static Image<Gray,byte> ToGray(this Image<Bgr,byte> image)
        {
            return image.Convert<Gray, byte>();
        }
    }
}
