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
        public void PercentageIsAnswer50()
        {
            //arrange

            Point[] p1 = { new Point(0, 0), new Point(0, 50), new Point(0, 100) };
            Point[] p2 = { new Point(50, 0), new Point(50, 50), new Point(50, 100) };
            Calculator calc = new Calculator();

            //Action

            double d = calc.Percentage(p1, p2);
            
            //assert

            Assert.AreEqual(50, d);
        }
        [TestMethod()]
        public void DoesItSort()
        {
            //arrange

            Point[] p1 = { new Point(0, 20), new Point(0, 0), new Point(0, 60) };
            Point[] p2 = { new Point(50, 20), new Point(50, 0), new Point(50, 60) };
            Calculator calc = new Calculator();

            //Action

            double d = calc.Percentage(p1, p2);

            //assert

            Assert.AreEqual(Math.Round((2.0/3.0)*100, 5), Math.Round(d, 5));
        }
        [TestMethod()]
        public void AreLinesParallel()
        {
            //arrange
            Point[] p1 = { new Point(0, 20), new Point(0, 0), new Point(0, 60) };
            Point[] p2 = { new Point(70, 70), new Point(0, 80), new Point(50, 20) };
            Calculator calc = new Calculator();
        
            //Action

            bool isPar = (calc.IsParallel(p1[0], p2[0], p1[1], p2[1]) && calc.IsParallel(p1[0], p2[0], p1[2], p2[2]) && calc.IsParallel(p1[1], p2[1], p1[2], p2[2]));

            //assert

            Assert.AreEqual(isPar, false);
        }
    }
}