using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _ExperimentCV.Extensions
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
