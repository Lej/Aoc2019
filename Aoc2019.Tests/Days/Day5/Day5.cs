using System;
using System.Collections.Generic;
using System.Linq;
using Aoc2019.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aoc2019.Intcode;

namespace Aoc2019.Tests.Days.Day5
{
    [TestClass]
    public class Day5
    {
        [TestMethod]
        public void SolvePart1()
        {
            var text = this.ReadEmbedded("Day5.txt");
            var program = new Program(text);
            program.Input.Enqueue(1);
            program.Execute();

            program.Output.Reverse().Skip(1).ForEach(x => Assert.AreEqual(0, x));
            var code = program.Output.Reverse().Take(1).Single();
            Console.WriteLine(code);
            Assert.AreEqual(13547311, code);
        }

        [TestMethod]
        public void SolvePart2()
        {
            var text = this.ReadEmbedded("Day5.txt");
            var program = new Program(text);
            program.Input.Enqueue(5);
            program.Execute();

            Assert.AreEqual(1, program.Output.Count);
            var code = program.Output.Dequeue();
            Console.WriteLine(code);
            Assert.AreEqual(236453, code);
        }
    }
}
