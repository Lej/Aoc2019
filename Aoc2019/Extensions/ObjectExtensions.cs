using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aoc2019.Extensions
{
    public static class ObjectExtensions
    {
        public static string ReadEmbedded(this object objectInAssembly, string embeddedResourceFileName)
        {
            using (var reader = GetManifestResourceStream(objectInAssembly, embeddedResourceFileName))
            {
                return reader.ReadToEnd();
            }
        }

        public static IEnumerable<string> ReadEmbeddedLines(this object objectInAssembly, string embeddedResourceFileName)
        {
            var lines = new List<string>();
            using (var reader = GetManifestResourceStream(objectInAssembly, embeddedResourceFileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines;
        }

        private static StreamReader GetManifestResourceStream(object objectInAssembly, string embeddedResourceFileName)
        {
            var assembly = objectInAssembly.GetType().Assembly;
            var resourceNames = assembly.GetManifestResourceNames();
            var filteredResourceNames = resourceNames.Where(x => x.Contains(embeddedResourceFileName)).ToList();

            if (filteredResourceNames.Count == 0)
            {
                throw new InvalidOperationException($"Embedded resource '{embeddedResourceFileName}' not found in assembly '{assembly.GetName()}'. Did you forget to embed it?");
            }
            if (filteredResourceNames.Count > 1)
            {
                throw new InvalidOperationException($"Found multiple embedded resources for '{embeddedResourceFileName}' in assembly '{assembly.GetName()}': {string.Join(", ", filteredResourceNames)}");
            }

            var resourceName = filteredResourceNames.Single();
            var stream = assembly.GetManifestResourceStream(resourceName);
            var reader = new StreamReader(stream);
            return reader;
        }
    }
}
