using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, List<int> messages) : base(data, false, messages)
        {
        }

        public ErrorDataResult(T data) : base(data, false)
        {
        }

        public ErrorDataResult(List<int> messages) : base(default, false, messages)
        {
        }

        public ErrorDataResult() : base(default, false)
        {
        }
    }
}
