using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace PredatorCV.Sources
{
    public interface IVideo
    {
        Image<Bgr, Byte> QueryFrame();
        Image<Bgr, Byte> QueryFrame(int destX, int destY , int cx , int cy );
    }
}
