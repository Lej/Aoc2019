using Aoc2019.Extensions;
using Aoc2019.Intcode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Aoc2019.Tests.Days.Day9
{
    [TestClass]
    public class Day9
    {
        [TestMethod]
        public void SolvePart1()
        {
            var programText = this.ReadEmbedded("Day9.txt");
            var program = new Program(programText);
            program.Input.Write(1);
            program.Execute();
            Assert.AreEqual(1, program.Output.Count);
            var output = program.Output.Single();
            Assert.AreEqual(2955820355, output);
            Console.WriteLine(output);
        }

        [TestMethod]
        public void SolvePart2()
        {
            var programText = this.ReadEmbedded("Day9.txt");
            var program = new Program(programText);
            program.Input.Write(2);
            program.Execute();
            Assert.AreEqual(1, program.Output.Count);
            var output = program.Output.Single();
            Assert.AreEqual(46643, output);
            Console.WriteLine(output);
        }
    }
}
