using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Token
{
    public interface ITokenHelper
    {
        string CreateToken(int length, bool toLower = false);
    }
}
