using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Aoc2019.Util
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
            var resourceName = resourceNames.SingleOrDefault(x => x.Contains(fileName));

            var stream = assembly.GetManifestResourceStream(resourceName);
            var reader = new StreamReader(stream);
            return reader;
        }
    }
}
