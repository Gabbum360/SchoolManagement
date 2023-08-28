using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.ApplicationServices.Features
{
    public class GenerateOtp
    {
        private static Random RNG = new Random();
        private static string Create6DigitNumbers(int number)
        {
            var builder = new StringBuilder();
            while (builder.Length < number)
            {
                builder.Append(RNG.Next(6).ToString());
            }
            return builder.ToString();
        }

        private static HashSet<string> Results = new HashSet<string>();
        public static string CreateUniqueNumber(int number)
        {
            var result = Create6DigitNumbers(number);
            while (!Results.Add(result))
            {
                result = Create6DigitNumbers(number);
            }
            return result;
        }

    }
}
