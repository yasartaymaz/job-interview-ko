using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(List<int> messages) : base(true, messages)
        {

        }

        public SuccessResult() : base(true)
        {

        }
    }
}
