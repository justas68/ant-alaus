using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Alus.Tests
{
    [TestClass]
    public class ListExtensionsTest
    {
        [TestMethod]
        public void CheckValidRanges()
        {
            var list = new List<int>(new int[] { 1, 2, 3 });
            Assert.IsTrue(list.InRange(0));
            Assert.IsTrue(list.InRange(1));
            Assert.IsTrue(list.InRange(2));
        }

        [TestMethod]
        public void CheckInvalidRanges()
        {
            var list = new List<int>(new int[] { 1, 2, 3 });
            Assert.IsFalse(list.InRange(-1));
            Assert.IsFalse(list.InRange(5));
        }
    }
}
