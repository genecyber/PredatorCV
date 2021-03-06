﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace PredatorCV.Detectors
{
    public class Face : IDetector
    {
        public static Image<Bgr, byte> DetectAndDrawFaces(Image<Bgr, byte> image, HaarCascade face, HaarCascade eye)
        {
            Image<Gray, Byte> gray = image.Convert<Gray, Byte>(); //Convert it to Grayscale

            gray._EqualizeHist();

            //Detect the faces  from the gray scale image and store the locations as rectangle
            //The first dimensional is the channel
            //The second dimension is the index of the rectangle in the specific channel
            MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(
               face,
               1.1,
               10,
               Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
               new Size(20, 20));

            foreach (MCvAvgComp f in facesDetected[0])
            {
                //draw the face detected in the 0th (gray) channel with blue color
                image.Draw(f.rect, new Bgr(Color.Blue), 2);

                //Set the region of interest on the faces
                if (eye != null)
                    DetectAndDrawEyes(image, gray, f, eye);
            }
            return image;
        }

        private static void DetectAndDrawEyes(Image<Bgr, byte> image, Image<Gray, byte> gray, MCvAvgComp f, HaarCascade eye)
        {
            gray.ROI = f.rect;
            MCvAvgComp[][] eyesDetected = gray.DetectHaarCascade(
                eye,
                1.1,
                10,
                Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                new Size(20, 20));
            gray.ROI = Rectangle.Empty;

            foreach (MCvAvgComp e in eyesDetected[0])
            {
                Rectangle eyeRect = e.rect;
                eyeRect.Offset(f.rect.X, f.rect.Y);
                image.Draw(eyeRect, new Bgr(Color.Red), 2);
            }
            
        }

        public DetectorResult Process(Image<Bgr, byte> rawFrame, Image<Gray, byte> grayFrame)
        {
            return new DetectorResult() { ProcessedImage = DetectAndDrawFaces(rawFrame, new HaarCascade("Detectors\\haar\\haarcascade_frontalface_alt_tree.xml"), new HaarCascade("Detectors\\haar\\haarcascade_eye.xml")) };
        }
    }
}
