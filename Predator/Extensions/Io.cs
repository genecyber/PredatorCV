using System.Collections.Generic;
using System.IO;
using System.Linq;
using Emgu.CV;
using Emgu.CV.Structure;


namespace _ExperimentCV.Extensions
{
    public static class Io
    {
        public static void SaveImageAtIndex(this IImage image, string label, int index, string directory)
        {

            var lastImg = directory + "\\" + label + "_" + (index - 1) + ".jpg";
            var newImage = directory + "\\" + label + "_" + index + ".jpg";

            if (lastImg.SavedWithinSeconds(10))
            {
                image.Save(newImage);
            }
        }

        public static int TotalImagesWithLabel(this string directory, string label)
        {
            return Directory.GetFiles(directory).Where(f => f.Split('\\').Last().Split('_').First() == label).Count();
        }

        private static IEnumerable<string> GetImageFilesFromDirectory(this string directory)
        {
            return Directory.GetFiles(directory).ToList().Where(f => f.Contains(".jpg"));
        }

        public static void CleanImageFileNames(this string directory)
        {

            var files = directory.GetImageFilesFromDirectory();
            var index = 0;
            var label = "";
            foreach (var file in files)
            {
                var newLabel = file.Replace(directory + "\\", "").Split('_').First();
                if (label != newLabel)
                {
                    index = 0;
                }
                File.Move(file, directory + "\\" + newLabel +"-tmp" + "_" + index + ".jpg");
                label = newLabel;
                index++;
            }
            files = directory.GetImageFilesFromDirectory();
            foreach (var file in files)
            {
                File.Move(file,file.Replace("-tmp",""));
            }
        }
    }
}
