using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, List<int> messages) : base(data, true, messages)
        {
        }

        public SuccessDataResult(T data) : base(data, true)
        {
        }

        public SuccessDataResult(List<int> messages) : base(default, true, messages)
        {
        }

        public SuccessDataResult() : base(default, true)
        {
        }
    }
}
