using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Conduit.Services
{
    public static class StringExtensions
    {
        public static string Sha256(this string input)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = sha.ComputeHash(bytes);

                return Convert.ToBase64String(hash);
            }
        }

        public static string Combine(this List<string> tagsList)
        {
            string result="";
            //tagsList.Aggregate((a, b) => a + "," + b);
            //foreach(var tag in tagsList)
            //{
            //    result += tag + ",";
            //}
            return result= tagsList.Aggregate((a, b) => a + "," + b);//.TrimEnd(','); ;
        }

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
