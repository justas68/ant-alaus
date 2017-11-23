using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Alus.Tests
{
    [TestClass]
    public class BarFileContainerTest
    {
        [TestMethod]
        public void ReadWriteToFile()
        {
            IBarContainer barContainer = new BarFileContainer("tmp.txt");
            Bar bar = new Bar(
              "Fabai",
              "54.734658,25.2540789",
              4.4,
              "Ateities gatvė 21",
              "ChIJwZrwhxOR3UYRVOQRS3PA5PI",
              "6",
              50,
              5
            );
            barContainer.Add(bar);
            List<Bar> barList;
            barList = new List<Bar>(barContainer.GetAll());
            Bar bar2 = barList[0];
            Assert.IsNotNull(bar2.Name);
            Assert.IsNotNull(bar2.Address);
        }
    }
}
