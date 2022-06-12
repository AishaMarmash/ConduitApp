using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Conduit.Domain.Services
{
    public static class UserServices
    {
        public static string GenerateSlug(this string phrase)
        {
            string str = phrase.ToLower();

            str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); // invalid chars          
            str = Regex.Replace(str, @"\s+", " ").Trim(); // convert multiple spaces into one space  
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim(); // cut and trim it  
            str = Regex.Replace(str, @"\s", "-"); // hyphens  
            return str;
        }
    }
}