using Aoc2019.Extensions;
using Aoc2019.Intcode;
using Aoc2019.Tests.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2019.Tests.Intcode
{
    [TestClass]
    public class ProgramTests
    {

        [DataTestMethod]
        [DataRow("1,0,0,0,99", new[] { 2, 0, 0, 0, 99 })]
        [DataRow("2,3,0,3,99", new[] { 2, 3, 0, 6, 99 })]
        [DataRow("2,4,4,5,99,0", new[] { 2, 4, 4, 5, 99, 9801 })]
        [DataRow("1,1,1,4,99,5,6,0,99", new[] { 30, 1, 1, 4, 2, 5, 6, 0, 99 })]
        [DataRow("1002,4,3,4,33", new[] { 1002, 4, 3, 4, 99 })]
        [DataRow("1101,100,-1,4,0", new[] { 1101, 100, -1, 4, 99 })]
        public void ProgramMemoryTest(string input, int[] output)
        {
            var program = new Program(input);
            program.Execute(null, null);
            var memory = program.Memory;
            Assert.That.SequenceEquals(output, memory.ToList());
        }

        [DataTestMethod]
        [DataRow("3,9,8,9,10,9,4,9,99,-1,8", new[] { 1 }, new[] { 0 })] // 1 != 8 => 0
        [DataRow("3,9,8,9,10,9,4,9,99,-1,8", new[] { 8 }, new[] { 1 })] // 8 == 8 => 1

        [DataRow("3,9,7,9,10,9,4,9,99,-1,8", new[] { 1 }, new[] { 1 })] // 1 < 8 => 1
        [DataRow("3,9,7,9,10,9,4,9,99,-1,8", new[] { 8 }, new[] { 0 })] // 8 < 8 => 0
        [DataRow("3,9,7,9,10,9,4,9,99,-1,8", new[] { 10 }, new[] { 0 })] // 10 < 8 => 0

        [DataRow("3,3,1108,-1,8,3,4,3,99", new[] { 1 }, new[] { 0 })] // 1 != 8 => 0
        [DataRow("3,3,1108,-1,8,3,4,3,99", new[] { 8 }, new[] { 1 })] // 8 == 8 => 1

        [DataRow("3,3,1107,-1,8,3,4,3,99", new[] { 1 }, new[] { 1 })] // 1 < 8 => 1
        [DataRow("3,3,1107,-1,8,3,4,3,99", new[] { 8 }, new[] { 0 })] // 8 < 8 => 0
        [DataRow("3,3,1107,-1,8,3,4,3,99", new[] { 10 }, new[] { 0 })] // 10 < 8 => 0

        [DataRow("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", new[] { 0 }, new[] { 0 })] // Jump test: 0 -> 0
        [DataRow("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", new[] { 1 }, new[] { 1 })] // Jump test: 1 -> 1
        [DataRow("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", new[] { 2 }, new[] { 1 })] // Jump test: 1 -> 1

        [DataRow("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", new[] { 0 }, new[] { 0 })] // Jump test: 0 -> 0
        [DataRow("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", new[] { 1 }, new[] { 1 })] // Jump test: 1 -> 1
        [DataRow("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", new[] { 2 }, new[] { 1 })] // Jump test: 1 -> 1

        [DataRow("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", new[] { 7 }, new[] { 999 })] // 7 < 8 -> 999
        [DataRow("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", new[] { 8 }, new[] { 1000 })] // 8 == 8 -> 1000
        [DataRow("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", new[] { 9 }, new[] { 1001 })] // 9 > 8 -> 1001
        public void ProgramOutputTest(string programText, IEnumerable<int> actualInput, IEnumerable<int> expectedOutput)
        {
            var program = new Program(programText);
            var input = new Queue<int>();
            actualInput.ForEach(x => input.Enqueue(x));
            var output = new Queue<int>();
            program.Execute(input, output);
            Assert.That.SequenceEquals(expectedOutput, output);
        }
    }
}
