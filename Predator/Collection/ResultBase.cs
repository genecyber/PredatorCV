using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PredatorCV.Collection
{
    public class ResultBase
    {
        private readonly List<string> _resultItems = new List<string>();

        public IEnumerable<string> Messages { get; set; }

        public void AppendResult(ResultBase resultToAppend)
        {
            AddMessages(resultToAppend.Messages);
        }

        public void AddMessages(IEnumerable<string> messages)
        {
            if (messages != null)
                foreach (var message in messages)
                {
                    AddMessage(message);
                }
        }

        public void AddMessage(string resultItem)
        {
            if (!_resultItems.Contains(resultItem)) _resultItems.Add(resultItem);
        }
    }
}
