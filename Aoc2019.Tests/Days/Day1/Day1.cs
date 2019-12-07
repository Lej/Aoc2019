using Aoc2019.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Aoc2019.Tests.Days.Day1
{
    [TestClass]
    public class Day1
    {
        [TestMethod]
        public void SolvePart1()
        {
            var fuel = this.ReadEmbeddedLines("Day1.txt")
                .Select(Fuel)
                .Sum();

            Console.WriteLine(fuel);
            Assert.AreEqual(3414791, fuel);
        }

        [DataTestMethod]
        [DataRow("1969", 654)]
        [DataRow("100756", 33583)]
        public void FuelTest(string input, int output) => Assert.AreEqual(output, Fuel(input));

        private int Fuel(string input) => int.Parse(input) / 3 - 2;

        [TestMethod]
        public void SolvePart2()
        {
            var fuel = this.ReadEmbeddedLines("Day1.txt")
                .Select(MoreFuel)
                .Sum();

            Console.WriteLine(fuel);
            Assert.AreEqual(5119312, fuel);
        }

        [DataTestMethod]
        [DataRow("1969", 966)]
        [DataRow("100756", 50346)]
        public void MoreFuelTest(string input, int output) => Assert.AreEqual(output, MoreFuel(input));

        private int MoreFuel(string input) => Fuel(input).AsEnumerable().SelectWhile(x => x / 3 - 2, x => x > 0).Sum();
    }
}
