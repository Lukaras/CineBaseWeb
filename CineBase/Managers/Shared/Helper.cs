using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CineBase
{
    public class Helper
    {

        private static Random rng = new Random();

        public static string Hashing(string input)
        {
            return System.Text.Encoding.ASCII.GetString(SHA256.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(input)));
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rng.Next(s.Length)]).ToArray());
        }

    }
}
