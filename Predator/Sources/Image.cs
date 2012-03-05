using System;
using Emgu.CV;
using Emgu.CV.Structure;

namespace PredatorCV.Sources
{
    public class Image : ISource
    {
        private static Image<Bgr, byte> _image = null;

        public Image<Bgr, byte> QueryFrame()
        {
            if (_image == null)
                _image = new Image<Bgr, byte>("iphone\\photo.png");
            return _image;
        }

        public Image<Bgr, byte> QueryFrame(int destX, int destY, int cx, int cy)
        {
            throw new NotImplementedException();
        }
    }
}
