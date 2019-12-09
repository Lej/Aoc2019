using System.Linq;
using System.Numerics;

namespace Aoc2019.Extensions
{
    public static class BigIntegerExtensions
    {
        public static int[] ToDigits(this BigInteger i)
        {
            return i.ToString().ToCharArray().Select(x => x.ToDigit()).ToArray();
        }

        public static int ToInt(this BigInteger i)
        {
            return int.Parse(i.ToString());
        }
    }
}
