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
            var input = new Queue<int>();
            input.Enqueue(1);
            var output = new Queue<int>();

            var text = this.ReadEmbedded("Day5.txt");
            var program = new Program(text);
            program.Execute(input, output);

            output.Reverse().Skip(1).ForEach(x => Assert.AreEqual(0, x));
            var code = output.Reverse().Take(1).Single();
            Console.WriteLine(code);
            Assert.AreEqual(13547311, code);
        }

        [TestMethod]
        public void SolvePart2()
        {
            var input = new Queue<int>();
            input.Enqueue(5);
            var output = new Queue<int>();

            var text = this.ReadEmbedded("Day5.txt");
            var program = new Program(text);
            program.Execute(input, output);

            Assert.AreEqual(1, output.Count);
            var code = output.Dequeue();
            Console.WriteLine(code);
            Assert.AreEqual(236453, code);
        }
    }
}
