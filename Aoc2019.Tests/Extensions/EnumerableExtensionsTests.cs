using Aoc2019.Extensions;
using Aoc2019.Tests.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Aoc2019.Tests.Extensions
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        [DataTestMethod]
        [DataRow(14, "abcd", "cbad")]

        public void PermutationTest(int n, string elements, string expectedPermutation) => Assert.AreEqual(expectedPermutation, string.Join("", elements.ToCharArray().Permutation(n)));

        [DataTestMethod]
        [DataRow("abc", new object[] { new string[] { "abc", "acb", "bac", "bca", "cab", "cba" } })]
        public void PermutationsTest(string elements, string[] expectedPermutations)
        {
            var permutations = elements.ToCharArray().Permutations().Select(x => string.Join("", x));
            Assert.That.SequenceEquals(expectedPermutations, permutations);
        }
    }
}
