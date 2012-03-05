using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace PredatorCV.Extensions
{
    public static class Cast
    {
        public static T As<T>(this Object obj)
        {
            if (obj == null)
                return default(T);

            if (obj is T)
                return (T)obj;

            if (typeof(T) == typeof(String))
                return obj.ToString().As<T>();

            if (typeof(T) == typeof(DateTime))
                return DateTime.Parse(obj.ToString()).As<T>();

            if (typeof(T) == typeof(bool) && obj.GetType() == typeof(String))
            {
                switch (obj.ToString().ToLower())
                {
                    case "1":
                    case "true":
                    case "yes":
                    case "on":
                        return true.As<T>();

                    case "0":
                    case "false":
                    case "no":
                    case "off":
                        return false.As<T>();

                    default:
                        return false.As<T>();
                }
            }

            if (typeof(T).IsEnum)
            {
                return Enum.Parse(typeof(T), obj.ToString()).As<T>();
            }

            if (typeof(T) == typeof(decimal))
            {
                decimal result;

                decimal.TryParse(obj.ToString(), out result);

                return result.As<T>();
            }
            if (typeof(T) == typeof(int))
            {
                int result;

                int.TryParse(obj.ToString(), out result);

                return result.As<T>();
            }
            if (typeof(T) == typeof(short))
            {
                short result;

                short.TryParse(obj.ToString(), out result);

                return result.As<T>();
            }
            if (typeof(T) == typeof(List<int>))
            {
                var result = (from item in (String[])obj select Int32.Parse(item)).ToList();
                return result.As<T>();
            }
            if (typeof(T) == typeof(List<string>))
            {
                var result = ((String[])obj).ToList();
                return result.As<T>();
            }
            if (typeof(T) == typeof(XDocument) && obj.GetType() == typeof(XElement))
            {
                var result = new XDocument(obj);
                return result.As<T>();
            }
            if (typeof(T) == typeof(XDocument) && obj.GetType() == typeof(string))
            {
                var result = XDocument.Parse(obj.As<string>());
                return result.As<T>();
            }
            if (typeof(T) == typeof(XDocument) && obj.GetType() == typeof(string))
            {
                var result = XDocument.Parse(obj.As<string>());
                return result.As<T>();
            }
            if (typeof(T) == typeof(XDocument) && obj.GetType() == typeof(string))
            {
                var result = XDocument.Parse(obj.As<string>());
                return result.As<T>();
            }
            return (T)obj;
        }
    }
}
