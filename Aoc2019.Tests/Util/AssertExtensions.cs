using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2019.Tests.Util
{
    public static class AssertExtensions
    {
        public static void SequenceEquals<T>(this Assert _, IList<T> expected, IList<T> actual)
        {
            if (!Enumerable.SequenceEqual(expected, actual))
            {
                Assert.Fail($"Expected [{string.Join(", ", expected)}] but got [{string.Join(", ", actual)}]");
            }
        }
    }
}
