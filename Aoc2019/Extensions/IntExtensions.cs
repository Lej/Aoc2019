using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Aoc2019.Extensions
{
    public static class IntExtensions
    {
        public static long Factorial(this int i)
        {
            if (i < 0) throw new ArgumentException("Parameter can not be negative", nameof(i));
            var factorial = 1;
            for (var factor = 2; factor <= i; factor++)
            {
                factorial *= factor;
            }
            return factorial;
        }

        public static int[] ToFactoradicDigits(this int i)
        {
            var digits = new Stack<int>();
            var dividend = i;
            var divisor = 1;
            int reminder;
            do
            {
                reminder = dividend % divisor;
                digits.Push(reminder);

                var quotient = dividend / divisor;
                dividend = quotient;

                divisor++;
            } while (dividend > 0);
            return digits.ToArray();
        }

        public static int ToFactoradic(this int i)
        {
            return i.ToFactoradicDigits().ToInt();
        }

        public static int[] ToDigits(this int i)
        {
            return i.ToString().ToCharArray().Select(x => x.ToDigit()).ToArray();
        }

        public static int Pow(this int baze, int exponent)
        {
            if (exponent < 0) throw new NotImplementedException();
            int result = 1;
            for (var i = 0; i < exponent; i++)
            {
                result *= baze;
            }
            return result;
        }
    }
}
