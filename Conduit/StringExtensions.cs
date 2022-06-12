using System;
using System.Security.Cryptography;
using System.Text;

namespace Conduit.Sercices
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
            foreach(var tag in tagsList)
            {
                result += tag + ",";
            }
            result.Remove(result.Length - 1);
            return result;
        }
    }
}
