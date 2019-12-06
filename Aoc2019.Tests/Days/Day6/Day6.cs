using Aoc2019.Tests.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc2019.Tests.Days.Day6
{
    [TestClass]
    public class Day6
    {
        [TestMethod]
        public void SolvePart1()
        {
            var lines = EmbeddedResourceUtils.ReadLines("Day6.txt");
            var com = GetCom(lines);

            var orbits = com.AsEnumerable().Flatten(x => x.Satellites).Sum(x => x.Depth);
            Console.WriteLine(orbits);
            Assert.AreEqual(139597, orbits);
        }

        [TestMethod]
        public void SolvePart2()
        {
            var lines = EmbeddedResourceUtils.ReadLines("Day6.txt");
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

        public class Satellite
        {
            public string Name { get; }
            public Satellite Orbits { get; private set; }
            public List<Satellite> Satellites { get; } = new List<Satellite>();
            public int Depth => Orbits == null ? 0 : Orbits.Depth + 1;
            public List<Satellite> Parents => Orbits == null ? new List<Satellite>() : new List<Satellite>(Orbits.Parents).Append(Orbits).ToList();

            public Satellite(string name)
            {
                Name = name;
            }

            public void AddSatellite(Satellite satellite)
            {
                Satellites.Add(satellite);
                satellite.Orbits = this;
            }
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

        private string ToString(Satellite primary, string prefix = "", bool isLast = true)
        {
            var builder = new StringBuilder();

            builder.Append(prefix);
            builder.Append(isLast ? "└" : "├");
            builder.AppendLine(primary.Name);

            for (var i = 0; i < primary.Satellites.Count; i++)
            {
                var satellite = primary.Satellites[i];
                var satellitePrefix = prefix + (isLast ? " " : "│");
                var isLastSatellite = i == (primary.Satellites.Count - 1);

                builder.Append(ToString(satellite, satellitePrefix, isLastSatellite));
            }

            return builder.ToString();
        }
    }
}
