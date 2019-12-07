using Aoc2019.Extensions;
using Aoc2019.Intcode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Aoc2019.Tests.Days.Day2
{
    [TestClass]
    public class Day2
    {
        [TestMethod]
        public void SolvePart1()
        {
            var input = this.ReadEmbedded("Day2.txt");

            var program = new Program(input);
            program.Memory[1] = 12;
            program.Memory[2] = 2;
            program.Execute();

            var value = program.Memory[0];
            Console.WriteLine(value);
            Assert.AreEqual(3790645, value);
        }

        [TestMethod]
        public void SolvePart2()
        {
            var input = this.ReadEmbedded("Day2.txt");

            for (var noun = 0; noun < 100; noun++)
            {
                for (var verb = 0; verb < 100; verb++)
                {
                    var program = new Program(input);
                    program.Memory[1] = noun;
                    program.Memory[2] = verb;
                    program.Execute();

                    if (program.Memory[0] == 19690720)
                    {
                        var value = 100 * noun + verb;
                        Console.WriteLine(value);
                        Assert.AreEqual(6577, value);
                        break;
                    }
                }
            }
        }
    }
}
