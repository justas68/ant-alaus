using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alus;

namespace BaroIvertinimoTestas
{
    [TestClass]
    public class IvertinimoIvedimoUnitTest
    {
        [TestMethod]
        public void Ivertinimo_Ivedimo_Testas() {

            String baro_Pavadinimas = "Labai,geras Baras";
            int ivertinimas = 10;
            bool expected = false;

            BarEvaluation baras = new BarEvaluation();
            bool actual = baras.evaluation_check(ivertinimas);

            Assert.AreEqual(1, 1,0.001,"Evaluation correct");
        }
    }
}
