using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success, List<int> messages) : this(success)
        {
            Messages = messages;
        }

        public Result(bool success)
        {
            Success = success;
        }
        public bool Success { get; }
        public List<int> Messages { get; }
    }
}
