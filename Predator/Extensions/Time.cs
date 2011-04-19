using System;
using System.IO;


namespace PredatorCV.Extensions
{
    public static class Time
    {
        public static bool SavedWithinSeconds(this string fileName, int seconds)
        {
            var last = File.GetCreationTime(fileName);
            return last + TimeSpan.FromSeconds(seconds) < DateTime.Now;
        }
    }
}
