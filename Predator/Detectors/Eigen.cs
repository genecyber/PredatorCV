using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Emgu.CV.CvEnum;
using Emgu.CV;
using Emgu.CV.Structure;

namespace PredatorCV.Detectors
{
    public class Eigen : IDetector
    {
        private List<string> _fileNames;
        private List<string> _labels;
        private readonly List<Image<Gray, byte>> _imgs = new List<Image<Gray, byte>>();
        private MCvTermCriteria _termCrit;
        private EigenObjectRecognizer _recognizer;
        private string DIRECTORY;


        /// <summary>
        /// Setups this instance.
        /// </summary>
        public Eigen(string directory, int iterations)
        {
            DIRECTORY = directory;
            _fileNames = GetImageFilesFromDirectory(DIRECTORY).ToList();
            _labels = GetLabels(_fileNames);
            foreach (var fileName in _fileNames)
            {
                _imgs.Add(new Image<Gray, Byte>(fileName));
            }
            MakeRecognizer(iterations);
        }

        /// <summary>
        /// Processes the specified raw frame.
        /// </summary>
        /// <param name="rawFrame">The raw frame.</param>
        /// <param name="grayFrame">The gray frame.</param>
        /// <returns></returns>
        public DetectorResult Process(Image<Bgr,byte> rawFrame, Image<Gray,byte> grayFrame)
        {
            int confidence = 0;
            var frame = grayFrame.Resize(320, 240, INTER.CV_INTER_CUBIC, false);
            string label = _recognizer.Recognize(frame);
            if (label != "")
                confidence = 100;
            return new DetectorResult
                       {
                           Confidence = confidence,
                           Label = label,
                           ProcessedImage = frame
                       };
        }

        private static IEnumerable<string> GetImageFilesFromDirectory(string directory)
        {
            return Directory.GetFiles(directory).ToList().Where(f => f.Contains(".jpg"));
        }

        private void MakeRecognizer(int maxIteration)
        {
            _termCrit = new MCvTermCriteria(maxIteration, 0.001);
            _recognizer = new EigenObjectRecognizer(_imgs.ToArray(), _labels.ToArray(), 5000, ref _termCrit);
        }

        

        private static List<string> GetLabels(List<string> files)
        {
            var labels = new List<string>();
            for (int i = 0; i < files.Count; i++)
            {
                var label = files[i].Split('_')[0].Split('\\').Last();
                labels.Add(label);
            }
            return labels;
        }
    }

}
