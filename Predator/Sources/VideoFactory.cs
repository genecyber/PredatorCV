using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using PredatorCV.Collection;

namespace PredatorCV.Sources
{
    public class VideoFactory
    {
        public static ResultOf<IVideo> GetCaptureDevice(VideoSource source, EventHandler processor)
        {
            switch (source)
            {
                case VideoSource.Camera:
                    return new ResultOf<IVideo>(GetCamera(processor).Value);
                case VideoSource.Desktop:
                    return  new ResultOf<IVideo>(GetDesktop(processor).Value);
            }
            return new ResultOf<IVideo>("Error");
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

        public enum VideoSource
        {
            Camera = 0,
            Desktop,
            Iphone,
            Android
        }
    }
}
