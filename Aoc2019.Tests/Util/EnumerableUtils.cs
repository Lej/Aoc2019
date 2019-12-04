using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2019.Tests.Util
{
    public class EnumerableUtils
    {
        public static IEnumerable<int> FromTo(int from, int to)
        {
            if (to < from) throw new ArgumentException(nameof(to));
            return Enumerable.Range(from, to - from + 1);
        }
    }
}
