using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Core.Utilities
{
    public static class Tools
    {
        public static int StrToInt(string term)
        {
            int Int;
            if (term == null)
            {
                Int = 0;
            }
            else
            {
                try
                {
                    Int = Int32.Parse(term);
                }
                catch (Exception)
                {
                    Int = 0;
                }
            }
            return Int;
        }

        public static bool IsObjectNullOrEmpty(object input)
        {
            bool result = false;
            if (input == null)
            {
                result = true;
            }

            return result;
        }

        public static bool IsObjectNullOrEmpty(ICollection input)
        {
            bool result = false;
            if (input == null || input.Count == 0)
            {
                result = true;
            }

            return result;
        }

    }
}
