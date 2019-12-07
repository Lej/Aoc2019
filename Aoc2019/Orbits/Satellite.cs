using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc2019.Orbits
{
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

        public override string ToString()
        {
            return ToString(this, "", true);
        }

        private string ToString(Satellite primary, string prefix, bool isLast)
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
