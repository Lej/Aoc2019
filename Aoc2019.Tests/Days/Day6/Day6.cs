using Aoc2019.Extensions;
using Aoc2019.Orbits;
using Aoc2019.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2019.Tests.Days.Day6
{
    [TestClass]
    public class Day6
    {
        [TestMethod]
        public void SolvePart1()
        {
            var lines = this.ReadEmbeddedLines("Day6.txt");
            var com = GetCom(lines);

            var orbits = com.AsEnumerable().Flatten(x => x.Satellites).Sum(x => x.Depth);
            Console.WriteLine(orbits);
            Assert.AreEqual(139597, orbits);
        }

        [TestMethod]
        public void SolvePart2()
        {
            var lines = this.ReadEmbeddedLines("Day6.txt");
            var com = GetCom(lines);

            var you = com.AsEnumerable().Flatten(x => x.Satellites).Single(x => x.Name == "YOU");
            var san = com.AsEnumerable().Flatten(x => x.Satellites).Single(x => x.Name == "SAN");

            var commonAncestor = you.Parents.Intersect(san.Parents, LambdaComparer<Satellite>.Create((x, y) => x.Name == y.Name))
                .OrderBy(x => x.Depth)
                .Last();

            var transfers = (you.Depth - commonAncestor.Depth) + (san.Depth - commonAncestor.Depth) - 2;
            Console.WriteLine(transfers);
            Assert.AreEqual(286, transfers);
        }

        public Satellite GetCom(IEnumerable<string> lines)
        {
            var pairs = lines.Select(x =>
            {
                var parts = x.Split(")");
                return new
                {
                    Primary = parts[0],
                    Satellite = parts[1]
                };
            });

            var satellites = new Dictionary<string, Satellite>();
            foreach (var pair in pairs)
            {
                if (!satellites.TryGetValue(pair.Primary, out var primary))
                {
                    satellites[pair.Primary] = primary = new Satellite(pair.Primary);
                }

                if (!satellites.TryGetValue(pair.Satellite, out var satellite))
                {
                    satellites[pair.Satellite] = satellite = new Satellite(pair.Satellite);
                }

                primary.AddSatellite(satellite);
            }

            var com = satellites.Values.Single(x => x.Orbits == null);

            return com;
        }
    }
}
