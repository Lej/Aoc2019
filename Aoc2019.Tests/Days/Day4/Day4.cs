using Aoc2019.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Linq;

namespace Aoc2019.Tests.Days.Day4
{
    [TestClass]
    public class Day4
    {
        [TestMethod]
        public void SolvePart1()
        {
            var passwords = EnumerableUtils.FromTo(152085, 670283)
                .Select(ToDigits)
                .Where(IsNonDecreasing)
                .Where(HasAdjacent)
                .ToList();

            var count = passwords.Count();
            Console.WriteLine(count);
            Assert.AreEqual(1764, count);
        }

        [TestMethod]
        public void SolvePart2()
        {
            var passwords = EnumerableUtils.FromTo(152085, 670283)
                .Select(ToDigits)
                .Where(IsNonDecreasing)
                .Where(HasTwoAdjacent)
                .ToList();

            var count = passwords.Count();
            Console.WriteLine(count);
            Assert.AreEqual(1196, count);
        }

        public int[] ToDigits(int i)
        {
            return i.ToString().ToCharArray().Select(CharUnicodeInfo.GetDecimalDigitValue).ToArray();
        }

        private bool IsNonDecreasing(int[] digits)
        {
            return digits.Skip(1).Select((digit, i) => (digit, i)).All(x => x.digit >= digits[x.i]);
        }

        private bool HasAdjacent(int[] digits)
        {
            return digits.Skip(1).Select((digit, i) => (digit, i)).Any(x => x.digit == digits[x.i]);
        }

        private bool HasTwoAdjacent(int[] digits)
        {
            for (var i = 0; i < digits.Length - 1; i++)
            {
                if (digits[i] == digits[i + 1]
                    && (i == 0 || digits[i - 1] != digits[i])
                    && (i == digits.Length - 2 || digits[i + 2] != digits[i]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
