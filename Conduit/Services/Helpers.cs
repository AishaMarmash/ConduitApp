using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Conduit.Services
{
    public static class Helpers
    {
        public static string Combine(this List<string> tagsList)
        {
            string result=String.Empty;
            if (tagsList != null && tagsList.Count!=0)
            {
                result = tagsList.Aggregate((a, b) => a + "," + b);
            }
            return result;
        }
        public static List<string> MoveToList(this string tagsString)
        {
            List<string> result = tagsString.Split(",").ToList();
            return result;
        }
        public static string GenerateSlug(this string phrase)
        {
            string str = phrase.ToLower();

            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");          
            str = Regex.Replace(str, @"\s+", " ").Trim(); 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim(); 
            str = Regex.Replace(str, @"\s", "-");
            return str;
        }
    }
}