using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace toyshop.InfraStructure
{

    public static class StringHelper
    {
/// <summary>
/// 
/// </summary>
/// <param name="text"></param>
/// <returns>true if it`s Null</returns>
        public static bool CheckStringIsnull(this string text)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
