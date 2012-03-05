using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using PredatorCV.Collection;
using PredatorCV.Extensions;

namespace PredatorCV.Sources
{
    public class SourceFactory
    {
        public static ResultOf<ISource> GetCaptureDevice(String source, EventHandler processor)
        {
            switch (Enum.Parse(typeof(VideoSource),source).As<VideoSource>())
            {
                case VideoSource.Camera:
                    return new ResultOf<ISource>(GetCamera(processor).Value);
                case VideoSource.Desktop:
                    return  new ResultOf<ISource>(GetDesktop(processor).Value);
                case VideoSource.Image:
                    return new ResultOf<ISource>(GetImage(processor).Value);
            }
            return new ResultOf<ISource>("Error");
        }
        public static ResultOf<Camera> GetCamera(EventHandler processor)
        {
            var result = new ResultOf<Camera>();
            try
            {
                result.Value = new Camera();
                Application.Idle += processor;

            }
            catch (NullReferenceException e)
            {
                result.AddMessage(e.Message);
            }
            return result;
        }
        public static ResultOf<Desktop> GetDesktop(EventHandler processor)
        {
            var result = new ResultOf<Desktop>();
            try
            {
                result.Value = new Desktop();
                Application.Idle += processor;

            }
            catch (NullReferenceException e)
            {
                result.AddMessage(e.Message);
            }
            return result;
        }
        public static ResultOf<Image> GetImage(EventHandler processor)
        {
            var result = new ResultOf<Image>();
            try
            {
                result.Value = new Image();
                Application.Idle += processor;

            }
            catch (NullReferenceException e)
            {
                result.AddMessage(e.Message);
            }
            return result;
        }

        public enum VideoSource
        {
            Camera = 0,
            Desktop,
            Image,
            iOS,
            Android
        }

    }
}
