using System;
using System.Globalization;
using System.Linq;

namespace Aoc2019.Extensions
{
    public static class IntExtensions
    {
        public static int[] ToDigits(this int i)
        {
            return i.ToString().ToCharArray().Select(CharUnicodeInfo.GetDecimalDigitValue).ToArray();
        }

        public static int Pow(this int baze, int exponent)
        {
            if (exponent < 0) throw new NotImplementedException();
            int result = 1;
            for(var i = 0; i < exponent; i++)
            {
                result *= baze;
            }
            return result;
        }
    }
}
