using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Token.StringToken
{
    public class StringTokenHelper : ITokenHelper
    {
        public IConfiguration _configuration;

        public StringTokenHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(int length, bool toLower)
        {
            string result = null;
            StringBuilder sb = new StringBuilder();
            char[] chars = "ABCDEFGHIJKLMNOPRSTUVYZabcdefghijklmnoprstuvyz0123456789".ToCharArray();
            int lengthOfArray = chars.Length;
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                sb.Append(chars[rnd.Next(0, lengthOfArray)].ToString());
            }
            result = toLower == true ? sb.ToString().ToLower() : sb.ToString();

            return result;
        }
    }
}
