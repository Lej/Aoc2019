using Aoc2019.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2019.Tests.Days.Day3
{
    [TestClass]
    public class Day3
    {
        [TestMethod]
        public void SolvePart1()
        {
            var distance = DistancePart1(CreateMap(Parse(this.ReadEmbeddedLines("Day3.txt"))));

            Console.WriteLine(distance);
            Assert.AreEqual(266, distance);
        }

        [TestMethod]
        public void SolvePart2()
        {
            var distance = DistancePart2(CreateMap(Parse(this.ReadEmbeddedLines("Day3.txt"))));

            Console.WriteLine(distance);
            Assert.AreEqual(19242, distance);
        }

        public class Segment
        {
            public char Direction { get; }
            public int Distance { get; }

            public Segment(char direction, int distance)
            {
                Direction = direction;
                Distance = distance;
            }
        }

        public class Wire
        {
            public int Index { get; }

            public Segment[] Segments { get; }

            public Wire(int index, Segment[] segments)
            {
                Index = index;
                Segments = segments;
            }
        }

        public class Map : Dictionary<(int X, int Y), Dictionary<int, int>>
        {
        }

        [DataTestMethod]
        [DataRow(new[] { "R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83" }, 159)]
        [DataRow(new[] { "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7" }, 135)]
        public void TestPart1(string[] inputs, int output) => Assert.AreEqual(output, DistancePart1(CreateMap(Parse(inputs))));

        [DataTestMethod]
        [DataRow(new[] { "R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83" }, 610)]
        [DataRow(new[] { "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7" }, 410)]
        public void TestPart2(string[] inputs, int output) => Assert.AreEqual(output, DistancePart2(CreateMap(Parse(inputs))));


        public Wire[] Parse(IEnumerable<string> inputs) => inputs.Select((x, i) => new Wire(i, x.Split(",").Select(x => new Segment(x[0], int.Parse(x.Substring(1)))).ToArray())).ToArray();

        public Map CreateMap(Wire[] wires)
        {
            var map = new Map();

            foreach (var wire in wires)
            {
                AddWire(map, wire);
            }

            return map;
        }

        public int DistancePart1(Map map)
        {
            var distance = map.Where(kvp => kvp.Value.Count > 1)
                .Select(kvp => Math.Abs(kvp.Key.X) + Math.Abs(kvp.Key.Y))
                .OrderBy(x => x)
                .First();

            return distance;
        }

        public int DistancePart2(Map map)
        {
            var distance = map.Where(kvp => kvp.Value.Count > 1)
                .Select(kvp => kvp.Value.Values.Sum())
                .OrderBy(x => x)
                .First();

            return distance;
        }

        private void AddWire(Map map, Wire wire)
        {
            var delta = new Dictionary<char, (int Dx, int Dy)>
            {
                { 'U', (Dx:  0, Dy:  1) },
                { 'R', (Dx:  1, Dy:  0) },
                { 'D', (Dx:  0, Dy: -1) },
                { 'L', (Dx: -1, Dy:  0) },
            };

            var distance = 0;
            var x = 0;
            var y = 0;
            foreach (var segment in wire.Segments)
            {
                var (dx, dy) = delta[segment.Direction];
                for (var i = 0; i < segment.Distance; i++)
                {
                    distance++;
                    x += dx;
                    y += dy;
                    if (!map.TryGetValue((x, y), out var distances)) {
                        distances = map[(x, y)] = new Dictionary<int, int>();
                    }
                    if (!distances.ContainsKey(wire.Index))
                    {
                        distances[wire.Index] = distance;
                    }
                }
            }
        }
    }
}
