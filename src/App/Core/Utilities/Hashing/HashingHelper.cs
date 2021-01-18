using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        public static string CreateCryptedText(string text, string secretKey)
        {
            StringBuilder hash = new StringBuilder();
            byte[] secretkeyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] inputBytes = Encoding.UTF8.GetBytes(text);
            if (secretKey=="")
            {
                using (var hmac = new HMACSHA512())
                {
                    byte[] hashValue = hmac.ComputeHash(inputBytes);
                    foreach (var theByte in hashValue)
                    {
                        hash.Append(theByte.ToString("x2"));
                    }
                }
            }
            else
            {
                using (var hmac = new HMACSHA512(secretkeyBytes))
                {
                    byte[] hashValue = hmac.ComputeHash(inputBytes);
                    foreach (var theByte in hashValue)
                    {
                        hash.Append(theByte.ToString("x2"));
                    }
                }
            }

            return hash.ToString();
        }
    }
}
