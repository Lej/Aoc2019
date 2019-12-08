using System.Globalization;

namespace Aoc2019.Extensions
{
    public static class CharExtensions
    {
        public static int ToDigit(this char c)
        {
            return CharUnicodeInfo.GetDecimalDigitValue(c);
        }
    }
}
