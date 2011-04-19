using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PredatorCV.Collection
{
    public class ResultOf<T> : ResultBase where T : class
    {
        public T Value { get; set; }

        public bool HasValue { get { return Value != null; } }

        public ResultOf()
        {
            Value = null;
        }

        public ResultOf(T value)
        {
            Value = value;
        }

        public ResultOf(ResultBase messages)
        {
            Value = null;
            AppendResult(messages);
        }

        public ResultOf(string message)
        {
            Value = null;
            AddMessage(message);
        }
    }
}
