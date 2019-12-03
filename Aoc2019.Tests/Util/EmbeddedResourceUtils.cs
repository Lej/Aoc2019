using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Aoc2019.Tests.Util
{
    public class EmbeddedResourceUtils
    {
        public static string ReadToEnd(string fileName)
        {
            using (var reader = GetManifestResourceStream(fileName))
            {
                return reader.ReadToEnd();
            }
        }

        public static IEnumerable<string> ReadLines(string fileName)
        {
            var lines = new List<string>();
            using (var reader = GetManifestResourceStream(fileName))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines;
        }

        public static StreamReader GetManifestResourceStream(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames = assembly.GetManifestResourceNames();
            var filteredResourceNames = resourceNames.Where(x => x.Contains(fileName)).ToList();

            if (filteredResourceNames.Count == 0)
            {
                throw new InvalidOperationException($"Embedded resource '{fileName}' not found. Did you forget to embed it?");
            }
            if (filteredResourceNames.Count > 1)
            {
                throw new InvalidOperationException($"Found multiple embedded resources for '{fileName}': {string.Join(", ", filteredResourceNames)}");
            }

            var resourceName = filteredResourceNames.Single();
            var stream = assembly.GetManifestResourceStream(resourceName);
            var reader = new StreamReader(stream);
            return reader;
        }
    }
}
