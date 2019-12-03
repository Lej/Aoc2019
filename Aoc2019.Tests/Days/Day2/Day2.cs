using Aoc2019.Tests.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Aoc2019.Tests.Days.Day2
{
    [TestClass]
    public class Day2
    {
        [TestMethod]
        public void SolvePart1()
        {
            var input = EmbeddedResourceUtils.ReadToEnd("Day2.txt");
            var memory = Parse(input);
            memory[1] = 12;
            memory[2] = 2;
            Run(memory);

            var value = memory[0];
            Console.WriteLine(value);
            Assert.AreEqual(3790645, value);
        }

        [TestMethod]
        public void SolvePart2()
        {
            var input = EmbeddedResourceUtils.ReadToEnd("Day2.txt");

            for (var noun = 0; noun < 100; noun++)
            {
                for (var verb = 0; verb < 100; verb++)
                {
                    var memory = Parse(input);
                    memory[1] = noun;
                    memory[2] = verb;
                    Run(memory);
                    if (memory[0] == 19690720)
                    {
                        var value = 100 * noun + verb;
                        Console.WriteLine(value);
                        Assert.AreEqual(6577, value);
                        break;
                    }
                }
            }
        }

        [DataTestMethod]
        [DataRow("1,0,0,0,99", new[] { 2, 0, 0, 0, 99 })]
        [DataRow("2,3,0,3,99", new[] { 2, 3, 0, 6, 99 })]
        [DataRow("2,4,4,5,99,0", new[] { 2, 4, 4, 5, 99, 9801 })]
        [DataRow("1,1,1,4,99,5,6,0,99", new[] { 30, 1, 1, 4, 2, 5, 6, 0, 99 })]
        public void ParseRunTest(string input, int[] output) => Assert.That.SequenceEquals(output, Run(Parse(input)));

        private int[] Parse(string input) => input.Split(",").Select(int.Parse).ToArray();

        private int[] Run(int[] memory)
        {
            Func<int, int, int> add = (int x, int y) => x + y;
            Func<int, int, int> multiply = (int x, int y) => x * y;

            var i = 0;
            while (memory[i] != 99)
            {
                var op = memory[i] == 1 ? add : multiply;
                memory[memory[i + 3]] = op(memory[memory[i + 1]], memory[memory[i + 2]]);
                i += 4;
            }

            return memory;
        }
    }
}
