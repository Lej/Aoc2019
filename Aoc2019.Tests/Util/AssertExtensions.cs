using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2019.Tests.Util
{
    public static class AssertExtensions
    {
        public static void SequenceEquals<T>(this Assert _, IEnumerable<T> expected, IEnumerable<T> actual)
        {
            var e = expected.ToList();
            var a = actual.ToList();
            if (!Enumerable.SequenceEqual(e, a))
            {
                Assert.Fail($"Expected [{string.Join(", ", e)}] but got [{string.Join(", ", a)}]");
            }
        }
    }
}
