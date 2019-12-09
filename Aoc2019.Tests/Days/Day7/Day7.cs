using Aoc2019.Extensions;
using Aoc2019.Intcode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aoc2019.Tests.Days.Day7
{
    [TestClass]
    public class Day7
    {
        [TestMethod]
        public void SolvePart1()
        {
            var programText = this.ReadEmbedded("Day7.txt");

            var maxThrusterSignal = Enumerable.Range(0, 5).Permutations().Select(x => GetThrusterSignal(programText, x.ToList(), false)).Max();

            Console.WriteLine(maxThrusterSignal);
            Assert.AreEqual(79723, maxThrusterSignal);
        }

        [TestMethod]
        public void SolvePart2()
        {
            var programText = this.ReadEmbedded("Day7.txt");

            var maxThrusterSignal = Enumerable.Range(5, 5).Permutations().Select(x => GetThrusterSignal(programText, x.ToList(), true)).Max();

            Console.WriteLine(maxThrusterSignal);
            Assert.AreEqual(70602018, maxThrusterSignal);
        }

        [DataTestMethod]
        [DataRow("3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0", new[] { 4, 3, 2, 1, 0 }, 43210)]
        [DataRow("3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0", new[] { 0, 1, 2, 3, 4 }, 54321)]
        [DataRow("3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0", new[] { 1, 0, 4, 3, 2 }, 65210)]
        public void ThrusterSignalTest(string programText, IList<int> phaseSettings, int expectedThrusterSignal)
        {
            var thrusterSignal = GetThrusterSignal(programText, phaseSettings, false);
            Assert.AreEqual(expectedThrusterSignal, thrusterSignal);
        }

        [DataTestMethod]
        [DataRow("3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5", new[] { 9, 8, 7, 6, 5 }, 139629729)]
        [DataRow("3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10", new[] { 9, 7, 8, 5, 6 }, 18216)]
        public void ThrusterSignalFeedbackLoopTest(string programText, IList<int> phaseSettings, int expectedThrusterSignal)
        {
            var thrusterSignal = GetThrusterSignal(programText, phaseSettings, true);
            Assert.AreEqual(expectedThrusterSignal, thrusterSignal);
        }

        private int GetThrusterSignal(string programText, IList<int> phaseSettings, bool loop)
        {
            var programs = phaseSettings.Select(x => new Program(programText)).ToArray();
            for (var i = 1; i < programs.Length; i++)
            {
                programs[i - 1].Pipe(programs[i]);
            }
            if (loop) programs.Last().Pipe(programs.First());
            for (var i = 0; i < programs.Length; i++)
            {
                programs[i].Input.Write(phaseSettings[i]);
            }
            programs.First().Input.Write(0);
            var tasks = programs.Select(x => x.ExecuteAsync()).ToArray();
            Task.WaitAll(tasks);
            var thrusterSignal = programs.Last().Output.Read();
            return thrusterSignal.ToInt();
        }
    }
}
