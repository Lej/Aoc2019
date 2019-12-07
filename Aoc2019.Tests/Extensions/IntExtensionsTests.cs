using Aoc2019.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests.Extensions
{
    [TestClass]
    public class IntExtensionsTests
    {
        [DataTestMethod]
        [DataRow(349, 242010)]

        public void ToFactoradicTest(int base10, int expectedFactoradic) => Assert.AreEqual(expectedFactoradic, base10.ToFactoradic());
    }
}
