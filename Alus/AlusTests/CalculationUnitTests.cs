using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Alus.Tests
{
    [TestClass()]
    public class CalculationUnitTests
    {
        [TestMethod()]
        public void percentageIsAnswer50()
        {
            //arrange

            Point[] p1 = { new Point(0, 0), new Point(0, 50), new Point(0, 100) };
            Point[] p2 = { new Point(50, 0), new Point(50, 50), new Point(50, 100) };
            Calculator calc = new Calculator();

            //Action

            double d = calc.percentage(p1, p2);
            
            //assert

            Assert.AreEqual(50, d);
        }

    }
}